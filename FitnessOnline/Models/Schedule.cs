using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
      
        public DateTime ScheduleDateTime { get; set; }

        public virtual ICollection<LocationSchedule> LocationSchedule { get; set; }
    }
}
