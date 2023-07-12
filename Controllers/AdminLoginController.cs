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
using System.Web.Security;

using FinalProject.Repository;

namespace FinalProject.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(Admin lc)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand("sp_AdminLogin", sqlconn);

            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddWithValue("@EmailAddress", lc.EmailAddress);
            sqlcomm.Parameters.AddWithValue("@Password", lc.Password);

            sqlconn.Open();
            SqlDataReader sqr = sqlcomm.ExecuteReader();

            if (sqr.Read())
            {
                FormsAuthentication.SetAuthCookie(lc.EmailAddress, true);
                Session["emailid"] = lc.EmailAddress.ToString();
               return RedirectToAction("AdminHome", "AdminLogin");
               

            }
            else
            {
                ViewData["message"] = "Username & Password are wrong !";
            }
            sqlconn.Close();
            return View();
        }



        public ActionResult AdminWelcome()
        {
            string loggedInEmail = (string)Session["emailid"];
            string connectionString = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SPS_GetAdminDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailAddress", loggedInEmail);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Admin admin = new Admin();
                if (reader.Read())
                {
                    string photo = reader["Photo"].ToString();
                    ViewData["Img"] = photo;
                    TempData["Oldimg"] = photo;

                    admin.FirstName = reader["FirstName"].ToString();
                    admin.LastName = reader["LastName"].ToString();
                    admin.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    admin.Gender = reader["Gender"].ToString();
                    admin.PhoneNumber = reader["PhoneNumber"].ToString();
                    admin.EmailAddress = reader["EmailAddress"].ToString();
                    admin.Address = reader["Address"].ToString();
                    admin.Country = reader["Country"].ToString();
                    admin.State = reader["State"].ToString();
                    admin.City = reader["City"].ToString();
                    admin.Postcode = reader["Postcode"].ToString();
                    admin.PassportNumber = reader["PassportNumber"].ToString();
                    admin.AdharNumber = reader["AdharNumber"].ToString();
                    admin.Username = reader["Username"].ToString();
                    admin.Password = reader["Password"].ToString();
                }

                connection.Close();
                return View(admin);
            }
        }

        //to view user 

        User_Repository usersDAL = new User_Repository();
        // GET: Product
        public ActionResult UsersView()
        {
            var UsersList = usersDAL.GetAllUsers();

            if (UsersList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently Users not available in the Database";
            }
            return View(UsersList);
        }


        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = usersDAL.GetUsersByID(id).FirstOrDefault();

                if (user == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                    return RedirectToAction("UsersView");
                }
                return View(user);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = usersDAL.GetUsersByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "User not available with ID " + id.ToString();
                    return RedirectToAction("UsersView");
                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id, FormCollection collection)
        {

            try
            {
                string result = usersDAL.DeleteUser(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("UsersView");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        public ActionResult AdminHome()
        {
            ViewBag.Message = "Admin Home page";

            return View();
        }


        //to view user 

        VisaApplication_Repository visadata = new VisaApplication_Repository();
        // GET: Product
        public ActionResult ApplicantsView()
        {
            
            var ApplicantList = visadata.GetAllApplicants();
           

                if (ApplicantList.Count == 0)
            {
               
                TempData["InfoMessage"] = "Currently Applicants not available in the Database";
            }
            foreach (var applicant in ApplicantList)
            {
                string photoFileName = applicant.Photo;
                applicant.PhotoUrl = string.IsNullOrEmpty(photoFileName)
                    ? null
                    : Url.Content("~/Visa-Images/" + photoFileName);
            }


            return View(ApplicantList);

        }


        public ActionResult ChangeStatus(int id, string status)
        {
            // Perform necessary logic to update the status of the visa application with the provided ID

            
            if (status == "approve")
            {
                // Update the status to "Approved" in the database
                visadata.UpdateStatus(id, "Approved");
                TempData["SuccessMessage"] = "Application has been approved successfully.";
            }
            else if (status == "reject")
            {
                // Update the status to "Rejected" in the database
                visadata.UpdateStatus(id, "Rejected");
                TempData["SuccessMessage"] = "Application has been rejected.";
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid status specified.";
            }

            // Redirect back to the ApplicantsView page
            return RedirectToAction("ApplicantsView");
        }

        // GET: Product/Details/5
        public ActionResult ApplicantDetails(int id)
        {
            try
            {
                var applicant = visadata.GetApplicantsByID(id).FirstOrDefault();

                if (applicant == null)
                {
                    TempData["InfoMessage"] = "Applicants not available with ID " + id.ToString();
                    return RedirectToAction("ApplicantsView");
                }
                return View(applicant);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }



        

        // GET: Product/Delete/5
        public ActionResult DeleteApplicant(int id)
        {
            try
            {
                
                var applicant = visadata.GetApplicantsByID(id).FirstOrDefault();

                if (applicant == null)
                {
                    TempData["InfoMessage"] = "Applicant is not available with ID " + id.ToString();
                    return RedirectToAction("ApplicantsView");
                }
                return View(applicant);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("DeleteApplicant")]
        public ActionResult ApplicantDeleteConfirmation(int id, FormCollection collection)
        {

            try
            {
                string result = visadata.DeleteApplicant(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("ApplicantsView");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public ActionResult UpdateVisaApplication(int id)
        {

            var applicant = visadata.GetApplicantsByID(id).FirstOrDefault();

            if (applicant == null)
            {
                TempData["InfoMessage"] = "Applicant not available with ID " + id.ToString();
                return RedirectToAction("ApplicantDetails");
            }

            return View(applicant);

        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult UpdateVisaApplication(VisaApplication applicant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = visadata.UpdateVisaApplication(applicant);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Visa Application details updated successfully!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update Visa Application.";
                    }
                }

                return RedirectToAction("ApplicantDetails", new { id = applicant.ApplicationID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the Visa Application.";
                return RedirectToAction("ApplicantDetails", new { id = applicant.ApplicationID });
            }
        }





    }
}