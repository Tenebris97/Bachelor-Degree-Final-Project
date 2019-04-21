using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class CommentListViewModel
    {
        [Key]
        public int CommentId { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "نظر")]
        public string CommentText { get; set; }
    }
}
