using AutoMapper;
using EnergyOutageNotifier.Business.Map;
using EnergyOutageNotifier.Business.Services.Abstracts;
using EnergyOutageNotifier.Business.Services.Base;
using EnergyOutageNotifier.Models.Context;
using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Dto.Common;
using EnergyOutageNotifier.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Business.Services.Concrates
{
    public class OutageService : BaseService<OutageDto, Outage,OutageClosureDto, Outage>, IOutageService
    {
        private readonly IMapper _mapper;

        public OutageService(IMapper mapper, EnerjisaDBContext dbContext) : base(mapper, dbContext)
        {
            _mapper = mapper;
        }

        public override async Task<TResult<Outage>> Add(OutageDto dto)
        {
            var entity = _mapper.Map<Outage>(dto);

            _dbContext.Outage.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new TResult<Outage>
            {
                IsSuccess = true,
                Data = entity
            };
        }
        public override async Task<TResult<Outage>> Get(long notificationId)
        {

            var entity = await _dbContext.Outage.FirstOrDefaultAsync(x => x.NotificationId == notificationId);


            return new TResult<Outage>
            {
                IsSuccess = true,
                Data = entity
            };
        }
  


        public override async Task<Outage> GetLast()
        {

            var outages = await _dbContext.Outage.Include(x=>x.Notification)
                       .OrderByDescending(x => x.OutageId).FirstOrDefaultAsync();
                    

            var entity = _mapper.Map<Outage>(outages);


            return entity;
        }

        //public override async Task<List<Outage>> GetAll()
        //{

        //    var outages = await _dbContext.Outage.Include(x => x.Notification).ToListAsync();

        //    var entity = _mapper.Map<List<Outage>>(outages);


        //    return entity;
        //}
      
        public override async Task<List<Outage>> GetAll()
        {
            var outages = await _dbContext.Outage.Include(x => x.Notification).AsNoTracking().ToListAsync();
            var entities = _mapper.Map<List<Outage>>(outages);
            return entities;
        }

        public  async Task<List<Outage>> GetCurrentOutages()
        {
            var outages = await _dbContext.Outage.Include(x => x.Notification).Where(x=>x.Notification.NotificationType==1).AsNoTracking().ToListAsync();
            var entities = _mapper.Map<List<Outage>>(outages);
            return entities;
        }




        public override async Task<TResult<Outage>> Update(OutageClosureDto dto)
        {
            var entity = await _dbContext.Outage.FirstOrDefaultAsync(o => o.NotificationId == dto.NotificationId);
            if (entity == null)
            {
                return new TResult<Outage> { IsSuccess = false, ErrorMessage = "Notification bulunamadı" };
            }

            _mapper.Map(dto, entity);

            await _dbContext.SaveChangesAsync();

            return new TResult<Outage> { IsSuccess = true, Data = entity };
        }
    }
}


