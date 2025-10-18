using System.ComponentModel.DataAnnotations;

namespace iettproje.Models
{
    public class RegisterVM
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }

        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        public int SicilNo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EPosta { get; set; }
        public string TelefonNumarasi { get; set; }

       
    }
}

