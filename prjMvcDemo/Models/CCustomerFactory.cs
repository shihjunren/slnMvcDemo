using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace prjMvcDemo.Models
{
    public class CCustomerFactory
    {
        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            List<CCustomer> list= queryBySql(sql, paras);
            if(list.Count==0)
                return null;
            return list[0];
        }
        public List<CCustomer> queryAll()
        {
            string sql = "SELECT * FROM tCustomer";
            return queryBySql( sql,null);
        }
        private List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if(paras!=null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CCustomer x = new CCustomer()
                {
                    fEmail = reader["fEmail"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                    fName = reader["fName"].ToString(),
                    fId = (int)reader["fId"],
                    fAddress = reader["fAddress"].ToString(),
                    fPassword = reader["fPassword"].ToString()
                };
                list.Add(x);
            }
            con.Close();
            return list;
        }

        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " UPDATE tCustomer SET ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " fName=@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " fPhone=@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " fEmail=@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " fAddress=@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " fPassword=@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE fId=@K_FID";
            paras.Add(new SqlParameter("K_FID", (object)p.fId));
            executeSql(sql, paras); 
        }

        public void delete(int id)
        {
            string sql = "DELETE FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)id));
            executeSql(sql, paras);
        }

        private void executeSql(string sql, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
        }

        public void create(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer(";
            if (!string.IsNullOrEmpty(p.fName))
                sql += "fName,";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone,";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail,";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword,";
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")VALUES(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            executeSql(sql, paras);
        }

        public List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE fName LIKE @K_KEYWORD";
            sql+= " OR fPhone LIKE @K_KEYWORD";
            sql += " OR fEmail LIKE @K_KEYWORD";
            sql += " OR fAddress LIKE @K_KEYWORD";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_KEYWORD", "%"+(object)keyword+"%"));
            return queryBySql(sql, paras);
        }
    }
}