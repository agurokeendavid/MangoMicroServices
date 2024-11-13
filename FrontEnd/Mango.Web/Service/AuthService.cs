using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    
    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = $"{SD.CouponAPIBase}/api/auth/login",
            Data = loginRequestDto
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = $"{SD.CouponAPIBase}/api/auth/register",
            Data = registrationRequestDto
        });
    }

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendASync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = $"{SD.CouponAPIBase}/api/auth/assign_role",
            Data = registrationRequestDto
        });
    }
}