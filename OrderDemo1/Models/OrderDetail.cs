using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace OrderDemo1.Models
{
    [DisplayName("订单明细")]
    [DisplayColumn("Name")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("订单主文件")]
        [Required]
        public virtual OrderHeader OrderHeader { get; set; }

        [DisplayName("订购商品")]
        [Required]
        public Product Product { get; set; }

        [DisplayName("售价")]
        [Required(ErrorMessage ="请输入售价")]
        [Description("由于售价可能会经常异动，因此必须保留购买当下的售价")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DisplayName("点菜数量")]
        [Required]
        public int Amount { get; set; }
    }
}