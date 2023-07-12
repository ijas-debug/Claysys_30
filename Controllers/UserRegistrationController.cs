using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using FinalProject.Repository;

namespace FinalProject.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: NewReg
        public ActionResult Index()
        {
            return View();
        }



        // Post: NewReg
        [HttpPost]
        public ActionResult Index(UserClass uc, HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            SqlCommand sqlCommand = new SqlCommand("sp_InsertUserReg", sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@FirstName", uc.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", uc.LastName);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", uc.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Gender", uc.Gender);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", uc.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", uc.EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Address", uc.Address);
            sqlCommand.Parameters.AddWithValue("@Country", uc.Country);
            sqlCommand.Parameters.AddWithValue("@State", uc.State);
            sqlCommand.Parameters.AddWithValue("@City", uc.City);
            sqlCommand.Parameters.AddWithValue("@Postcode", uc.Postcode);
            sqlCommand.Parameters.AddWithValue("@PassportNumber", uc.PassportNumber);
            sqlCommand.Parameters.AddWithValue("@AdharNumber", uc.AdharNumber);
            sqlCommand.Parameters.AddWithValue("@Username", uc.Username);
            sqlCommand.Parameters.AddWithValue("@Password", uc.Password);
            

            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("/User-Images/"), filename);
                file.SaveAs(imgpath);
                sqlCommand.Parameters.AddWithValue("@Photo", "/User-Images/" + filename);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Photo", DBNull.Value);
            }
            sqlconn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlconn.Close();

            ViewData["Message"] = "User Record " + uc.FirstName + " Is Saved Successfully!";
            return View();


        }


        


    }
}