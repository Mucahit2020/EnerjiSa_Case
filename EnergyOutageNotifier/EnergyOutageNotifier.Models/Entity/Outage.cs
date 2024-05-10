using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Entity
{
    public class Outage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OutageId { get; set; } // Primary key

        [Required]
        public DateTime OutageStartTime { get; set; } // Zorunlu alan

        [Required]
        public DateTime OutageETD { get; set; } // Zorunlu alan

        [Required]
        public DateTime OutageEndTime { get; set; } // Zorunlu alan

        [Required]
        public string AffectedArea { get; set; } // Zorunlu alan

        [Required]
        public string OutageCause { get; set; } // Zorunlu alan

        [ForeignKey("Notification")]
        public long NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}