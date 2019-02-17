using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Npgsql;
namespace chatApp.Models
{
    public class DataLayer
    {
        NpgsqlConnection con = new NpgsqlConnection();
        public DataLayer()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        }
        public userModel login(string email, string password)
        {
            userModel user = new userModel();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string sql = "select * from tbluser where email='" + email + "' and password='" + password + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                user.userid = Convert.ToInt32(row["userid"].ToString());
                user.email = row["email"].ToString();
                user.mobile = row["mobile"].ToString();
                user.password = row["password"].ToString();
            }
            return user;
        }
        public List<userModel> getusers(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<userModel> userlist = new List<userModel>();
            string sql = "select * from tbluser where userid<>" + id;
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                userModel user = new userModel();
                user.userid = Convert.ToInt32(row["userid"].ToString());
                user.email = row["email"].ToString();
                user.mobile = row["mobile"].ToString();
                user.password = row["password"].ToString();
                user.dob = row["dob"].ToString();
                userlist.Add(user);
            }
            return userlist;
        }
    }
}