using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUygulamaProje1.Models
{
    public class Anket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AnketAdi { get; set; }
        public string Soru1 { get; set;}

        [Required]
        public string Soru2 { get; set;}

        [Required]
        public string Soru3 { get; set; }

        [ValidateNever]
        public int AnketTuruId { get; set; }
        [ForeignKey("AnketTuruId")]
		
        [ValidateNever]
		public AnketTuru AnketTuru { get; set;}

		[ValidateNever]
		public string ResimUrl { get; set; }
        
    }
}
