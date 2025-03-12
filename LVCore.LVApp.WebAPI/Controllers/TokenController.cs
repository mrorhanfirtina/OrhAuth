using LVCore.LVApp.BusinessService.Services.AuthServices;
using LVCore.LVApp.Shared.Dtos.RequestDtos;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace LVCore.LVApp.WebAPI.Controllers
{
    [RoutePrefix("api/v2/Token")]
    public class TokenController : ApiController
    {
        private readonly IAuthBusinessService _authService;

        public TokenController(IAuthBusinessService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            try
            {
                var token = await _authService.RefreshTokenAsync(model.RefreshToken);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("validate")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ValidateToken([FromBody] ValidateTokenDto model)
        {
            var isValid = await _authService.ValidateTokenAsync(model.Token);
            return Ok(new { valid = isValid });
        }

        [HttpPost]
        [Route("revoke")]
        [Authorize]
        public async Task<IHttpActionResult> RevokeToken([FromBody] RevokeTokenDto model)
        {
            var success = await _authService.RevokeTokenAsync(model.RefreshToken);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Geçersiz token" });
        }
    }
}