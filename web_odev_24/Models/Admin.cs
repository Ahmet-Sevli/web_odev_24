using System.ComponentModel.DataAnnotations;


namespace web_odev_24.Models
{
    public class Admin
    {
        public int adminID { get; set; }

        [Required(ErrorMessage = "Email adresi boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [Display(Name = "Email")]
        public string admin_email { get; set; }


        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [Display(Name = "Sifre")]
        [DataType(DataType.Password)]
        
        public string admin_sifre { get; set; }

    }
}
