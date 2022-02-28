using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SealTravels.Models
{
    public class FlightsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string TripType { get; set; }
        public string LeavingFrom { get; set; }
        public string GoingTo { get; set; }
        public string Departing { get; set; }
        public string Returning { get; set; }
        public string Adults { get; set; }
        public string Children { get; set; }
        public string FlightType { get; set; }
        public string Airline { get; set; }
        public string TravelClass { get; set; }
        public string Amount { get; set; }

        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryDate { get; set; }

        public string PassengerName { get; set; }
        public string Status { get; set; }
        public string Flight { get; set; }
        public string Seat { get; set; }
        public string CheckIn { get; set; }
        public string Gate { get; set; }
        public string Date { get; set; }
        public string Barcode { get; set; }
    }

    public class FlightsLayer
    {
        //Flight Booking Records - Roundtrip
        public IEnumerable<FlightsModel> RoundtripFlightModel
        {
            get
            {
                List<FlightsModel> flightsModel = new List<FlightsModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from ROUNDTRIPFLIGHTS", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FlightsModel flightModel = new FlightsModel();
                        flightModel.Id = Convert.ToInt32(sdr["Id"]);
                        flightModel.Name = sdr["Name"].ToString();
                        flightModel.Email = sdr["Email"].ToString();
                        flightModel.Contact = sdr["Contact"].ToString();
                        flightModel.TripType = sdr["TripType"].ToString();
                        flightModel.LeavingFrom = sdr["LeavingFrom"].ToString();
                        flightModel.GoingTo = sdr["GoingTo"].ToString();
                        flightModel.Departing = sdr["Departing"].ToString();
                        flightModel.Returning = sdr["Returning"].ToString();
                        flightModel.Adults = sdr["Adults"].ToString();
                        flightModel.Children = sdr["Children"].ToString();
                        flightModel.FlightType = sdr["FlightType"].ToString();
                        flightModel.Airline = sdr["Airline"].ToString();
                        flightModel.TravelClass = sdr["TravelClass"].ToString();
                        flightModel.Amount = sdr["Amount"].ToString();

                        flightsModel.Add(flightModel);
                    }
                }
                return flightsModel;
            }
        }

        //Flight Booking Records - Oneway
        public IEnumerable<FlightsModel> OnewayFlightModel
        {
            get
            {
                List<FlightsModel> flightsModel = new List<FlightsModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from ONEWAYFLIGHTS", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FlightsModel flightModel = new FlightsModel();
                        flightModel.Id = Convert.ToInt32(sdr["Id"]);
                        flightModel.Name = sdr["Name"].ToString();
                        flightModel.Email = sdr["Email"].ToString();
                        flightModel.Contact = sdr["Contact"].ToString();
                        flightModel.TripType = sdr["TripType"].ToString();
                        flightModel.LeavingFrom = sdr["LeavingFrom"].ToString();
                        flightModel.GoingTo = sdr["GoingTo"].ToString();
                        flightModel.Departing = sdr["Departing"].ToString();
                        flightModel.Adults = sdr["Adults"].ToString();
                        flightModel.Children = sdr["Children"].ToString();
                        flightModel.FlightType = sdr["FlightType"].ToString();
                        flightModel.Airline = sdr["Airline"].ToString();
                        flightModel.TravelClass = sdr["TravelClass"].ToString();
                        flightModel.Amount = sdr["Amount"].ToString();

                        flightsModel.Add(flightModel);
                    }
                }
                return flightsModel;
            }
        }

        //Flight Payment Records
        public IEnumerable<FlightsModel> FlightPaymentModel
        {
            get
            {
                List<FlightsModel> flightspaymentModel = new List<FlightsModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from FLIGHTPAYMENTS", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FlightsModel flightpaymentModel = new FlightsModel();
                        flightpaymentModel.Id = Convert.ToInt32(sdr["Id"]);
                        flightpaymentModel.Name = sdr["Name"].ToString();
                        flightpaymentModel.Email = sdr["Email"].ToString();
                        flightpaymentModel.PaymentMethod = sdr["PaymentMethod"].ToString();
                        flightpaymentModel.CardNumber = sdr["CardNumber"].ToString();
                        flightpaymentModel.CVV = sdr["CVV"].ToString();
                        flightpaymentModel.ExpiryDate = sdr["ExpiryDate"].ToString();
                        flightpaymentModel.Amount = sdr["Amount"].ToString();

                        flightspaymentModel.Add(flightpaymentModel);
                    }
                }
                return flightspaymentModel;
            }
        }

        //Flight Ticket Records
        public IEnumerable<FlightsModel> FlightTicketModel
        {
            get
            {
                List<FlightsModel> flightsticketModel = new List<FlightsModel>();

                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("select * from FLIGHTTICKETS", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FlightsModel flightticketModel = new FlightsModel();
                        flightticketModel.Id = Convert.ToInt32(sdr["Id"]);
                        flightticketModel.PassengerName = sdr["PassengerName"].ToString();
                        flightticketModel.Status = sdr["Status"].ToString();
                        flightticketModel.Contact = sdr["Contact"].ToString();
                        flightticketModel.LeavingFrom = sdr["LeavingFrom"].ToString();
                        flightticketModel.GoingTo = sdr["GoingTo"].ToString();
                        flightticketModel.Email = sdr["Email"].ToString();
                        flightticketModel.Airline = sdr["Airline"].ToString();
                        flightticketModel.TravelClass = sdr["TravelClass"].ToString();
                        flightticketModel.Flight = sdr["Flight"].ToString();
                        flightticketModel.Seat = sdr["Seat"].ToString();
                        flightticketModel.CheckIn = sdr["CheckIn"].ToString();
                        flightticketModel.Gate = sdr["Gate"].ToString();
                        flightticketModel.Date = sdr["Date"].ToString();

                        flightsticketModel.Add(flightticketModel);
                    }
                }
                return flightsticketModel;
            }
        }

        //Flight Booking - Roundtrip
        public void flightbookingroundtrip(FlightsModel flightBookingroundtrip)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spROUNDTRIPFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = flightBookingroundtrip.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = flightBookingroundtrip.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = flightBookingroundtrip.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramTripType = new SqlParameter();
                paramTripType.ParameterName = "@TripType";
                paramTripType.Value = flightBookingroundtrip.TripType;
                cmd.Parameters.Add(paramTripType);

                SqlParameter paramLeavingFrom = new SqlParameter();
                paramLeavingFrom.ParameterName = "@LeavingFrom";
                paramLeavingFrom.Value = flightBookingroundtrip.LeavingFrom;
                cmd.Parameters.Add(paramLeavingFrom);

                SqlParameter paramGoingTo = new SqlParameter();
                paramGoingTo.ParameterName = "@GoingTo";
                paramGoingTo.Value = flightBookingroundtrip.GoingTo;
                cmd.Parameters.Add(paramGoingTo);

                SqlParameter paramDeparting = new SqlParameter();
                paramDeparting.ParameterName = "@Departing";
                paramDeparting.Value = flightBookingroundtrip.Departing;
                cmd.Parameters.Add(paramDeparting);

                SqlParameter paramReturning = new SqlParameter();
                paramReturning.ParameterName = "@Returning";
                paramReturning.Value = flightBookingroundtrip.Returning;
                cmd.Parameters.Add(paramReturning);

                SqlParameter paramAdults = new SqlParameter();
                paramAdults.ParameterName = "@Adults";
                paramAdults.Value = flightBookingroundtrip.Adults;
                cmd.Parameters.Add(paramAdults);

                SqlParameter paramChildren = new SqlParameter();
                paramChildren.ParameterName = "@Children";
                paramChildren.Value = flightBookingroundtrip.Children;
                cmd.Parameters.Add(paramChildren);

                SqlParameter paramFlightType = new SqlParameter();
                paramFlightType.ParameterName = "@FlightType";
                paramFlightType.Value = flightBookingroundtrip.FlightType;
                cmd.Parameters.Add(paramFlightType);

                SqlParameter paramAirline = new SqlParameter();
                paramAirline.ParameterName = "@Airline";
                paramAirline.Value = flightBookingroundtrip.Airline;
                cmd.Parameters.Add(paramAirline);

                SqlParameter paramTravelClass = new SqlParameter();
                paramTravelClass.ParameterName = "@TravelClass";
                paramTravelClass.Value = flightBookingroundtrip.TravelClass;
                cmd.Parameters.Add(paramTravelClass);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = flightBookingroundtrip.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Flight Booking - Oneway
        public void flightbookingoneway(FlightsModel flightBookingoneway)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spONEWAYFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = flightBookingoneway.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = flightBookingoneway.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = flightBookingoneway.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramTripType = new SqlParameter();
                paramTripType.ParameterName = "@TripType";
                paramTripType.Value = flightBookingoneway.TripType;
                cmd.Parameters.Add(paramTripType);

                SqlParameter paramLeavingFrom = new SqlParameter();
                paramLeavingFrom.ParameterName = "@LeavingFrom";
                paramLeavingFrom.Value = flightBookingoneway.LeavingFrom;
                cmd.Parameters.Add(paramLeavingFrom);

                SqlParameter paramGoingTo = new SqlParameter();
                paramGoingTo.ParameterName = "@GoingTo";
                paramGoingTo.Value = flightBookingoneway.GoingTo;
                cmd.Parameters.Add(paramGoingTo);

                SqlParameter paramDeparting = new SqlParameter();
                paramDeparting.ParameterName = "@Departing";
                paramDeparting.Value = flightBookingoneway.Departing;
                cmd.Parameters.Add(paramDeparting);

                SqlParameter paramAdults = new SqlParameter();
                paramAdults.ParameterName = "@Adults";
                paramAdults.Value = flightBookingoneway.Adults;
                cmd.Parameters.Add(paramAdults);

                SqlParameter paramChildren = new SqlParameter();
                paramChildren.ParameterName = "@Children";
                paramChildren.Value = flightBookingoneway.Children;
                cmd.Parameters.Add(paramChildren);

                SqlParameter paramFlightType = new SqlParameter();
                paramFlightType.ParameterName = "@FlightType";
                paramFlightType.Value = flightBookingoneway.FlightType;
                cmd.Parameters.Add(paramFlightType);

                SqlParameter paramAirline = new SqlParameter();
                paramAirline.ParameterName = "@Airline";
                paramAirline.Value = flightBookingoneway.Airline;
                cmd.Parameters.Add(paramAirline);

                SqlParameter paramTravelClass = new SqlParameter();
                paramTravelClass.ParameterName = "@TravelClass";
                paramTravelClass.Value = flightBookingoneway.TravelClass;
                cmd.Parameters.Add(paramTravelClass);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = flightBookingoneway.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Edit Flight Booking Records - Roundtrip
        public void editflightbookingrecordsroundtrip(FlightsModel editFlightBookingRecordsroundtrip)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spUpdateROUNDTRIPFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = editFlightBookingRecordsroundtrip.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = editFlightBookingRecordsroundtrip.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = editFlightBookingRecordsroundtrip.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = editFlightBookingRecordsroundtrip.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramTripType = new SqlParameter();
                paramTripType.ParameterName = "@TripType";
                paramTripType.Value = editFlightBookingRecordsroundtrip.TripType;
                cmd.Parameters.Add(paramTripType);

                SqlParameter paramLeavingFrom = new SqlParameter();
                paramLeavingFrom.ParameterName = "@LeavingFrom";
                paramLeavingFrom.Value = editFlightBookingRecordsroundtrip.LeavingFrom;
                cmd.Parameters.Add(paramLeavingFrom);

                SqlParameter paramGoingTo = new SqlParameter();
                paramGoingTo.ParameterName = "@GoingTo";
                paramGoingTo.Value = editFlightBookingRecordsroundtrip.GoingTo;
                cmd.Parameters.Add(paramGoingTo);

                SqlParameter paramDeparting = new SqlParameter();
                paramDeparting.ParameterName = "@Departing";
                paramDeparting.Value = editFlightBookingRecordsroundtrip.Departing;
                cmd.Parameters.Add(paramDeparting);

                SqlParameter paramReturning = new SqlParameter();
                paramReturning.ParameterName = "@Returning";
                paramReturning.Value = editFlightBookingRecordsroundtrip.Returning;
                cmd.Parameters.Add(paramReturning);

                SqlParameter paramAdults = new SqlParameter();
                paramAdults.ParameterName = "@Adults";
                paramAdults.Value = editFlightBookingRecordsroundtrip.Adults;
                cmd.Parameters.Add(paramAdults);

                SqlParameter paramChildren = new SqlParameter();
                paramChildren.ParameterName = "@Children";
                paramChildren.Value = editFlightBookingRecordsroundtrip.Children;
                cmd.Parameters.Add(paramChildren);

                SqlParameter paramFlightType = new SqlParameter();
                paramFlightType.ParameterName = "@FlightType";
                paramFlightType.Value = editFlightBookingRecordsroundtrip.FlightType;
                cmd.Parameters.Add(paramFlightType);

                SqlParameter paramAirline = new SqlParameter();
                paramAirline.ParameterName = "@Airline";
                paramAirline.Value = editFlightBookingRecordsroundtrip.Airline;
                cmd.Parameters.Add(paramAirline);

                SqlParameter paramTravelClass = new SqlParameter();
                paramTravelClass.ParameterName = "@TravelClass";
                paramTravelClass.Value = editFlightBookingRecordsroundtrip.TravelClass;
                cmd.Parameters.Add(paramTravelClass);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = editFlightBookingRecordsroundtrip.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Edit Flight Booking Records - Oneway
        public void editflightbookingrecordsoneway(FlightsModel editFlightBookingRecordsoneway)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spUpdateONEWAYFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = editFlightBookingRecordsoneway.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = editFlightBookingRecordsoneway.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = editFlightBookingRecordsoneway.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = editFlightBookingRecordsoneway.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramTripType = new SqlParameter();
                paramTripType.ParameterName = "@TripType";
                paramTripType.Value = editFlightBookingRecordsoneway.TripType;
                cmd.Parameters.Add(paramTripType);

                SqlParameter paramLeavingFrom = new SqlParameter();
                paramLeavingFrom.ParameterName = "@LeavingFrom";
                paramLeavingFrom.Value = editFlightBookingRecordsoneway.LeavingFrom;
                cmd.Parameters.Add(paramLeavingFrom);

                SqlParameter paramGoingTo = new SqlParameter();
                paramGoingTo.ParameterName = "@GoingTo";
                paramGoingTo.Value = editFlightBookingRecordsoneway.GoingTo;
                cmd.Parameters.Add(paramGoingTo);

                SqlParameter paramDeparting = new SqlParameter();
                paramDeparting.ParameterName = "@Departing";
                paramDeparting.Value = editFlightBookingRecordsoneway.Departing;
                cmd.Parameters.Add(paramDeparting);

                SqlParameter paramAdults = new SqlParameter();
                paramAdults.ParameterName = "@Adults";
                paramAdults.Value = editFlightBookingRecordsoneway.Adults;
                cmd.Parameters.Add(paramAdults);

                SqlParameter paramChildren = new SqlParameter();
                paramChildren.ParameterName = "@Children";
                paramChildren.Value = editFlightBookingRecordsoneway.Children;
                cmd.Parameters.Add(paramChildren);

                SqlParameter paramFlightType = new SqlParameter();
                paramFlightType.ParameterName = "@FlightType";
                paramFlightType.Value = editFlightBookingRecordsoneway.FlightType;
                cmd.Parameters.Add(paramFlightType);

                SqlParameter paramAirline = new SqlParameter();
                paramAirline.ParameterName = "@Airline";
                paramAirline.Value = editFlightBookingRecordsoneway.Airline;
                cmd.Parameters.Add(paramAirline);

                SqlParameter paramTravelClass = new SqlParameter();
                paramTravelClass.ParameterName = "@TravelClass";
                paramTravelClass.Value = editFlightBookingRecordsoneway.TravelClass;
                cmd.Parameters.Add(paramTravelClass);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = editFlightBookingRecordsoneway.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Remove Flight Booking Record - Roundtrip
        public void removeflightbookingrecordroundtrip(int id)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spDeleteROUNDTRIPFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramID = new SqlParameter();
                paramID.ParameterName = "@Id";
                paramID.Value = id;
                cmd.Parameters.Add(paramID);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        //Remove Flight Booking Record - Oneway
        public void removeflightbookingrecordoneway(int id)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spDeleteONEWAYFLIGHTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramID = new SqlParameter();
                paramID.ParameterName = "@Id";
                paramID.Value = id;
                cmd.Parameters.Add(paramID);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }


        //Flight Payment
        public void flightpayment(FlightsModel flightPayment)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spFLIGHTPAYMENTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                string ecn = encryptcardnumber(flightPayment.CardNumber);
                string ecvv = encryptcvv(flightPayment.CVV);


                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = flightPayment.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = flightPayment.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPaymentMethod = new SqlParameter();
                paramPaymentMethod.ParameterName = "@PaymentMethod";
                paramPaymentMethod.Value = flightPayment.PaymentMethod;
                cmd.Parameters.Add(paramPaymentMethod);

                SqlParameter paramCardNumber = new SqlParameter();
                paramCardNumber.ParameterName = "@CardNumber";
                paramCardNumber.Value = ecn;
                cmd.Parameters.Add(paramCardNumber);

                SqlParameter paramCVV = new SqlParameter();
                paramCVV.ParameterName = "@CVV";
                paramCVV.Value = ecvv;
                cmd.Parameters.Add(paramCVV);

                SqlParameter paramExpiryDate = new SqlParameter();
                paramExpiryDate.ParameterName = "@ExpiryDate";
                paramExpiryDate.Value = flightPayment.ExpiryDate;
                cmd.Parameters.Add(paramExpiryDate);

                SqlParameter paramAmount = new SqlParameter();
                paramAmount.ParameterName = "@Amount";
                paramAmount.Value = flightPayment.Amount;
                cmd.Parameters.Add(paramAmount);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public string encryptcardnumber(string CardNumber)
        {
            string encrypt = "";
            byte[] encode = new byte[CardNumber.Length];
            encode = Encoding.UTF8.GetBytes(CardNumber);
            encrypt = Convert.ToBase64String(encode);
            return encrypt;
        }

        public string encryptcvv(string CVV)
        {
            string encrypt = "";
            byte[] encode = new byte[CVV.Length];
            encode = Encoding.UTF8.GetBytes(CVV);
            encrypt = Convert.ToBase64String(encode);
            return encrypt;
        }

        //Remove Flight Payment Record
        public void removeflightpaymentrecord(int id)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spDeleteFLIGHTPAYMENTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        //Flight Ticket
        public void flightticket(FlightsModel flightTicket)
        {
            string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(s))
            {
                SqlCommand cmd = new SqlCommand("spFLIGHTTICKETS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramPassengerName = new SqlParameter();
                paramPassengerName.ParameterName = "@PassengerName";
                paramPassengerName.Value = flightTicket.PassengerName;
                cmd.Parameters.Add(paramPassengerName);

                SqlParameter paramStatus = new SqlParameter();
                paramStatus.ParameterName = "@Status";
                paramStatus.Value = flightTicket.Status;
                cmd.Parameters.Add(paramStatus);

                SqlParameter paramContact = new SqlParameter();
                paramContact.ParameterName = "@Contact";
                paramContact.Value = flightTicket.Contact;
                cmd.Parameters.Add(paramContact);

                SqlParameter paramLeavingFrom = new SqlParameter();
                paramLeavingFrom.ParameterName = "@LeavingFrom";
                paramLeavingFrom.Value = flightTicket.LeavingFrom;
                cmd.Parameters.Add(paramLeavingFrom);

                SqlParameter paramGoingTo = new SqlParameter();
                paramGoingTo.ParameterName = "@GoingTo";
                paramGoingTo.Value = flightTicket.GoingTo;
                cmd.Parameters.Add(paramGoingTo);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = flightTicket.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramAirline = new SqlParameter();
                paramAirline.ParameterName = "@Airline";
                paramAirline.Value = flightTicket.Airline;
                cmd.Parameters.Add(paramAirline);

                SqlParameter paramTravelClass = new SqlParameter();
                paramTravelClass.ParameterName = "@TravelClass";
                paramTravelClass.Value = flightTicket.TravelClass;
                cmd.Parameters.Add(paramTravelClass);

                SqlParameter paramSeat = new SqlParameter();
                paramSeat.ParameterName = "@Seat";
                paramSeat.Value = flightTicket.Seat;
                cmd.Parameters.Add(paramSeat);

                SqlParameter paramFlight = new SqlParameter();
                paramFlight.ParameterName = "@Flight";
                paramFlight.Value = flightTicket.Flight;
                cmd.Parameters.Add(paramFlight);

                SqlParameter paramCheckIn = new SqlParameter();
                paramCheckIn.ParameterName = "@Checkin";
                paramCheckIn.Value = flightTicket.CheckIn;
                cmd.Parameters.Add(paramCheckIn);

                SqlParameter paramGate = new SqlParameter();
                paramGate.ParameterName = "@Gate";
                paramGate.Value = flightTicket.Gate;
                cmd.Parameters.Add(paramGate);

                SqlParameter paramBarcode = new SqlParameter();
                paramBarcode.ParameterName = "@Barcode";
                paramBarcode.Value = flightTicket.Barcode;
                cmd.Parameters.Add(paramBarcode);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
