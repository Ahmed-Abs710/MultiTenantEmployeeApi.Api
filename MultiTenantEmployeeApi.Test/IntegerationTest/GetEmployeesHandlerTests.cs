using Microsoft.EntityFrameworkCore;
using Moq;
using MultiTenantEmployeeApi.Application.Queries.GetEmployees;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Infrastructure.Multitenancy;
using MultiTenantEmployeeApi.Infrastructure.Persistence;
using FluentAssertions;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;
using MultiTenantEmployeeApi.Domain.ValueObjects;

namespace MultiTenantEmployeeApi.Test.IntegerationTest
{
    public class GetEmployeesHandlerTests
    {
        private readonly ApplicationDbContext context;
        private readonly Mock<ICurrentTenant> currentTenantMock;
        private readonly GetEmployeesHandler handler;
        private readonly IRepository<Employee> repository;

        public GetEmployeesHandlerTests()
        {
            currentTenantMock = new Mock<ICurrentTenant>();

            currentTenantMock.Setup(x => x.TenantId)
                .Returns(Guid.NewGuid());

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(
            options,
                currentTenantMock.Object);

            repository = new Repository<Employee>(context);

            handler = new GetEmployeesHandler(repository);
        }

        [Fact]
        public async Task Handle_Should_Return_Paginated_Data()
        {
            // Arrange
            for (int i = 1; i <= 20; i++)
            {
                context.Employees.Add(new Employee
                {
                    ID = Guid.NewGuid(),
                    TenantId = currentTenantMock.Object.TenantId,
                    FirstName = $"Emp{i}",
                    LastName = "Test",
                    Email = $"emp{i}@test.com",
                    Department = "IT",
                    Salary = new Money(100000, "USD")
                });
            }

            await context.SaveChangesAsync();

            var query = new GetEmployeesQuery(1, 5);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Items.Count.Should().Be(5);

            result.TotalCount.Should().Be(20);
        }

      
    }

}
