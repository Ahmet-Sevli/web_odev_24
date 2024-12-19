using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_odev_24.Models
{
    public class RandevuViewModel
    {
        public int MusteriID { get; set; }
        public string MusteriAd { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public TimeSpan RandevuSaati { get; set; }

        public int? IslemID { get; set; } // Nullable, çünkü başlangıçta seçilmemiş olabilir
        public int? CalisanID { get; set; } // Nullable, çünkü çalışan başlangıçta seçilmemiş olabilir

        public List<SelectListItem> Islemler { get; set; } // İşlem listesi
        public List<SelectListItem> Calisanlar { get; set; } // Çalışanlar listesi

    }
}
