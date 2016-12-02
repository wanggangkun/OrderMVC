using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace OrderDemo1.Models
{
    [DisplayName("会员信息")]
    [DisplayColumn("Name")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("昵称")]
        [Required(ErrorMessage ="请输入昵称")]
        [MaxLength(10,ErrorMessage ="昵称请勿超过10个字")]
        public string Nickname { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage ="请输入密码")]
        [MaxLength(40,ErrorMessage ="密码不得超过40个字符")]
        [MinLength(6,ErrorMessage ="密码不得少于6个字符")]
        [Description("密码将以SHA1进行哈希运算，通过SHA1哈希运算后的结果转为HEX表示法的字符串长度皆为40个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}