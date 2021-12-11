using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Company.WebApi.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
