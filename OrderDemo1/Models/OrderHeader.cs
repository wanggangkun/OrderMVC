using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderDemo1.Models
{
    [DisplayName("订单主文件")]
    [DisplayColumn("DisplayName")]
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("订购会员")]
        [Required]
        public virtual Member Member { get; set; }

        [DisplayName("订单金额")]
        [Required]
        [DataType(DataType.Currency)]
        [Description("由于订单会受商品优惠折扣等方式异动，因而必须保留购买当下的订单金额")]
        public int TotalPrice { get; set; }

        [DisplayName("订单备注")]
        [DataType(DataType.MultilineText)]
        public string Memo { get; set; }

        [DisplayName("点菜时间")]
        public DateTime BuyOn { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get { return this.Member.Nickname + "于" + this.BuyOn + "点的菜"; }
        }

        public virtual ICollection<OrderDetail> OrderDetailItems { get; set; }
    }
}