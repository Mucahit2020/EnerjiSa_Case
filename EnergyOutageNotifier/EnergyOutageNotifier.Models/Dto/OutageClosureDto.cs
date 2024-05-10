using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Dto
{
    public class OutageClosureDto
    {
        public long NotificationId { get; set; }
        [Required]
        [Range(0, 1)]
        public byte NotificationType { get; set; }
        public DateTime OutageEndTime { get; set; }

    }
}
