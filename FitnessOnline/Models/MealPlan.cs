using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
       
        [Display(Name = "Assign To :")]
        public int ApplicationUserId { get; set; }

        [Display(Name = "Meal Plan Name")]
        public string Name { get; set; }

        [Display(Name = "Meal Plan:")]
        [Required]
        public string Meal { get; set; }


        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int TotalCalories { get; set; }



        [ForeignKey("AppUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}


