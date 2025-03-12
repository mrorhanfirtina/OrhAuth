namespace LVCore.LVApp.Shared.Dtos.AuthDtos
{
    // Şifre sıfırlama DTO'su
    public class ResetPasswordDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
