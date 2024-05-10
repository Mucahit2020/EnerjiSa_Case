using EnergyOutageNotifier.API.Extensions.Event;
using EnergyOutageNotifier.Business.Services.Abstracts;
using EnergyOutageNotifier.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EnergyOutageNotifier.API.Controllers
{
    public class OutageController : ControllerBase
    {
        public OutageController()
        {
            
        }

        [HttpPost("outageAdd")]
        public async Task<IActionResult> Add([FromBody] NotificationDto notificationDto)
        {
            //var rt = await _notificationService.Add(notificationDto);

            //return Ok(rt);

            return null;
        }

        //[HttpGet("outageNotificationsGet")]
        //public async Task<NotificationDto> Get(long notificationId)
        //{
        //    var rt = await _notificationService.Get(notificationId);

        //    return rt;
        //}
        //[HttpGet("outageNotificationsGetAll")]
        //public async Task<List<NotificationDto>> GetAll()
        //{
        //    var rt = await _notificationService.GetAll();

        //    return rt;
        //}
    }
}