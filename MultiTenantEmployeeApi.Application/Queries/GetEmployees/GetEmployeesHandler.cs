using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantEmployeeApi.Application.DTOs;
using MultiTenantEmployeeApi.Application.Models;
using MultiTenantEmployeeApi.Domain.Entities;
using MultiTenantEmployeeApi.Domain.ValueObjects;
using MultiTenantEmployeeApi.Infrastructure.Persistence;
using MultiTenantEmployeeApi.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEmployeeApi.Application.Queries.GetEmployees
{
    public class GetEmployeesHandler
     : IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
    {
        private readonly IRepository<Employee> _repo;

        public GetEmployeesHandler(IRepository<Employee> repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<EmployeeDto>> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(request.Search) ||
                    x.LastName.Contains(request.Search) ||
                    x.Email.Contains(request.Search));
            }

            var total = await _repo.CountAsync(query, cancellationToken);

            var items = await _repo.GetPagedAsync(
                query,
                (request.PageNumber - 1) * request.PageSize,
                request.PageSize,
                cancellationToken);

            return new PagedResult<EmployeeDto>
            {
                Items = items.Select(x => new EmployeeDto
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Department = x.Department,
                    Salary = new Money(x.Salary.AmountMinor,x.Salary.CurrencyCode)
                }).ToList(),
                TotalCount = total,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
