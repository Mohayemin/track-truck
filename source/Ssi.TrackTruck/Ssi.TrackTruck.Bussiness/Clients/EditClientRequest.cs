using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    // TODO: this class has a lot of duplicacies with AddClientRequest, fix it
    public class EditClientRequest
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please specify client's name")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Truck per day cannot be less than zero")]
        public int TrucksPerDay { get; set; }

        public List<DbTextItem> Addresses { get; set; }

        public List<EditBranchRequest> Branches { get; set; }


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

            if (Addresses.Any())
            {
                var addresses = Addresses.Select(item => item.Text).ToList();
                var addressDuplicate = addresses.Distinct().Count() != addresses.Count();
                if (addressDuplicate)
                {
                    yield return new ValidationResult("Two or more addresses are same");
                }
            }
        }
    }
}
