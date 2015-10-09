using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddClientRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Please specify client's name")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Truck per day cannot be less than zero")]
        public int TrucksPerDay { get; set; }

        public IList<AddBranchRequest> Branches { get; set; }

        public AddClientRequest()
        {
            Branches = new List<AddBranchRequest>();
        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Branches.Any())
            {
                var branchNames = Branches.Select(b => b.Name).ToList();
                var branchNameDuplicate = branchNames.Distinct().Count() != branchNames.Count;
                if (branchNameDuplicate)
                {
                    yield return new ValidationResult("Two or more branches has the same name");
                }
            }

        }
    }
}
