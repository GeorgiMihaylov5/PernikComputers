using Microsoft.EntityFrameworkCore;
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
                Phone = phone,
            };
            context.Employees.Add(employee);
            return context.SaveChanges() != 0;
        }

        public Employee GetEmployee(string employeeId)
        {
            return context.Employees.FirstOrDefault(x => x.UserId == employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public string GetFullName(string employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                return null;
            }
            return $"{employee.FirstName} {employee.LastName}";
        }

        public bool Remove(string employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var myOrders = context.Orders.Where(x => x.CustomerId == employee.UserId);

            if (employee == null)
            {
                return false;
            }

            foreach (var order in myOrders)
            {
                context.Orders.Remove(order);
            }
            var user = context.Users.Find(employee.UserId);

            context.Users.Remove(user);
            context.Employees.Remove(employee);

            return context.SaveChanges() != 0;
        }

        public bool Update(string id, string firstName, string lastName, string phone)
        {
            var employees = context.Employees.Find(id);

            if (employees == null)
            {
                return false;
            }
            employees.FirstName = firstName;
            employees.LastName = lastName;
            employees.Phone = phone;

            context.Employees.Update(employees);
            return context.SaveChanges() != 0;
        }
    }
}
