using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext context;

        public EmployeeService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public bool CreateEmployee(string firstName, string lastName, string phone, string jobTitle, string userId)
        {
            if (context.Employees.Any(x => x.UserId == userId))
            {
                throw new InvalidOperationException("User already exist.");
            }

            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                JobTitle = jobTitle,
                UserId = userId,
                Phone = phone
            };
            context.Employees.Add(employee);
            return context.SaveChanges() != 0;
        }

        public Employee GetEmployee(string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            throw new System.NotImplementedException();
        }

        public string GetFullName(string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
