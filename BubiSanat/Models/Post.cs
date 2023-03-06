using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubiSanat.Models
{
    public class Post
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("Başlık")]
        [Column(TypeName = "nchar(50)")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter")]
        public string Title { get; set; }

        [DisplayName("Tarih")]
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Kategori")]
        public short CategoryId { get; set; }

        [NotMapped]
        [DisplayName("Görsel")]
        public IFormFile? FormImage { get; set; }

        [Column(TypeName ="image")]
        public byte[]? Image { get; set; }


        [DisplayName("Kategori")]
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DisplayName("İçerik")]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool Deleted  { get; set; }

        public string AuthorId { get; set; }


        [DisplayName("Önceki İçerik")]
        public long? PreviousPostId { get; set; }

        [DisplayName("Sonraki İçerik")]
        public long? NextPostId { get; set; }

        public long Likes { get; set; }
        public long DisplayCount { get; set; }


        [DisplayName("Etiketler")]
        [Column(TypeName = "nchar(100)")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        public string? Tags { get; set; }


        [DisplayName("Önceki İçerik")]
        [ForeignKey("PreviousPostId")]
        public Post? PreviousPost { get; set; }

        [DisplayName("Sonraki İçerik")]
        [ForeignKey("NextPostId")]
        public Post? NextPost { get; set; }
        

        [DisplayName("Yazar")]
        [ForeignKey("AuthorId")]
        public ApplicationUser? Author { get; set; }

    }
}
