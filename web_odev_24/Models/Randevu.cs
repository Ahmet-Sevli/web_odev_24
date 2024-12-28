using System.ComponentModel.DataAnnotations;

namespace web_odev_24.Models
{
    public class Randevu
    {
        public int randevuID { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Randevu Tarihi")]
        public DateTime randevu_tarihi { get; set; }

        [Required(ErrorMessage = "Randevu saati zorunludur.")]
        [DataType(DataType.Time)]
        [Display(Name = "Randevu Saati")]
        public TimeSpan randevu_saati { get; set; }

        [Required(ErrorMessage = "Bir işlem seçilmelidir.")]
        public int islemID { get; set; }
        public Islem? Islem { get; set; }

        [Required(ErrorMessage = "Bir çalışan seçilmelidir.")]
        public int calisanID { get; set; }
        public Calisan? Calisan { get; set; }

        [Required(ErrorMessage = "Bir müşteri seçilmelidir.")]
        public int musteriID { get; set; }
        public Musteri? Musteri { get; set; }

        [Display(Name = "Admin Onayı")]
        public bool onay_durumu { get; set; } = false; // Varsayılan olarak onay bekliyor
    }


}

