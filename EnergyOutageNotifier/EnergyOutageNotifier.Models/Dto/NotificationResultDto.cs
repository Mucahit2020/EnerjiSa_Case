using EnergyOutageNotifier.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Dto
{
    public class NotificationResultDto
    {
        public Notification Notification { get; set; }
        public Outage Outage { get; set; }

    }
}
