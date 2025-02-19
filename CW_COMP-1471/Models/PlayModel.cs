using System.ComponentModel.DataAnnotations;

namespace CW_COMP_1471.Models
{
    public class Play
    {
        [Key]
        public int Playid { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime Dateandtime { get; set; }
        public string Playtype { get; set; }
        public int Createdby { get; set; }
        public User? CreatedbyUser { get; set; }
        public DateTime Createddate { get; set; }
        public int PackageId { get; set; }
        public Package? Package { get; set; }
    }
}
