using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubiSanat.Models
{
    public class Comment
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("İçerik")]
        [Column(TypeName = "nchar(256)")]
        [StringLength(256, ErrorMessage = "En fazla 256 karakter")]
        public string Content { get; set; }


        [Column(TypeName = "smalldatetime")]
        public DateTime TimeStamp { get; set; }

        [DisplayName("Post")]
        public long PostId { get; set; }

        [DisplayName("Post")]
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

    }
}
