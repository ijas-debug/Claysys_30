using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using FinalProject.Models;

namespace FinalProject.Repository
{
    public class User_Repository
    {

        String conString = ConfigurationManager.ConnectionStrings["Myconnection"].ToString();

        //Get All Users
        public List<UserClass> GetAllUsers()
        {
            List<UserClass> UserList = new List<UserClass>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_GetAllUsers";
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataTable dataUsers = new DataTable();

                connection.Open();
                sqlData.Fill(dataUsers);
                connection.Close();

                foreach (DataRow datarow in dataUsers.Rows)
                {
                    UserList.Add(new UserClass
                    {
                        ID = Convert.ToInt32(datarow["ID"]),
                        FirstName = datarow["FirstName"].ToString(),
                        LastName = datarow["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(datarow["DateOfBirth"]),
                        Gender = datarow["Gender"].ToString(),
                        PhoneNumber = datarow["PhoneNumber"].ToString(),
                        EmailAddress = datarow["EmailAddress"].ToString(),
                        Address = datarow["Address"].ToString(),
                        Country = datarow["Country"].ToString(),
                        State = datarow["State"].ToString(),
                        City = datarow["City"].ToString(),
                        Postcode = datarow["Postcode"].ToString(),
                        PassportNumber = datarow["PassportNumber"].ToString(),
                        AdharNumber = datarow["AdharNumber"].ToString(),
                        Username = datarow["Username"].ToString(),
                    });
                }
            }
            return UserList;
        }


        //Get All Userss by ID
        public List<UserClass> GetUsersByID(int ID)
        {
            List<UserClass> UserList = new List<UserClass>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_GetUsersByID";
                command.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();

                foreach (DataRow datarow in dtProducts.Rows)
                {
                    UserList.Add(new UserClass
                    {
                        ID = Convert.ToInt32(datarow["ID"]),
                        FirstName = datarow["FirstName"].ToString(),
                        LastName = datarow["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(datarow["DateOfBirth"]),
                        Gender = datarow["Gender"].ToString(),
                        PhoneNumber = datarow["PhoneNumber"].ToString(),
                        EmailAddress = datarow["EmailAddress"].ToString(),
                        Address = datarow["Address"].ToString(),
                        Country = datarow["Country"].ToString(),
                        State = datarow["State"].ToString(),
                        City = datarow["City"].ToString(),
                        Postcode = datarow["Postcode"].ToString(),
                        PassportNumber = datarow["PassportNumber"].ToString(),
                        AdharNumber = datarow["AdharNumber"].ToString(),
                        Username = datarow["Username"].ToString(),
                    });
                }
            }
            return UserList;
        }



        //Delete Product
        public string DeleteUser(int userid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPD_DeleteUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID",userid);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }
            return result;
        }

       

        // Check Status by EmailId and PassportNumber
        public string CheckStatus(string emailId, string passportNumber)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_CheckVisaApplication";
                command.Parameters.AddWithValue("@EmailId", emailId);
                command.Parameters.AddWithValue("@PassportNumber", passportNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                string status = string.Empty;

                if (reader.Read())
                {
                    status = reader["Status"].ToString();
                }

                reader.Close();
                connection.Close();

                return status;
            }
        }

       

    }
}