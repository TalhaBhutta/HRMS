//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using HRMS.Models;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.IO;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;

namespace HRMS.ViewModels
{
    public class RecruterCreateViewModel
    {

        public Recruter newRecruter { get; set; }
        //public IEnumerable<Location> loc { get; set; }
        //public IEnumerable<Experience> Experiences { get; set; }

        public Location location { get; set; }

        ////public DbSet<Location> locations { get; set; }
        ////[BindProperty]
        //public Experience expe{ get; set; }

        //public List<Experience> expes { get; set; }

        //public List<Education> education { get; set; }
        public CreditCard creditcard { get; set; }
        public Bank bank { get; set; }
    }
}
