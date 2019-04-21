using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class CommentAddEdit
    {
        [Key]
        public int CommentId { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا نظر خود را وارد کنید")]
        public string CommentText { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }
    }
}
