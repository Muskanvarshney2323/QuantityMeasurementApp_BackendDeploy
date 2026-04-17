using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppBusinessLayer.Interfaces;
using QuantityMeasurementAppModel.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace QuantityMeasurementAppAPI.Controllers
{
    /// <summary>
    /// APIs for user authentication such as register, login, google login, and fetch current user.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="dto">User registration details.</param>
        /// <returns>Returns registration result.</returns>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register user", Description = "Creates a new user account using name, email, and password.")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = _service.Register(dto);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Login an existing user.
        /// </summary>
        /// <param name="dto">User login credentials.</param>
        /// <returns>Returns login result.</returns>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login user", Description = "Authenticates a user using email and password.")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var result = _service.Login(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Login or register user using Google account.
        /// </summary>
        /// <param name="dto">Google user details.</param>
        /// <returns>Returns google login result.</returns>
        [HttpPost("google-login")]
        [SwaggerOperation(Summary = "Google login", Description = "Logs in a user with Google account details. If user does not exist, a new account is created.")]
        public IActionResult GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            var result = _service.GoogleLogin(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Get current user details by email.
        /// </summary>
        /// <param name="email">User email address.</param>
        /// <returns>Returns current user details.</returns>
        [HttpGet("me")]
        [SwaggerOperation(Summary = "Get current user", Description = "Returns user details for the provided email.")]
        public IActionResult GetCurrentUser([FromQuery] string email)
        {
            var result = _service.GetCurrentUser(email);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}