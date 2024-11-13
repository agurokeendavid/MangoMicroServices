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
        else
        {
            TempData["error"] = couponResponse?.Message;
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
    public async Task<IActionResult> Create(CouponDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _couponService.CreateCouponAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int couponId)
    {
        var couponResponse = await _couponService.GetCouponByIdAsync(couponId);

        if (couponResponse != null && couponResponse.IsSuccess)
        {
            CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = couponResponse?.Message;
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(CouponDto couponDto)
    {
        var couponResponse = await _couponService.DeleteCouponAsync(couponDto.CouponId);

        if (couponResponse != null && couponResponse.IsSuccess)
        {
            TempData["success"] = couponResponse.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = couponResponse?.Message;
        }

        return View(couponDto);
    }
}