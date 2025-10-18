using Microsoft.AspNetCore.Mvc;

namespace iettproje.Models
{
    public class UrunModelleriVM 
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Modeli { get; set; }
        public List<UrunModelleri> Items { get; set; } = new List<UrunModelleri>(); // Başlatma
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalItems / PageSize) : 0;
        public  List<UrunKategorileri> Kategoriler { get; set; } = new List<UrunKategorileri>();

        public string Kategorisi { get; set; }
        public bool Durum { get; set; }



    }
}
