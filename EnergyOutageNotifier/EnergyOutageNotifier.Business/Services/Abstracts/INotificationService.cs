using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Dto.Common;
using EnergyOutageNotifier.Models.Entity;

namespace EnergyOutageNotifier.Business.Services.Abstracts
{
    public interface INotificationService
    {
        Task<TResult<Notification>> Add(NotificationDto dto);
        Task<TResult<Notification>> Update(OutageClosureDto dto);
        Task<TResult<Notification>> Get(long notificationId);
        Task<Notification> GetLast();



    }
}