using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iettproje.Models
{
    [Table("Kullanicilar")]
    public class Kullanicilar 
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Isim { get; set; } = null!;

        [Required]
        public string Sifre { get; set; } = null!;

        [Required]
        public string Soyisim { get; set; } = null!;

        [Required]
        public int SicilNo { get; set; }

        [Required]
        public string EPosta { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string TelefonNumarasi { get; set; }     

        public bool Aktiflik {  get; set; } = false;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } 
    }


}
