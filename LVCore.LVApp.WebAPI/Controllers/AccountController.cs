using LVCore.LVApp.BusinessService.Services.AuthServices;
using LVCore.LVApp.Shared.Dtos.AuthDtos;
using LVCore.LVApp.Shared.Dtos.RequestDtos;
using OrhAuth.Models.Dtos;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace LVCore.LVApp.WebAPI.Controllers
{
    [RoutePrefix("api/v2/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthBusinessService _authService;

        public AccountController(IAuthBusinessService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody] UserForRegisterDto model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                    return Content(HttpStatusCode.BadRequest, "Email ve şifre gereklidir");

                var user = await _authService.RegisterAsync(model);
                return Ok(new { success = true, userId = user.Id });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("register-extended")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RegisterExtended([FromBody] ExtendedUserForRegisterDto model)
        {
            try
            {
                var user = await _authService.RegisterExtendedAsync(model);
                return Ok(new { success = true, userId = user.Id });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromBody] UserForLoginDto model)
        {
            try
            {
                var token = await _authService.LoginAsync(model);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("activate/{activationCode}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ActivateAccount(string activationCode)
        {
            var success = await _authService.ActivateAccountAsync(activationCode);
            if (success)
                return Ok(new { success = true, message = "Hesap başarıyla aktifleştirildi" });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Geçersiz aktivasyon kodu" });
        }

        [HttpPost]
        [Route("password/reset-request")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RequestPasswordReset([FromBody] Shared.Dtos.AuthDtos.PasswordResetRequestDto model)
        {
            var success = await _authService.RequestPasswordResetAsync(model.Email);
            if (success)
                return Ok(new { success = true, message = "Şifre sıfırlama talimatları e-posta adresinize gönderildi" });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Kullanıcı bulunamadı" });
        }

        [HttpPost]
        [Route("password/reset")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ResetPassword([FromBody] PasswordResetDto model)
        {
            var success = await _authService.ResetPasswordAsync(model.ResetToken, model.NewPassword);
            if (success)
                return Ok(new { success = true, message = "Şifreniz başarıyla sıfırlandı" });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Geçersiz veya süresi dolmuş şifre sıfırlama kodu" });
        }

        [HttpPost]
        [Route("password/change")]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword([FromBody] Shared.Dtos.AuthDtos.ChangePasswordDto model)
        {
            // User ID'yi token'dan al
            int userId = GetUserIdFromToken();

            var success = await _authService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (success)
                return Ok(new { success = true, message = "Şifreniz başarıyla değiştirildi" });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Geçersiz şifre" });
        }

        private int GetUserIdFromToken()
        {
            // Token'dan User ID'yi çıkarma (.NET Framework için)
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userIdClaim = identity?.FindFirst("UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}