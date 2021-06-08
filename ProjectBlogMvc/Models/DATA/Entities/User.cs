using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [MaxLength(20, ErrorMessage = "Lütfen Kullanıcı Adınızı 20 karakterden fazla girmeyiniz.")]
        [MinLength(2, ErrorMessage = "Lütfen en az 3 karakter giriniz.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string UserName { get; set; }

        [Display(Name = "Ad")]
        [MaxLength(20, ErrorMessage = "Lütfen Kullanıcı Adınızı 20 karakterden fazla girmeyiniz.")]
        [MinLength(2, ErrorMessage = "Lütfen en az 3 karakter giriniz.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(20, ErrorMessage = "Lütfen Kullanıcı Adınızı 20 karakterden fazla girmeyiniz.")]
        [MinLength(2, ErrorMessage = "Lütfen en az 3 karakter giriniz.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "E-mail Adresi")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş bırkılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Açıklama")]
        [MaxLength(250, ErrorMessage = "Lütfen 250 karakteri geçmeyiniz.")]
        public string Bio { get; set; }

        [Display(Name = "URL Adresi")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string URL { get; set; }

        [Display(Name = "Profil Resmi")]
        public string ProfilPicture { get; set; }

        public bool IsActive { get; set; }

        public int HitRate { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<UsersTopics> UsersTopics { get; set; }


    }
}
