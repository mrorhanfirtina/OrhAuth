using LVCore.LVApp.BusinessService.Services.AuthServices;
using LVCore.LVApp.Shared.Dtos.RequestDtos;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace LVCore.LVApp.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/v2/Auth")]
    public class RoleController : ApiController
    {
        private readonly IAuthBusinessService _authService;

        public RoleController(IAuthBusinessService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("role")]
        public async Task<IHttpActionResult> AddRole([FromBody] RoleDto model)
        {
            var role = await _authService.AddRoleAsync(model.RoleName);
            return Ok(role);
        }

        [HttpDelete]
        [Route("role/{id}")]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            var success = await _authService.DeleteRoleAsync(id);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Rol silinemedi" });
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IHttpActionResult> GetRoles()
        {
            var roles = await _authService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Route("claim")]
        public async Task<IHttpActionResult> AddOperationClaim([FromBody] OperationClaimDto model)
        {
            var claim = await _authService.AddOperationClaimAsync(model.ClaimName);
            return Ok(claim);
        }

        [HttpPost]
        [Route("user-role")]
        public async Task<IHttpActionResult> AssignRoleToUser([FromBody] UserRoleDto model)
        {
            var success = await _authService.AssignRoleToUserAsync(model.UserId, model.RoleId);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Rol atanamadı" });
        }

        [HttpDelete]
        [Route("user-role")]
        public async Task<IHttpActionResult> RemoveRoleFromUser([FromBody] UserRoleDto model)
        {
            var success = await _authService.RemoveRoleFromUserAsync(model.UserId, model.RoleId);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Rol kaldırılamadı" });
        }

        [HttpGet]
        [Route("user/{userId}/roles")]
        public async Task<IHttpActionResult> GetUserRoles(int userId)
        {
            var roles = await _authService.GetUserRolesAsync(userId);
            return Ok(roles);
        }

        [HttpPost]
        [Route("role-claim")]
        public async Task<IHttpActionResult> AssignClaimToRole([FromBody] RoleClaimDto model)
        {
            var success = await _authService.AssignClaimToRoleAsync(model.RoleId, model.OperationClaimId);
            if (success)
                return Ok(new { success = true });

            return Content(HttpStatusCode.BadRequest, new { success = false, message = "Yetki atanamadı" });
        }

        [HttpGet]
        [Route("has-permission")]
        public async Task<IHttpActionResult> HasPermission([FromUri] int userId, [FromUri] string operationName)
        {
            var hasPermission = await _authService.HasPermissionAsync(userId, operationName);
            return Ok(new { hasPermission });
        }
    }
}
