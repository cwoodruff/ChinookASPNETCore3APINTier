using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chinook.Domain.Entities
{
    public class Employee : IConvertModel<Employee, EmployeeApiModel>
    {
        public Employee()
        {
            Customers = new HashSet<Customer>();
            InverseReportsToNavigation = new HashSet<Employee>();
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public int? ReportsTo { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual Employee ReportsToNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Customer> Customers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
        
        public EmployeeApiModel Convert() =>
            new EmployeeApiModel
            {
                EmployeeId = EmployeeId,
                LastName = LastName,
                FirstName = FirstName,
                Title = Title,
                ReportsTo = ReportsTo,
                BirthDate = BirthDate,
                HireDate = HireDate,
                Address = Address,
                City = City,
                State = State,
                Country = Country,
                PostalCode = PostalCode,
                Phone = Phone,
                Fax = Fax,
                Email = Email
            };
    }
}