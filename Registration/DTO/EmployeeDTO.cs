using System.ComponentModel.DataAnnotations;

namespace Registration.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }
    }
}
