using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje1.Models
{
    public class AnketTuru
    {
        [Key] // primary key
        public int Id { get; set; }
        [Required(ErrorMessage="Anket Türü Adı Boş Bırakılamaz!")] //not null
        [MaxLength(25)]
        [DisplayName("Anket Türü Adı")]
        public string Ad { get; set; }
    }
}
