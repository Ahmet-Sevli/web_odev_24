using System.ComponentModel.DataAnnotations;


namespace web_odev_24.Models
{
    public class Islem
    {
        public int islemID { get; set; }

        [Required(ErrorMessage = "İşlem adı boş bırakılamaz.")]
        [Display(Name = "işlem adı")]
        public string islem_ad { get; set; }



        [Required(ErrorMessage = "İşlem ücreti boş bırakılamaz.")]
        [Display(Name = "İşlem ücreti")]
        [Range(0, double.MaxValue, ErrorMessage = "Ücret negatif olamaz.")]
        [DataType(DataType.Currency)]
        public int islem_ucret { get; set; }

        [Display(Name = "Çalışan adı")]
        public int calisanID { get; set; }
        [Display(Name = "Çalışan adı")]
        public Calisan calisan { get; set; }




    }
}
