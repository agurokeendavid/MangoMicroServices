﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    
    public async Task<ResponseDto?> GetCouponAsync(string couponCode)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponAPIBase}/api/coupon/GetByCode/{couponCode}"
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponAPIBase}/api/coupon"
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponAPIBase}/api/coupon/{id}"
        });
    }

    public async Task<ResponseDto?> CreateCouponAsync(CouponDto dto)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = $"{SD.CouponAPIBase}/api/coupon",
            Data = dto
        });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto dto)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.PUT,
            Url = $"{SD.CouponAPIBase}/api/coupon",
            Data = dto
        });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.DELETE,
            Url = $"{SD.CouponAPIBase}/api/coupon/{id}"
        });
    }
}