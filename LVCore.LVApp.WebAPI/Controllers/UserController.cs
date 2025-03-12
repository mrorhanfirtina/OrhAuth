using LVCore.LVApp.BusinessService.Services.AuthServices;
using LVCore.LVApp.Shared.Dtos.AuthDtos;
using LVCore.LVApp.Shared.Dtos.RequestDtos;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace LVCore.LVApp.WebAPI.Controllers
{
    [RoutePrefix("api/v2/User")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IAuthBusinessService _authService;

        public UserController(IAuthBusinessService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await _authService.GetUserAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Route("email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetUserByEmail(string email)
        {
            var user = await _authService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateUser([FromBody] Shared.Dtos.AuthDtos.UserUpdateDto model)
        {
            var success = await _authService.UpdateUserAsync(model);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Kullanıcı güncellenemedi" });
        }

        [HttpPut]
        [Route("extended")]
        public async Task<IHttpActionResult> UpdateExtendedUser([FromBody] ExtendedUserUpdateDto model)
        {
            var success = await _authService.UpdateExtendedUserFieldsAsync(model);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Kullanıcı güncellenemedi" });
        }

        [HttpGet]
        [Route("extended/{id}")]
        public async Task<IHttpActionResult> GetExtendedUser(int id)
        {
            var user = await _authService.GetExtendedUserInfoAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        [Route("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> SetUserStatus(int id, [FromBody] SetUserStatusDto model)
        {
            var success = await _authService.SetUserStatusAsync(id, model.IsActive);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Kullanıcı durumu güncellenemedi" });
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetUsers([FromUri] int page = 1, [FromUri] int pageSize = 10)
        {
            var users = await _authService.GetUsersAsync(page, pageSize);
            var count = await _authService.GetUserCountAsync();

            return Ok(new
            {
                data = users,
                totalCount = count,
                pageCount = (int)Math.Ceiling(count / (double)pageSize)
            });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var success = await _authService.DeleteUserAsync(id);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Kullanıcı silinemedi" });
        }
    }
}