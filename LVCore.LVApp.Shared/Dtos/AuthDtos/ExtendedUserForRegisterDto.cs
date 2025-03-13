using OrhAuth.Models.Dtos;

namespace LVCore.LVApp.Shared.Dtos.AuthDtos
{
    public class ExtendedUserForRegisterDto : UserForRegisterDto
    {
        public int LVUserId { get; set; }
        public string LVUserLogin { get; set; }
        public string LVPasswordText { get; set; } // Şifreyi düz metin olarak alıp byte dizisine çeviririz
    }
}
