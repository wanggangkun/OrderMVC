using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderDemo1.Models
{
    [DisplayName("类别")]
    [DisplayColumn("Name")]
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("类别名称")]
        [Required(ErrorMessage ="请输入类别名称")]
        [MaxLength(20,ErrorMessage ="类别名称不超过20个字")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}