using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<CouponDto?> list = new();
        
        var couponResponse = await _couponService.GetAllCouponsAsync();

        if (couponResponse != null && couponResponse.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(couponResponse.Result));
        }
        
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CouponDto dto)
    {
        if (ModelState.IsValid)
        {
            
        }
        return View();
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var coupon = await _couponService.GetCouponByIdAsync(id);

        if (coupon is null)
            return NotFound();

        return View(coupon.Result);
    }
}