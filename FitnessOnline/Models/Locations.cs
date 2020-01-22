using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessOnline.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationAddress { get; set; }
        [Required]
        public string LocationCity { get; set; }
        [Required]
        public string LocationZipCode { get; set; }

        public virtual ICollection<LocationSchedule> LocationSchedule { get; set; }
    }
}
