using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _response;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();
                _response.IsSuccess = true;
                _response.Result = objList;
            }
            catch (Exception exception)
            {
                _response.Message = exception.Message;
            }

            return _response;
        }

        [HttpGet("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon? objRecord = _dbContext.Coupons.FirstOrDefault(i => i.CouponId == id);
                _response.IsSuccess = true;
                _response.Result = objRecord;
            }
            catch (Exception exception)
            {
                _response.Message = exception.Message;
            }

            return _response;
        }
    }
}
