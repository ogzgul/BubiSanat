using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubiSanat.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("Ad Soyad*")]
        [Column(TypeName = "nchar(100)")]
        [StringLength(100, MinimumLength =5, ErrorMessage = "En az 5, en fazla 100 karakter")]
        public string Name { get; set; }

        public bool Deleted { get; set; }


        //NotMapped = veri tabanında tutmamak anlamına gelir.
        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("Şifre*")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "En az 8, en fazla 100 karakter")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("Şifre(Tekrar)*")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "En az 8, en fazla 100 karakter")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string ConfirmPassWord { get; set; }

        [NotMapped]
        [DisplayName("Üyelik sözleşmesini kabul ediyorum.")]
        public bool Agreed { get; set; }
    }
}
