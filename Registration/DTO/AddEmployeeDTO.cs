using System.ComponentModel.DataAnnotations;

namespace Registration.DTO
{
    public class AddEmployeeDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
    }
}
