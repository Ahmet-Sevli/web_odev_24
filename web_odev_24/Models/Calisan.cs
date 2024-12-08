﻿using System.Collections.Generic;
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

        public int islemID { get; set; }
        public ICollection<Islem>? Islemler { get; set; }


    }
}
