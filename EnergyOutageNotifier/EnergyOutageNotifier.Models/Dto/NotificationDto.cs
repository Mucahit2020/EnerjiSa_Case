using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Models.Dto
{
    public class NotificationDto
    {
        public DateTime? OutageStartTime { get; set; } // Kesinti başlangıç
        public DateTime? OutageETD { get; set; } // Kesinti tahmini bitiş
        public byte NotificationType { get; set; } // Bildirim Tipi : Kesinti başlangıcı mı bitişi mi?
        public string AffectedArea { get; set; } // Kesintiden etkilenen bölge
        public NotificationDetailsDto NotificationDetails { get; set; } 

    }
}
