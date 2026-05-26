using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Domain.ValueObjects;
using MultiTenantEmployeeApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace MultiTenantEmployeeApi.Test.IntegerationTest
{
    public class EmployeesIntegrationTests :
    IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres;

        private WebApplicationFactory<Program> _factory = null!;

        private HttpClient _client = null!;

        public EmployeesIntegrationTests()
        {
            _postgres = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _postgres.StartAsync();

            _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["ConnectionStrings:DefaultConnection"] = _postgres.GetConnectionString()
                    });
                });

                builder.ConfigureServices(services =>
                {
                    var descriptor = services
                        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseNpgsql(_postgres.GetConnectionString());
                    });
                });
            });
       

            // migrate
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();

            _client = _factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            await _postgres.DisposeAsync();
        }

        [Fact]
        public async Task CreateEmployee_Should_Work()
        {
            // Arrange
            var tenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var request = new
            {
                firstName = "Ahmed",
                lastName = "Eid",
                email = "ahmed@test.com",
                department = "IT",
                amountMinor = 100000,
                currencyCode = "USD"
            };

            _client.DefaultRequestHeaders.Add(
                "X-Tenant-Id",
                tenantId.ToString());

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/v1/employees",
                request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }


        [Fact]
        public async Task Should_Return_Only_Current_Tenant_Data()
        {
            // Arrange
            //var tenantA = Guid.Parse("11111111-1111-1111-1111-111111111111");
            //var tenantB = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var tenantA = Guid.NewGuid();
            var tenantB = Guid.NewGuid();

            // seed data (Tenant A + Tenant B)
            var factory = _factory;

            using (var scope = factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var tenant1 = new Tenant
                {
                    ID = tenantA,
                    Name = "Tenant A"
                };
                var tenant2 = new Tenant
                {
                    ID = tenantB,
                    Name = "Tenant B"
                };
                db.Tenants.AddRange(
                    tenant1
               , tenant2
                );

                await db.SaveChangesAsync();

                var requestA = new
                {
                    firstName = "Ahmed",
                    lastName = "A",
                    email = "a@test.com",
                    department = "IT",
                    amountMinor = 100000,
                    currencyCode = "USD"
                };

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(
                    "X-Tenant-Id",
                    tenantA.ToString());

                // Act
                var responseEmpA = await _client.PostAsJsonAsync(
                    "/api/v1/employees",
                    requestA);
                responseEmpA.EnsureSuccessStatusCode();

                var requestB = new
                {
                    firstName = "Ali",
                    lastName = "B",
                    email = "b@test.com",
                    department = "HR",
                    amountMinor = 200000,
                    currencyCode = "USD"
                };

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(
                    "X-Tenant-Id",
                    tenantB.ToString());

                // Act
                var responseEmpB = await _client.PostAsJsonAsync(
                    "/api/v1/employees",
                    requestB);
                responseEmpB.EnsureSuccessStatusCode();

            }

            // Act
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantA.ToString());

            var response = await _client.GetAsync("/api/v1/employees?pageNumber=1&pageSize=10");

            // Assert
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWrapper<PagedResult<EmployeeDto>>>();

            result.Should().NotBeNull();
            result.Data?.Items.Should().HaveCount(1);
            result.Data?.Items.First().Email.Should().Be("a@test.com");
        }

    }
}
