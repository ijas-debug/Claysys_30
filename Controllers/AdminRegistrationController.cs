using FinalProject.Models;
using FinalProject.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace FinalProject.Controllers
{
    public class AdminRegistrationController : Controller
    {
        // GET: AdminReg
        public ActionResult Index()
        {
            return View();
        }

        // Post: AdminReg
        [HttpPost]
        public ActionResult Index(Admin admin, HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand sqlCommand = new SqlCommand("sp_InsertAdminReg", sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@FirstName", admin.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", admin.LastName);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", admin.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Gender", admin.Gender);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", admin .EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Address", admin.Address);
            sqlCommand.Parameters.AddWithValue("@Country", admin.Country);
            sqlCommand.Parameters.AddWithValue("@State", admin.State);
            sqlCommand.Parameters.AddWithValue("@City", admin.City);
            sqlCommand.Parameters.AddWithValue("@Postcode", admin.Postcode);
            sqlCommand.Parameters.AddWithValue("@PassportNumber", admin.PassportNumber);
            sqlCommand.Parameters.AddWithValue("@AdharNumber", admin.AdharNumber);
            sqlCommand.Parameters.AddWithValue("@Username", admin.Username);
            sqlCommand.Parameters.AddWithValue("@Password", admin.Password);


            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/Admin-Images/"), filename);
                file.SaveAs(imgpath);
                sqlCommand.Parameters.AddWithValue("@Photo", "/Admin-Images/" + filename);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Photo", DBNull.Value);
            }
            sqlconn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlconn.Close();

            ViewData["Message"] = "User Record " + admin.FirstName + " Is Saved Successfully!";
            return View();


        }


        
    }
}