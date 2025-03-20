using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlayId { get; set; }

        [ForeignKey("PlayId")]
        public Play Play { get; set; }

        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

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

        [Required]        
        public int ReviewerId { get; set; }
    }
}
