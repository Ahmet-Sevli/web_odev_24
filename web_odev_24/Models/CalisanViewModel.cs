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



       
       
        [Display(Name = "Telefon No")]
        public string calisan_telefon { get; set; }


        
       
        [Display(Name = "Email")]
        public string calisan_email { get; set; }



        [Display(Name = "Yapabildiği İşlem")]
        public string islem_ad { get; set; }
    }
}
