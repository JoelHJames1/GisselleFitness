using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Subscription Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater than ${1}")]
        public double  Price { get; set; }

        public enum SubscriptionType { Basic=0,Normal=1,Premium=2}

        public bool isSubscriptionActive { get; set; }

       
        public byte[] SubscriptionImage { get; set; }


        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


    }
}
