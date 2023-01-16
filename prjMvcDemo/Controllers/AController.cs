using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public ActionResult demoUpload() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult demoUpload(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\Users\iSpan\Desktop\MVC\MVCData\slnMvcDemo\prjMvcDemo\Images\test.jpg");
            return View();
        }
        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {                
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                double r = b * b - 4 * a * c;
                r=Math.Sqrt(r);

                ViewBag.ANS = ((-b+r)/(2*a)).ToString("0.0#")+
                    " Or X="+ ((-b - r) / (2 * a)).ToString();
            }
            return View();
        }

        public string testingQuery()
        {                        
            return "目前客戶數："+ (new CCustomerFactory()).queryAll().Count.ToString();
        }
        public string testingUPdate(int? id)
        {
            if (id == null)
                return "請指定 Id";
            CCustomer x = new CCustomer();
            x.fId = (int)id;
            //x.fName = "Jason";
            x.fPhone = "0955444666";
            //x.fEmail = "kk@gmail.com";
            x.fAddress = "PingTung";
            x.fPassword = "4321";
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "請指定 Id";           
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer();
            x.fName = "Jason";
            //x.fPhone = "0966541254";
            x.fEmail = "jason@gmail.com";
           // x.fAddress = "Tainan";
            x.fPassword = "1234";
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }

        public ActionResult bindingById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    x = new CCustomer()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fId = (int)reader["fId"]
                    };                    
                }
                con.Close();
            }
            return View(x);
        }
        // GET: A
        public ActionResult showById(int? id)
        {
            if (id != null) 
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fId = (int)reader["fId"]
                    };
                    ViewBag.KK = x;
                }
                con.Close();
            } 
            return View();
        }
        public string queryById(int? id)
        {
            if(id==null)
                return "請指定要查詢的Id";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM tCustomer WHERE fId="+id.ToString(),
                con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無該筆資料";
            if(reader.Read())
                s=reader["fName"].ToString() + " <br/> " + reader["fPhone"].ToString();
            con.Close();
            return s;
        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\QNote\01.jpg");
            Response.End();
            return "";
        }
        public string demoParaneter(int? id)
        {
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }
        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }
        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }

        [NonAction]



        public string lotto()
        {
            CLottoGen x = new CLottoGen();
            return x.getNumbers();
        }


    }
}