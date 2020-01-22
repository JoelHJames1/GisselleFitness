using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models.ViewModel
{
    public class SubscriptionViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than ${1}")]
        public double Price { get; set; }

     
        [Display(Name = "Package Image")]
        public IFormFile SubscriptionImage { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
