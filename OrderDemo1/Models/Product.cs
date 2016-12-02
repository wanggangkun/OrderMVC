using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace OrderDemo1.Models
{
    [DisplayName("商品信息")]
    [DisplayColumn("Name")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("类别")]
        [Required]
        public virtual ProductCategory ProductCategory { get; set; }

        [DisplayName("名称")]
        [Required(ErrorMessage ="请输入名称")]
        [MaxLength(60,ErrorMessage ="名称不可超过60个字")]
        public string Name { get; set; }

        [DisplayName("售价")]
        [Required(ErrorMessage ="请输入售价")]
        public int Price { get; set; }

        [Required(ErrorMessage ="请插入图片")]
        public string Picture { get; set; }
    }
}