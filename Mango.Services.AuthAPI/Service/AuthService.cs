using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;


    public AuthService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> RegisterAsync(RegistrationRequestDto dto)
    {
        ApplicationUser user = new()
        {
            UserName = dto.Email,
            Email = dto.Email,
            NormalizedUserName = dto.Email.ToUpper(),
            NormalizedEmail = dto.Email.ToUpper(),
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // var userToReturn = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == dto.Email);
                //
                // UserDto userDto = new()
                // {
                //     Email = userToReturn.Email,
                //     ID = userToReturn.Id,
                //     Name = userToReturn.Name,
                //     PhoneNumber = userToReturn.PhoneNumber
                // };

                return "";
            }

            return result.Errors.FirstOrDefault().Description;
        }
        catch (Exception exception)
        {
            
        }

        return "Error Encountered";
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u =>
            u.UserName.ToLower() == dto.UserName.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (user is null || !isValid)
        {
            return new LoginResponseDto() { User = null, Token = string.Empty };
        }

        // if user was found, generate jwt token
        string token = _jwtTokenGenerator.GenerateToken(user);
        UserDto userDto = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new()
        {
            User = userDto,
            Token = token
        };

        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u =>
            u.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                // create role if it does not exist
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;
    }
}