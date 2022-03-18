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
            var employee = context.Employees.Find(employeeId);

            if (employee.User == null)
            {
                employee.User = context.Users.Find(employee.UserId);
            }

            return employee;
        }

        public List<Employee> GetEmployees()
        {
            var list = context.Employees.ToList();

            foreach (var item in list)
            {
                if (item.User == null)
                {
                    item.User = context.Users.Find(item.UserId);
                }
            }
            return list;
        }

        public string GetFullName(string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                return false;
            }
            var user = context.Users.Find(employee.UserId);

            context.Users.Remove(user);
            context.Employees.Remove(employee);

            return context.SaveChanges() != 0;
        }
    }
}
