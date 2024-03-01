using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }

        public Employee(int id, string firstName, string lastName, string email, string department, string title, int salary)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Department = department;
            Title = title;
            Salary = salary;
        }
    }
}