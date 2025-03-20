using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("pricingband")]
    public class PricingBand
    {
        [Key]
        [Column("pricingid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricingId { get; set; }

        [Required]
        [Column("band")]
        [StringLength(50)]  
        public string Band { get; set; }

        [Required]
        [Column("price", TypeName = "numeric(10,2)")]
        public double Price { get; set; }

        [Column("playid")]
        public int? PlayId { get; set; }

        [ForeignKey("PlayId")]
        public Play? Play { get; set; }

        [NotMapped]
        public SelectList? Plays { get; set; }
    }
}
