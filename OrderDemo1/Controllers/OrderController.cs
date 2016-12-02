using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderDemo1.Models;

namespace OrderDemo1.Controllers
{
    [Authorize] //必须登录会员才能使用订单结账功能
    public class OrderController : Controller
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

        public ActionResult Complete()
        {
            return View();
        }
        //显示完成订单的窗体页面
        [HttpPost]
        public ActionResult Complete(OrderHeader form)
        {
            var member = db.Members.Where(p=>p.Nickname==User.Identity.Name).FirstOrDefault();
            if (member == null) return RedirectToAction("Index", "Home");
            if (this.Carts.Count == 0) return RedirectToAction("Index", "Cart");
            //将订单信息与购物车信息写入数据库
            OrderHeader oh = new OrderHeader()
            {
                Member = member,
                Memo = form.Memo,
                BuyOn = DateTime.Now,
                OrderDetailItems = new List<OrderDetail>()
            };

            int total_price = 0;
            foreach (var item in this.Carts)
            {
                var product = db.Products.Find(item.Product.Id);
                if (product == null) return RedirectToAction("Index", "Cart");
                total_price += item.Product.Price * item.Amount;
                oh.OrderDetailItems.Add(new OrderDetail()
                {
                    Product = product,
                    Price = product.Price,
                    Amount = item.Amount
                });
            }
            oh.TotalPrice = total_price;
            db.Orders.Add(oh);
            db.SaveChanges();

            //订单完成后必须清空购物车信息
            this.Carts.Clear();

            //TODO:将订单信息与购物车信息写入数据库
            //TODO:订单完成后必须清空现有购物车信息
            //订单完成后回到网站首页
            return RedirectToAction("Index", "Home");
        }
    }
}