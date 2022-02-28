using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SealTravels.Models
{
    public class AdministratorModel
    {
        public string AdministratorId { get; set; }
        public string Password { get; set; }
        public string Captcha { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string DateAdded { get; set; }    
    }

    public class AdministratorLayer
    {
        //User Records
        public IEnumerable<AdministratorModel> administratormodel
        {  
            get
            {
                List<AdministratorModel> administrator = new List<AdministratorModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from USERS", conn);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        AdministratorModel administratorModel = new AdministratorModel();
                        administratorModel.Id = Convert.ToInt32(rdr["Id"]);
                        administratorModel.Email = rdr["Email"].ToString();
                        administratorModel.Username = rdr["Username"].ToString();
                        administratorModel.Password = rdr["Password"].ToString();
                        administratorModel.DateAdded = rdr["DateAdded"].ToString();

                        administrator.Add(administratorModel);
                    }
                }
                return administrator;
            }
        }
        //Edit Users
        public void edituser(AdministratorModel editUser)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUSERS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = editUser.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = editUser.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@Username";
                paramUserID.Value = editUser.Username;
                cmd.Parameters.Add(paramUserID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        //Remove User
        public void removeuser(int id)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUSERS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramID = new SqlParameter();
                paramID.ParameterName = "@Id";
                paramID.Value = id;
                cmd.Parameters.Add(paramID);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
