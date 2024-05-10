using AutoMapper;
using EnergyOutageNotifier.Models.Context;
using EnergyOutageNotifier.Models.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Business.Services.Base
{
    public abstract class BaseService<TDto, TEntity, TUpdateEntity, T> where TEntity : class // bu kısmı Tentity'i referans olarak verebilmek için ekledim.
    {
        protected readonly IMapper _mapper;
        protected readonly EnerjisaDBContext _dbContext;

        protected BaseService(IMapper mapper, EnerjisaDBContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public abstract Task<TResult<T>> Add(TDto dto);

        protected async Task<TEntity> AddEntity(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public abstract Task<TResult<T>> Update(TUpdateEntity dto);

        protected async Task<TEntity> UpdateEntity(TUpdateEntity dto)
        {
            
            var entity = _mapper.Map<TEntity>(dto);

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }


        public abstract Task<TResult<T>> Get(long notificationId);

        protected async Task<TEntity> GetEntity(long notificationId)
        {
            var entity = await _dbContext.FindAsync<TEntity>(notificationId);
            if (entity == null)
            {
                // Eğer var olmayan bir varlıkla karşılaşılırsa, gerekirse bir hata fırlatılabilir.
                // throw new EntityNotFoundException($"Entity with id {notificationId} not found.");
            }
            return entity;
        }

        public abstract Task<TEntity> GetLast();
        public abstract Task<List<TEntity>> GetAll();


        protected async Task<TEntity> GetLastEntity()
        {
            var entity = await _dbContext.FindAsync<TEntity>();
            if (entity == null)
            {
                // Eğer var olmayan bir varlıkla karşılaşılırsa, gerekirse bir hata fırlatılabilir.
                // throw new EntityNotFoundException($"Entity with id {notificationId} not found.");
            }
            return entity;
        }

    }
}