using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CW_COMP_1471.Models
{
    public class Pricing
    {
        [Key]
        public int Pricingid { get; set; }
        public string Band { get; set; }
        public double Price { get; set; }
        public int PlayId { get; set; }
        public SelectList? plays { get; set; }
    }
}
