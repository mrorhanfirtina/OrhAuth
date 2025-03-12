namespace LVCore.LVApp.Shared.Dtos.RequestDtos
{
    public class PasswordResetDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
