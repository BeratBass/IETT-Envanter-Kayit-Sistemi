using Microsoft.AspNetCore.Mvc;

namespace iettproje.Models
{
    public class UrunlerVM
    {
        public int Id { get; set; }

        public string Urunler { get; set; }

        public bool Durum { get; set; }
        public List<UrunKategorileri> Items { get; set; } = new List<UrunKategorileri>(); // Başlatma
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalItems / PageSize) : 0;
    }


}
