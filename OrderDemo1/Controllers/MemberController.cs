using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderDemo1.Models;
using System.Web.Security;

namespace OrderDemo1.Controllers
{
    public class MemberController : Controller
    {
        OrderContext db = new OrderContext();

        //会员注册页面
        public ActionResult Register()
        {
            return View();
        }

        //密码哈希所需的Salt随机数
        private string pwSalt = "hbY7j470hV4h03G7E49kloB05dR56hJKDq8z";
        //写入会员信息
        [HttpPost]
        public ActionResult Register(Member member)
        {
            //检查会员是否已存在
            var chk_member = db.Members.Where(p=>p.Nickname==member.Nickname).FirstOrDefault();
            if(chk_member!=null)
            {
                ModelState.AddModelError("NickName", "您输入的昵称已经被人注册");
            }
            if (ModelState.IsValid)
            {
                //将密码加“盐”之后进行哈希运算以提高会员密码的安全性
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                db.Members.Add(member);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //显示会员登入页面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //进行会员登陆
        [HttpPost]
        public ActionResult Login(string nickname,string password,string returnUrl)
        {
            if(ValidataUser(nickname,password))
            {
                FormsAuthentication.SetAuthCookie(nickname, false);

                if(String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");                  
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "您输入的账号或密码错误");
            return View();
        }

        private bool ValidataUser(string nickname,string password)
        {
            var hash_pw= FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password, "SHA1");
            var member = (from p in db.Members
                          where (p.Password == hash_pw && p.Nickname==nickname)
                          select p).FirstOrDefault();

            //如果member对象不为null则代表会员的昵称/密码输入正确
            return (member != null);
        }

        //会员注销
        public ActionResult Logout()
        {
            //清除窗体验证的Cookies
            FormsAuthentication.SignOut();

            //清除所有曾经写入过的Session信息
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}