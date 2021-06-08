using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.DATA.Entities
{
    public class Article
    {

        public int ID { get; set; }

        public int UserID { get; set; }


        [Display(Name = "Makale Başlığı")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş geçilemez.")]
        [MinLength(2, ErrorMessage = "Lütfen en az 3 karakter giriniz.")]
        [MaxLength(150, ErrorMessage = "Lütfen 150 karakteri aşan başlık girmeyiniz.")]
        public string Title { get; set; }

        [Display(Name = "Makale Açıklaması")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(250, ErrorMessage = "Lütfen 80 karakteri Aşmayınız.")]
        public string Description { get; set; }

        [Display(Name = "Makale İçeriği")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu alan boş geçilemez")]
        public string Content { get; set; }

        [Display(Name = "Makale Resmi")]
        public string Picture { get; set; }

        public short ReadingTime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int HitRate { get; set; }

        public User User { get; set; }

        public virtual ICollection<ArticlesTopics> ArticlesTopics { get; set; }






    }
}
