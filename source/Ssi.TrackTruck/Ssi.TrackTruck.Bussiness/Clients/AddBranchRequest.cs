﻿using System.ComponentModel.DataAnnotations;
using Ssi.TrackTruck.Bussiness.DAL.Clients;

namespace Ssi.TrackTruck.Bussiness.Clients
{
    public class AddBranchRequest
    {
        [Required(ErrorMessage = "Please specify branch name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please specify branch address")]
        public string Address { get; set; }
        
        public DbBranch ToBranch()
        {
            return new DbBranch { Name = Name, Address = Address };
        }
    }
}
