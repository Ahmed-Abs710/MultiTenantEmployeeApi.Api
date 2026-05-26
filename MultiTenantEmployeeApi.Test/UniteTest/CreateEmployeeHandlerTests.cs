using Moq;
using MultiTenantEmployeeApi.Application.Commands.CreateEmployee;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using MultiTenantEmployeeApi.Domain.Exceptions;
using MultiTenantEmployeeApi.Application.Queries.GetEmployees;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;

namespace MultiTenantEmployeeApi.Test.UniteTest
{
    public class CreateEmployeeHandlerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly CreateEmployeeHandler _handler;

        public CreateEmployeeHandlerTests()
        {
            _uowMock = new Mock<IUnitOfWork>();

            _handler = new CreateEmployeeHandler(_uowMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Employee()
        {
            // Arrange
            var command = new CreateEmployeeCommand(
                "Ahmed",
                "Eid",
                "ahmed@test.com",
                "IT",
                100000,
                "USD"
            );

            _uowMock.Setup(x =>
                x.Employees.AnyAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();

            result.Email.Should().Be(command.Email);

            _uowMock.Verify(x =>
                x.Employees.AddAsync(It.IsAny<Employee>()),
                Times.Once);

            _uowMock.Verify(x =>
                x.SaveChangesAsync(default),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_Email_Exists()
        {
            // Arrange
            var command = new CreateEmployeeCommand(
                "Ahmed",
                "Eid",
                "exists@test.com",
                "IT",
                100000,
                "USD"
            );

            _uowMock.Setup(x =>
                x.Employees.AnyAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .ReturnsAsync(true);

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(command, default);

            // Assert
            await act.Should()
                .ThrowAsync<DuplicateException>();
        }
    }

}
