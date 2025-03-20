using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("reviews")]
    public class Review
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("playid")]
        public int PlayId { get; set; }

        [ForeignKey("PlayId")]
        public Play Play { get; set; }

        [Required]
        [StringLength(100)]
        [Column("reviewername")]
        public string ReviewerName { get; set; }

        [Required]
        [Column("reviewerid")]
        public int ReviewerId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        [Column("rating")]
        public int Rating { get; set; }

        [StringLength(1000)]
        [Column("comment")]
        public string Comment { get; set; }

        [Column("reviewdate")]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }

    public class PostReviewModel
    {
        [Required]
        public int PlayId { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        public int? ReviewerId { get; set; }

        [Required]
        public string PaymentRefNumber { get; set; }
    }
}
