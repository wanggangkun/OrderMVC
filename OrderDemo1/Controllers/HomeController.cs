using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderDemo1.Models;
using System.Drawing;

namespace OrderDemo1.Controllers
{
    public class HomeController : Controller
    {
        OrderContext db = new OrderContext();
        // 首页
        public ActionResult Index()
        {
            var data = db.ProductCategories.ToList();        
            return View(data);
        }

        //商品列表
        public ActionResult ProductList(int id)
        {
            var productCategory = db.ProductCategories.Find(id);
            if(productCategory!=null)
            {
                var data = productCategory.Products.ToList();              
                db.SaveChanges();
                data = productCategory.Products.ToList();
                return View(data);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

    }
}