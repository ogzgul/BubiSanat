using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubiSanat.Models
{
    public class Category
    {

        public short Id { get; set; }

        [Required(ErrorMessage ="Bu alan gereklidir")]
        [DisplayName("İsim")]
        [Column(TypeName ="nchar(50)")]
        [StringLength(50,ErrorMessage ="En fazla 50 karakter")]
        public string Name { get; set; }
        
        [Column(TypeName ="nchar(100)")]
        [DisplayName("Bilgi")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        public string? Info { get; set; }


        [DisplayName("Ana Kategori (opsiyonel)")]
        public short? TopCategoryId { get; set; }

        [DisplayName("Ana Kategori")]
        [ForeignKey(nameof(TopCategoryId))]
        public TopCategory? TopCategory { get; set; }

    }
}
