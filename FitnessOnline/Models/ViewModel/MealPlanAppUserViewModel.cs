using System;
using System.Collections.Generic;

namespace FitnessOnline.Models.ViewModel
{
    public class MealPlanAppUserViewModel
    {
      // List of Users to Assign From
      public IEnumerable<ApplicationUser> UserList { get; set; }
      
        // Meal Plan Name
      public MealPlan MealPlan { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
