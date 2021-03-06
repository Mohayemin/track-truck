﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ssi.TrackTruck.Bussiness.Auth
{
    public class AddUserRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string InitialPassword { get; set; }
        public Role Role { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Role == 0)
            {
                yield return new ValidationResult("Please choose a role");
            }
        }
    }
}
