using System.ComponentModel.DataAnnotations;

namespace web_odev_24.Models
{
    public class CalisanViewModel
    {
        public int calisanID { get; set; }

        [Display(Name ="Çalışanın Adı")]
       public string calisan_ad { get; set; }



        [Display(Name = "Çalışanın Soyadı")]
        public string calisan_soyad { get; set; }

        

        [Display(Name = "Çalışanın İşteki Tecrübesi")]
        public string calisan_tecrube { get; set; }



        [Display(Name = "Yapabildiği İşlem")]
        public string islem_ad { get; set; }
    }
}
