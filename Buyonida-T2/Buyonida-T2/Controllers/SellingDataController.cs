using Buyonida.Business.DTOs.DataDtos;
using Buyonida.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buyonida_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellingDataController : ControllerBase
    {
        private readonly ISellingTypeService _sellingTypeService;
        private readonly ISellingPointService _sellingPointService;
        public SellingDataController(ISellingTypeService sellingTypeService, ISellingPointService sellingPointService)
        {
            _sellingTypeService = sellingTypeService;
            _sellingPointService = sellingPointService;
        }

        [HttpPost("selling-points")]
        public async Task<IActionResult> SellingPoint(SellingPointDto dto)
        {
            try
            {
                await _sellingPointService.CreateSellingPoint(dto);
            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK
            });
        }


        [HttpPost("what-you-sell")]
        public async Task<IActionResult> SellingType(SellingTypeDto dto)
        {
            try
            {
                await _sellingTypeService.CreateSellingType(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK
            });
        }
    }
}
