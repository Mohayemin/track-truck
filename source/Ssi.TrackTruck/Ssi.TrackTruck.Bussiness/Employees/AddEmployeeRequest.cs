using System.ComponentModel.DataAnnotations;

namespace Ssi.TrackTruck.Bussiness.Employees
{
    public class AddEmployeeRequest
    {
        [Required(ErrorMessage = "Please specify employee's first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please specify employee's last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please specify employee's designation")]
        public string Designation { get; set; }
    }
}
