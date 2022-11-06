using System;
using Microsoft.AspNetCore.Mvc;
using SegurosUdla.WebApi.Dto;
using SegurosUdla.WebApi.Exceptions.Auth;
using SegurosUdla.WebApi.Interfaces;

namespace SegurosUdla.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                var token = await authService.Authenticate(model.username, model.password);
                return Ok(token);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidPasswordException)
            {
                return BadRequest();
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                await authService.Register(model.Username!, model.Password!);
                return Ok();
            }
            catch (RegisterException ex)
            {
                return BadRequest(ex.errors);
            }
        }
    }
}

