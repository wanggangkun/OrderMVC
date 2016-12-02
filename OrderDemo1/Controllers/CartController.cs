using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderDemo1.Models;

namespace OrderDemo1.Controllers
{
    public class CartController : Controller
    {
        OrderContext db = new OrderContext();

        List<Cart> Carts
        {
            get
            {
                if(Session["Carts"]==null)
                {
                    Session["Carts"] = new List<Cart>();
                }
                return (Session["Carts"] as List<Cart>);
            }
            set { Session["Carts"] = value; }
        }

        
      

        //添加菜到购物车，如果没有传入Amount参数则默认购买数量为1
        [HttpPost]
        //因为知道要通过Ajax调用这个Action，所以可以先标示[HttpPost]属性
        public ActionResult AddToCart(int ProductId, int Amount = 1)
        {
            var product = db.Products.Find(ProductId);
            //验证产品是否存在
            if (product == null)
                return HttpNotFound();

            var exitingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if(exitingCart!=null)
            {
                exitingCart.Amount += 1;
            }
            else
            {
                this.Carts.Add(new Cart() { Product = product, Amount = 0 });
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Created);
        }

        //显示当前的购物车项目
        public ActionResult Index()
        {
            return View(this.Carts);
        }

        //移除购物车项目
        [HttpPost]
        public ActionResult Remove(int ProductId)
        {
            var exitingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if(exitingCart!=null)
            {
                this.Carts.Remove(exitingCart);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }



        //更新购物车中特定项目的数量
       [HttpPost]
        public ActionResult UpdateAmount(List<Cart> Carts)
        {
            foreach(var item in Carts)
            {
                var exitingCart = this.Carts.FirstOrDefault(p => p.Product.Id == item.Product.Id);
                if(exitingCart!=null)
                {
                    exitingCart.Amount = item.Amount;
                }
            }
            return RedirectToAction("Index", "Cart");
        }

    }
}