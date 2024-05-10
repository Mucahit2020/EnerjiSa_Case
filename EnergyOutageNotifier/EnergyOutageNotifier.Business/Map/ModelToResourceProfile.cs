using AutoMapper;
using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyOutageNotifier.Business.Map
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap <NotificationDto,Notification>()
                    .ForMember(dest => dest.NotificationDetail, opt => opt.MapFrom(src => src.NotificationDetails.NotificationDetail));

            CreateMap<OutageDto, Outage>()
                    .ForMember(dest => dest.NotificationId, opt => opt.MapFrom(src => src.NotificationId))
                    .ForMember(dest => dest.AffectedArea, opt => opt.MapFrom(src => src.NotificationDto.AffectedArea))
                    .ForMember(dest => dest.OutageStartTime, opt => opt.MapFrom(src => src.NotificationDto.OutageStartTime))
                    .ForMember(dest => dest.OutageETD, opt => opt.MapFrom(src => src.NotificationDto.OutageETD))
                    .ForMember(dest => dest.OutageCause, opt => opt.MapFrom(src => src.NotificationDto.NotificationDetails.OutageCause));


            CreateMap<OutageClosureDto, Notification>()
                    .ForMember(dest => dest.NotificationId, opt => opt.MapFrom(src => src.NotificationId))
                    .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.NotificationType));

            CreateMap<OutageClosureDto, Outage>()
                    .ForMember(dest => dest.NotificationId, opt => opt.MapFrom(src => src.NotificationId))
                    .ForMember(dest => dest.OutageEndTime, opt => opt.MapFrom(src => src.OutageEndTime));


        }
    }
}