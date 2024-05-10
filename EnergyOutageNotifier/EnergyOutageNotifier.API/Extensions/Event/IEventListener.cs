using EnergyOutageNotifier.Models.Dto;

namespace EnergyOutageNotifier.API.Extensions.Event
{
    // Olayları dinleyen arayüz
    public interface IEventListener
    {
        void OnEvent(NotificationDto notification);
    }

    // Olay yayıncısı (Event Bus)
    public class EventBus
    {
        private List<IEventListener> listeners = new List<IEventListener>();

        // Olay dinleyicisi ekleme
        public void AddListener(IEventListener listener)
        {
            listeners.Add(listener);
        }

        // Olay yayınlama
        public void PublishEvent(NotificationDto notification)
        {
            foreach (var listener in listeners)
            {
                listener.OnEvent(notification);
            }
        }
    }
}