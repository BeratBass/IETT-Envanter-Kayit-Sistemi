using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace iettproje.Models
{
    public class UrunModelleri 
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Marka { get; set; }

        [Required]
        public string Modeli { get; set; }

        public string Kategorisi { get; set; }

        [Required]
        public bool Durum { get; set; }



    }
}
