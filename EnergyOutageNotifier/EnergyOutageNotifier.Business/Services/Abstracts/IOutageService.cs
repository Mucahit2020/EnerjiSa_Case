using EnergyOutageNotifier.Models.Dto.Common;
using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Business.Services.Abstracts
{
    public interface IOutageService
    {
        Task<TResult<Outage>> Add(OutageDto dto);
        Task<TResult<Outage>> Update(OutageClosureDto dto);
        Task<TResult<Outage>> Get(long notificationId);
        Task<Outage> GetLast();
        Task<List<Outage>> GetAll();
        Task<List<Outage>> GetCurrentOutages();


    }
}
