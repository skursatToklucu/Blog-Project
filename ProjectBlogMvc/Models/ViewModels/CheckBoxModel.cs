using ProjectBlogMvc.Models.DATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models.ViewModels
{
    public class CheckBoxTopicModel : Topic
    {
        public bool IsChecked { get; set; }
    }

    public class CheckBoxTopicList
    {
        public List<CheckBoxTopicModel> TopicItems { get; set; }
    }
}

