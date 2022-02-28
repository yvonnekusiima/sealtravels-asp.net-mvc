using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SealTravels.Models
{
    public class HotelsModel
    {
        public int Id { get; set; }
        public string Hotel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Travelers { get; set; }
        public string Rooms { get; set; }
        public string RoomType { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Contact { get; set; }
        public string Amount { get; set; }
    }

    public class HotelsLayer
    {
        //Hotel Booking Records
        public IEnumerable<HotelsModel> hotelsmodel
        {           
            get
            {
                List<HotelsModel> hotelsModel = new List<HotelsModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from HOTELBOOKING", conn);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        HotelsModel hotelModel = new HotelsModel();
                        hotelModel.Id = Convert.ToInt32(rdr["Id"]);
                        hotelModel.Hotel = rdr["Hotel"].ToString();
                        hotelModel.Name = rdr["Name"].ToString();
                        hotelModel.Email = rdr["Email"].ToString();
                        hotelModel.Travelers = rdr["Travelers"].ToString();
                        hotelModel.Rooms = rdr["Rooms"].ToString();
                        hotelModel.RoomType = rdr["RoomType"].ToString();
                        hotelModel.CheckIn = rdr["CheckIn"].ToString();
                        hotelModel.CheckOut = rdr["CheckOut"].ToString();
                        hotelModel.Contact = rdr["Contact"].ToString();
                        hotelModel.Amount = rdr["Amount"].ToString();

                        hotelsModel.Add(hotelModel);
                    }
                }
                return hotelsModel;
            }
        }

        //Hotel Booking
        public void hotelbooking(HotelsModel hotelBooking)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spHOTELBOOKING", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramHotel = new SqlParameter();
                paramHotel.ParameterName = "@Hotel";
                paramHotel.Value = hotelBooking.Hotel;
                cmd.Parameters.Add(paramHotel);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = hotelBooking.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = hotelBooking.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramTravelers = new SqlParameter();
                paramTravelers.ParameterName = "@Travelers";
                paramTravelers.Value = hotelBooking.Travelers;
                cmd.Parameters.Add(paramTravelers);

                SqlParameter paramRooms = new SqlParameter();
                paramRooms.ParameterName = "@Rooms";
                paramRooms.Value = hotelBooking.Rooms;
                cmd.Parameters.Add(paramRooms);

                SqlParameter paramRoomType = new SqlParameter();
                paramRoomType.ParameterName = "@RoomType";
                paramRoomType.Value = hotelBooking.RoomType;
                cmd.Parameters.Add(paramRoomType);

                SqlParameter paramCheckIn = new SqlParameter();
                paramCheckIn.ParameterName = "@CheckIn";
                paramCheckIn.Value = hotelBooking.CheckIn;
                cmd.Parameters.Add(paramCheckIn);

                SqlParameter paramCheckOut = new SqlParameter();
                paramCheckOut.ParameterName = "@CheckOut";
                paramCheckOut.Value = hotelBooking.CheckOut;
                cmd.Parameters.Add(paramCheckOut);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = hotelBooking.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = hotelBooking.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Edit Hotel Booking Records
        public void edithotelbookingrecords(HotelsModel editHotelBooking)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spUpdateHOTELBOOKING", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = editHotelBooking.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramHotel = new SqlParameter();
                paramHotel.ParameterName = "@Hotel";
                paramHotel.Value = editHotelBooking.Hotel;
                cmd.Parameters.Add(paramHotel);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = editHotelBooking.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = editHotelBooking.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramTravelers = new SqlParameter();
                paramTravelers.ParameterName = "@Travelers";
                paramTravelers.Value = editHotelBooking.Travelers;
                cmd.Parameters.Add(paramTravelers);

                SqlParameter paramRooms = new SqlParameter();
                paramRooms.ParameterName = "@Rooms";
                paramRooms.Value = editHotelBooking.Rooms;
                cmd.Parameters.Add(paramRooms);

                SqlParameter paramRoomType = new SqlParameter();
                paramRoomType.ParameterName = "@RoomType";
                paramRoomType.Value = editHotelBooking.RoomType;
                cmd.Parameters.Add(paramRoomType);

                SqlParameter paramCheckIn = new SqlParameter();
                paramCheckIn.ParameterName = "@CheckIn";
                paramCheckIn.Value = editHotelBooking.CheckIn;
                cmd.Parameters.Add(paramCheckIn);

                SqlParameter paramCheckOut = new SqlParameter();
                paramCheckOut.ParameterName = "@CheckOut";
                paramCheckOut.Value = editHotelBooking.CheckOut;
                cmd.Parameters.Add(paramCheckOut);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = editHotelBooking.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = editHotelBooking.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        //Remove Hotel Booking Record
        public void removehotelbookingrecord(int id)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spDeleteHOTELBOOKING", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

    }
}
