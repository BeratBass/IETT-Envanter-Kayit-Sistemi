using System.ComponentModel.DataAnnotations;


namespace iettproje.Models
{
    public class IslemlerListVM
    {
        public int Id { get; set; }

        public int Sicil { get; set; }

        public string Marka { get; set; }
        public string Modeli { get; set; }

        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public bool Durum { get; set; }
        public List<IslemlerList> Items { get; set; } = new List<IslemlerList>(); // Başlatma
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((decimal)TotalItems / PageSize) : 0;
        public List<UrunModelleri> Modeller { get; set; } = new List<UrunModelleri>();



    }
}
