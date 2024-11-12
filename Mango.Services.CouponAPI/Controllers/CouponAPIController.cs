using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CouponAPIController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this._mapper = mapper;
            _response = new ResponseDto();

        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
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

                if (objRecord is null)
                    _response.IsSuccess = false;

                _response.Result = _mapper.Map<CouponDto>(objRecord);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.Message = exception.Message;
            }

            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon? objRecord = _dbContext.Coupons.FirstOrDefault(i => i.CouponCode.ToLower() == code.ToLower());
                if (objRecord is null)
                {
                    _response.IsSuccess = false;
                }

                _response.Result = _mapper.Map<CouponDto>(objRecord);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.Message = exception.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto dto)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(dto);
                _dbContext.Coupons.Add(couponObj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.Message = exception.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto dto)
        {
            try
            {
                var couponObj = _mapper.Map<Coupon>(dto);
                _dbContext.Coupons.Update(couponObj);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(couponObj);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.Message = exception.Message;
            }

            return _response;
        }

        [HttpDelete("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var couponObj = _dbContext.Coupons.First(i => i.CouponId == id);
                _dbContext.Coupons.Remove(couponObj);
                _dbContext.SaveChanges();
                _response.Message = $"Coupon ID: {couponObj.CouponId} has been successfully deleted.";
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.Message = exception.Message;
            }

            return _response;
        }
    }
}
