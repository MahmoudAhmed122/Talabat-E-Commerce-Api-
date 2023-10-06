using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        public UserManager<ApplicationUser> _userManager { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }
        public ITokenService _tokenService { get; }
        public IMapper Mapper { get; }

        public AccountsController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager , 
            ITokenService tokenService , 
            IMapper mapper 
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            Mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            }) ;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {

            if (CheckIfEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse()
                {
                   Errors= new List<string> { "Email is already Exist!" }

                });
            var user = new ApplicationUser()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            });

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser() {

            var email = User.FindFirstValue(ClaimTypes.Email); // it will find email of user who send the request

            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =await  _tokenService.CreateTokenAsync(user, _userManager)
            }); ;
        

        
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress() {

            var user =await  _userManager.GetUserWithAddressAsync(User);

            var addressDto = Mapper.Map<Address, AddressDto>(user.Address);

            return Ok(addressDto);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {

            var user = await _userManager.GetUserWithAddressAsync(User);
            var address = Mapper.Map<AddressDto, Address>(addressDto);
            address.Id = user.Address.Id;
            user.Address = address;
            var result =await  _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(addressDto);
        }

        [HttpGet("EmailExistence")]
        public async Task<ActionResult<bool>> CheckIfEmailExist(string email) {

            return _userManager.FindByEmailAsync(email) is not null;
        }
        
    }
}
