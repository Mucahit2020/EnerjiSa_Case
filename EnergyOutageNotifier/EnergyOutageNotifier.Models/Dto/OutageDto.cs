using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Dto
{
    public class OutageDto
    {
        public required NotificationDto NotificationDto { get; set; }
        public long NotificationId { get; set; }

    }
}
