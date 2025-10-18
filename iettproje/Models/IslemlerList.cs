using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iettproje.Models
{
    public class IslemlerList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Sicil { get; set; }


        [Required]
        public DateTime Tarih { get; set; }

        [Required]
        public string Marka { get; set; }   
        [Required]
        public string Modeli { get; set; }

        [Required]
        public int Adet { get; set; }

        [Required]
        public bool Durum { get; set; }



      

    }
}
