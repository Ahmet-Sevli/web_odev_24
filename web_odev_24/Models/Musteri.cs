using System.ComponentModel.DataAnnotations;


namespace web_odev_24.Models
{
    public class Musteri
    {
        public int musteriID { get; set; }

        [Required(ErrorMessage = "Müşteri adı boş bırakılamaz.")]
        [MaxLength(100)]
        [Display(Name = "Müşteri adı")]
        public string musteri_ad { get; set; }


        [Required(ErrorMessage = "Müşteri soyadı boş bırakılamaz.")]
        [MaxLength(100)]
        [Display(Name = "Müşteri soyadı")]
        public string musteri_soyad { get; set; }


        [Required(ErrorMessage = "Müşteri telefonu boş bırakılamaz.")]
        [Phone(ErrorMessage = "Lütfen geçerli bir telefon numarası girin.")]
        [Display(Name = "Telefon No")]
        public string musteri_telefon { get; set; }


        [Required(ErrorMessage = "Email boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email girin.")]
        [Display(Name = "Email")]
        public string musteri_email { get; set; }


        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Şifre en az 3, en fazla 10 karakter olmalıdır.")]
        [Display(Name = "Sifre")]
        public string musteri_sifre { get; set; }


    }
}
