using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public Employee GetEmployee(string employeeId);
        public bool CreateEmployee(string firstName, string lastName, string phone, string jobTitle, string userId);
        public bool Remove(string employeeId);
        public string GetFullName(string employeeId);
        public bool Update(string id, string firstName, string lastName, string phone);
    }
}
