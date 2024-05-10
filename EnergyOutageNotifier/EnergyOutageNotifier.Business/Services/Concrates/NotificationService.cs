using AutoMapper;
using EnergyOutageNotifier.Business.Map;
using EnergyOutageNotifier.Business.Services.Abstracts;
using EnergyOutageNotifier.Business.Services.Base;
using EnergyOutageNotifier.Models.Context;
using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Dto.Common;
using EnergyOutageNotifier.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EnergyOutageNotifier.Business.Services.Concrates
{

    public class NotificationService : BaseService<NotificationDto, Notification,OutageClosureDto,Notification>,INotificationService
    {
        private readonly IMapper _mapper;
        public NotificationService(IMapper mapper, EnerjisaDBContext dbContext) : base(mapper, dbContext)
        {
            _mapper = mapper;

        }

        public override async Task<TResult<Notification>> Add(NotificationDto dto)
        {


            var entity = _mapper.Map<Notification>(dto);

            _dbContext.Notification.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new TResult<Notification>
            {
                IsSuccess = true,
                Data = entity
            };
        }

        public override async Task<TResult<Notification>> Get(long notificationId)
        {

           var entity=  await  _dbContext.Notification.FindAsync(notificationId);


            return new TResult<Notification>
            {
                IsSuccess = true,
                Data = entity
            };
        }

        public override async Task<Notification> GetLast()
        {

            var notifications = await _dbContext.Notification
                       .OrderByDescending(x => x.NotificationId).FirstOrDefaultAsync();

            var entity =  _mapper.Map<Notification>(notifications);


            return entity;
        }

        public override async Task<List<Notification>> GetAll()
        {
            var notification = await _dbContext.Notification.AsNoTracking().ToListAsync();
            var entities = _mapper.Map<List<Notification>>(notification);
            return entities;
        }
 


        public override async Task<TResult<Notification>> Update(OutageClosureDto dto)
        {
            var entity = await _dbContext.Notification.FindAsync(dto.NotificationId);
            if (entity == null)
            {
                return new TResult<Notification> { IsSuccess = false,  ErrorMessage = "Notification bulunamadı" };
            }

            _mapper.Map(dto, entity);

            await _dbContext.SaveChangesAsync();

            return new TResult<Notification> { IsSuccess = true, Data = entity };
        }

    }
}

