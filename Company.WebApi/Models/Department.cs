using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Company.WebApi.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
