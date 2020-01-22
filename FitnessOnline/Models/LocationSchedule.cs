using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class LocationSchedule
    {
        [Key]
        public int LocationScheduleId { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

    }
}
