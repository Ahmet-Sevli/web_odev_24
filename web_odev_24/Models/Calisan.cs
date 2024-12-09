using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web_odev_24.Models
{
    public class Calisan
    {
        public int calisanID { get; set; }

        [Required(ErrorMessage = "Çalışan adı boş bırakılamaz.")]
        [MaxLength(100)]
        [Display(Name = "Çalışan adı")]
        public string calisan_ad { get; set; }


        [Required(ErrorMessage = "Çalışan soyadı boş bırakılamaz.")]
        [MaxLength(100)]
        [Display(Name = "Çalışan soyadı")]
        public string calisan_soyad { get; set; }

        [Required(ErrorMessage = "Çalışan tecrübesi boş bırakılamaz.")]
        [MaxLength(100)]
        [Display(Name = "Çalışan tecrübesi")]
        public string calisan_tecrube { get; set; }

        [Display(Name = "İşlem Becerisi ")]
        public int islemID { get; set; }
        
        public Islem islem { get; set; }

        

    }
}
