using System.ComponentModel.DataAnnotations;

namespace iettproje
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Eposta Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string EPosta { get; set; }


        [Required(ErrorMessage = "Sifre Zorunludur")]
        [Compare("Sifre", ErrorMessage = "Şifre Bulunamadı!")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}

