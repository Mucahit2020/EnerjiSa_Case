using AutoMapper;
using EnergyOutageNotifier.API.Extensions.Event;
using EnergyOutageNotifier.API.Extensions.SignalR;
using EnergyOutageNotifier.Business.Services.Abstracts;
using EnergyOutageNotifier.Models.Dto;
using EnergyOutageNotifier.Models.Dto.Common;
using EnergyOutageNotifier.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace EnergyOutageNotifier.API.Controllers
{
    [ApiController]

    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IOutageService _outageService;

        private readonly IHubContext<NotificationHub> _hubContext;


        public NotificationController(INotificationService notificationService, IOutageService outageService, IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _outageService = outageService;
            _hubContext = hubContext;

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] NotificationDto notificationDto)
        {
            var resultNotification = await _notificationService.Add(notificationDto);

            if (!resultNotification.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "NotificationService hata: " + resultNotification.ErrorMessage
                };
                return BadRequest(baseResult);
            }

            OutageDto outageDto = new OutageDto
            {
                NotificationDto = notificationDto,
                NotificationId = resultNotification.Data.NotificationId
            };


            var resultOutage = await _outageService.Add(outageDto);

            if (resultOutage.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = true,
                    Data = new { Notification = resultNotification.Data, Outage = resultOutage.Data }
                };

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Yeni bildirim!");

                return Ok(baseResult);
            }
            else
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "OutageService hata: " + resultOutage.ErrorMessage
                };
                return BadRequest(baseResult);
            }
        }

        [HttpGet("get/{notificationId}")]
        public async Task<IActionResult> Get(long notificationId)
        {
            var resultNotification = await _notificationService.Get(notificationId);

            if (!resultNotification.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "NotificationService hata: " + resultNotification.ErrorMessage
                };
                return BadRequest(baseResult);
            }

            var outageNotification = await _outageService.Get(notificationId);


            if (outageNotification.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = true,
                    Data = new { Notification = resultNotification.Data, Outage = outageNotification.Data }
                };
                return Ok(baseResult);
            }

            else
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "OutageService hata: " + outageNotification.ErrorMessage
                };
                return BadRequest(baseResult);
            }

          
        }


        [HttpGet("get-last")]
        public async Task<IActionResult> GetLast()
        {
            try
            {

                var outage = await _outageService.GetLast();

                if (outage == null)
                {
                    return NotFound("outage bulunamadı");
                }

                return Ok(outage);
            }
            catch (Exception ex)
            {
                // Hata durumunda bir Internal Server Error yanıtı döndür
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }

        }

        [HttpGet("get-current")]
        public async Task<IActionResult> GetCurrentOutages()
        {
            try
            {

                var outage = await _outageService.GetCurrentOutages();

                if (outage == null)
                {
                    return NotFound("Mevcut kesinti bulunamadı.");
                }

                return Ok(outage);
            }
            catch (Exception ex)
            {
                // Hata durumunda bir Internal Server Error yanıtı döndür
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var outage = await _outageService.GetAll();

                if (outage == null)
                {
                    return NotFound("outage bulunamadı");
                }

                return Ok(outage);
            }
            catch (Exception ex)
            {
                // Hata durumunda bir Internal Server Error yanıtı döndür
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }


        }



        [HttpPut("update")]
        public async Task<IActionResult> Update( [FromBody] OutageClosureDto outageClosureDto)
        {
            var resultNotification = await _notificationService.Update(outageClosureDto);

            if (!resultNotification.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "NotificationService hata: " + resultNotification.ErrorMessage
                };
                return BadRequest(baseResult);
            }


            var resultOutage = await _outageService.Update(outageClosureDto);


            if (!resultOutage.IsSuccess)
            {
                var baseResult = new BaseResult<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "NotificationService hata: " + resultNotification.ErrorMessage
                };
                return BadRequest(baseResult);
            }
            else
            {
                return BadRequest(); // Buraya bir gözat
            }
           
        }



    }
}
