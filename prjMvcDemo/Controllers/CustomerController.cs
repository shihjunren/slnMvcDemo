using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CustomerController : Controller
    {
        [HttpPost]
        public ActionResult Edit(CCustomer p)
        {
            (new CCustomerFactory()).update(p);
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = (new CCustomerFactory()).queryById((int)id);
            return View(x);
        }
        // GET: Customer
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                (new CCustomerFactory()).delete((int)id);
            }
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            List<CCustomer> datas = null;
            string keyword = Request.Form["txtKeyword"];
            if(string.IsNullOrEmpty(keyword))
                datas=(new CCustomerFactory()).queryAll();
            else
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);
        }
        public ActionResult Create()
        { 
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }
    }
}