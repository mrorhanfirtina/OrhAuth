namespace LVCore.LVApp.Shared.Dtos.AuthDtos
{
    public class ExtendedUserUpdateDto : UserUpdateDto
    {
        public int LVUserId { get; set; }
        public string LVUserLogin { get; set; }
        public byte[] LVPasswordText { get; set; } // Güncelleme sırasında LVPassword değişecekse
    }
}
