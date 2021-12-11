using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Mobile.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
