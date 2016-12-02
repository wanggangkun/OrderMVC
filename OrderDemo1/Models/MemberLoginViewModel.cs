using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderDemo1.Models
{
    public class MemberLoginViewModel
    {
        [DisplayName("昵称")]
        [Required(ErrorMessage ="请输入{0}")]
        public string nickName { get; set; }

        [DisplayName("密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="请输入{0}")]
        public string password { get; set; }
    }
}