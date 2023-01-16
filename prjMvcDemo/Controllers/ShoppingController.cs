using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            dbDemoEntities db =new dbDemoEntities();
            var datas = from t in db.tProduct select t;
            return View(datas);
        }
        public ActionResult AddToCart(int? id)
        {
            ViewBag.fId=id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm)  //需要品項ID與輸入的數量
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct p = db.tProduct.FirstOrDefault(t => t.fId == vm.txtFId);
            if(p == null) 
            {
                return RedirectToAction("List");
            }
            tShoppingCart item = new tShoppingCart();
            item.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            item.fPrice = p.fPrice;
            item.fProductId = vm.txtFId;
            item.fCustomerId = 1;
            item.fCount=vm.txtCount;
            db.tShoppingCart.Add(item);
            db.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}