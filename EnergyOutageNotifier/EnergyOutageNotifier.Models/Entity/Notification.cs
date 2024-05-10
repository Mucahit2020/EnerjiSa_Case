using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Entity
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationId { get; set; } // Primary key

        [Required]
        public DateTime NotificationTime { get; set; } // Zorunlu alan

        [Required]
        [Range(0, 1)]
        public byte NotificationType { get; set; } // 0 ve 1 değerlerini alabilen zorunlu bit alan
        [Required]
        public string AffectedArea { get; set; }

        public string NotificationDetail { get; set; } 
    }
}
