using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace iettproje.Models
{
    public class UrunKategorileri 
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Urunler { get; set; }

        [Required]
        public bool Durum { get; set; }



    }
}
