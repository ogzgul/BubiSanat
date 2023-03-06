using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BubiSanat.Models
{
    public class TopCategory
    {

        public short Id { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("İsim")]
        [Column(TypeName = "nchar(50)")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter")]
        public string Name { get; set; }

        public List<Category>? Categories { get; set; }

    }
}
