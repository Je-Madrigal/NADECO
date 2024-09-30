using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace DataLibrary.Models
{
    public class Members
    {
        public int Timestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryBy { get; set; }
        public string No_ { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string MemberId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string CityNo { get; set; }
        public string Barangay { get; set; }
        public string BarangayNo { get; set; }
        public string ZipCode { get; set; }
        public string EmployeeStatus { get; set; }
        public DateTime MembershipDate { get; set; }
        public string EmployeeNo { get; set; }
        public string Employer { get; set; }
        public string TINNo { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
        public decimal Salary { get; set; }
        public string OtherSource { get; set; }
        public int Dependents { get; set; }
        public string DependentName { get; set; }
        public int DependentAge { get; set; }
        public string Relationship { get; set; }
        public string AccountStatus { get; set; }

        public string RegistrationStatus { get; set; }
    }
}
