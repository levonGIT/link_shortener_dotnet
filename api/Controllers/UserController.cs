using api.Data;
using api.Dtos.Link;
using api.Dtos.User;
using api.Handlers;
using api.Mappers;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserRepository _userRepo;
        private readonly JwtService _jwtService;

        public UserController(ApplicationDBContext context, UserRepository userRepo, JwtService jwtService)
        {
            _context = context;
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("/api/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
        {
            var result = await _jwtService.Authenticate(userDto);
            if (result == null) 
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/user/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            var hashedPassword = PasswordHashHandler.HashPassword(userDto.Password);
            var userModel = userDto.ToUserFromCreateDTO();
            if(await _userRepo.GetByLoginAsync(userModel.Login)!=null)
            {
                return BadRequest("User with that login already exists");
            }
            userModel.Password = hashedPassword;
            await _userRepo.CreateAsync(userModel);
            return Created();
        }
    }
}
