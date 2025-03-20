using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("plays")]
    public class Play
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("playid")]
        public int PlayId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("genre")]
        public string Genre { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("dateandtime")]
        public DateTime Dateandtime { get; set; }
        [Column("playtype")]
        public string PlayType { get; set; }

        [Column("imageurl")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IEnumerable<Review> Reviews { get; set; }

        [NotMapped]
        public IEnumerable<PricingBand> PricingBands { get; set; }

    }

}
