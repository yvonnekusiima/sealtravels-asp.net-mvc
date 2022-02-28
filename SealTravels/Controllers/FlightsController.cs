using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SealTravels.Models;
using ZXing;

namespace SealTravels.Controllers
{
    public class FlightsController : Controller
    {
        // GET: Flight
        private void isuserloggedin()
        {
            if (Session["userloggedin"] == null || !Convert.ToBoolean(Session["userloggedin"]))
            {
                Response.Redirect("/Users/UserLoginView");
            }
        }
        private void isadministratorloggedin()
        {
            if (Session["administratorloggedin"] == null || !Convert.ToBoolean(Session["administratorloggedin"]))
            {
                Response.Redirect("/Administrator/AdministratorLoginView");
            }
        }
        //Flight Booking Records - Roundtrip
        [HttpGet]
        public ActionResult RoundtripRecordsViewA()
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.RoundtripFlightModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult RoundtripRecordsViewU()
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.RoundtripFlightModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Flight Booking Records - Oneway
        [HttpGet]
        public ActionResult OnewayRecordsViewA()
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.OnewayFlightModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OnewayRecordsViewU()
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.OnewayFlightModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Flight Booking - Roundtrip
        [HttpGet]
        public ActionResult RoundtripBookingViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult RoundtripBookingViewA(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightbookingroundtrip(flightModel);

                Response.Write("<script> alert('Flight Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been recieved." + "\n \n" + "BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Returning - " + flightModel.Returning + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("RoundtripRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult RoundtripBookingViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult RoundtripBookingViewU(FlightsModel flightModel, string email, string subject = "Flight Booking", string link = "https://SealTravels.yvonnevanita.com/Flight/FlightPaymentView")
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightbookingroundtrip(flightModel);

                Response.Write("<script> alert('Flight Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been recieved." + "\n \n" + "BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Returning - " + flightModel.Returning + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("RoundtripRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Flight Booking - Oneway
        [HttpGet]
        public ActionResult OnewayBookingViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult OnewayBookingViewA(FlightsModel flightModel, string email, string subject = "Flight Booking", string link = "http://localhost:56481/Flight/PaymentView")
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightbookingoneway(flightModel);

                Response.Write("<script> alert('Flight Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been recieved." + "\n \n" + "BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("OnewayRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OnewayBookingViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult OnewayBookingViewU(FlightsModel flightModel, string email, string subject = "Flight Booking", string link = "http://localhost:56481/Flight/PaymentView")
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightbookingoneway(flightModel);

                Response.Write("<script> alert('Flight Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been recieved." + "\n \n" + "BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("OnewayRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Edit Flight Booking Records - Roundtrip
        [HttpGet]
        public ActionResult EditRoundtripRecordsViewA(int id)
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                FlightsModel flightModel = flightLayer.RoundtripFlightModel.Single(flight => flight.Id == id);              

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditRoundtripRecordsViewA(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer hotelLayer = new FlightsLayer();
                hotelLayer.editflightbookingrecordsroundtrip(flightModel);

                Response.Write("<script> alert('Flight booking updated')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking Update",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been updated." + "\n \n" + "NEW BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Returning - " + flightModel.Returning + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("RoundtripRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View(flightModel);
        }
        [HttpGet]
        public ActionResult EditRoundtripRecordsViewU(int id)
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                FlightsModel flightModel = flightLayer.RoundtripFlightModel.Single(flight => flight.Id == id);

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditRoundtripRecordsViewU(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer hotelLayer = new FlightsLayer();
                hotelLayer.editflightbookingrecordsroundtrip(flightModel);

                Response.Write("<script> alert('Flight booking updated')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking Update",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been updated." + "\n \n" + "NEW BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Returning - " + flightModel.Returning + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("RoundtripRecordsViewu", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View(flightModel);
        }

        //Edit Flight Booking Records - Oneway
        [HttpGet]
        public ActionResult EditOnewayRecordsViewA(int id)
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                FlightsModel flightModel = flightLayer.OnewayFlightModel.Single(flight => flight.Id == id);

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditOnewayRecordsViewA(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer hotelLayer = new FlightsLayer();
                hotelLayer.editflightbookingrecordsoneway(flightModel);

                Response.Write("<script> alert('Flight booking updated')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking Update",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been updated." + "\n \n" + "NEW BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("OnewayRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View(flightModel);
        }
        [HttpGet]
        public ActionResult EditOnewayRecordsViewU(int id)
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                FlightsModel flightModel = flightLayer.OnewayFlightModel.Single(flight => flight.Id == id);

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditOnewayRecordsViewU(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer hotelLayer = new FlightsLayer();
                hotelLayer.editflightbookingrecordsoneway(flightModel);

                Response.Write("<script> alert('Flight booking updated')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Booking Update",
                        Body = "Hello " + flightModel.Name + ", your flight booking has been updated." + "\n \n" + "NEW BOOKING DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Contact - " + flightModel.Contact + "\n" + "Trip Type - " + flightModel.TripType + "\n" + "Leaving From - " + flightModel.LeavingFrom + "\n" + "Going To - " + flightModel.GoingTo + "Departing - " + flightModel.Departing + "\n" + "Adults - " + flightModel.Adults + "\n" + "Children - " + flightModel.Children + "\n" + "Flight Type - " + flightModel.FlightType + "\n" + "Airline - " + flightModel.Airline + "\n" + "Travel Class - " + flightModel.TravelClass + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("OnewayRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View(flightModel);
        }

        //Remove Flight Booking Record - Roundtrip
        public ActionResult RemoveRoundtripRecordsViewA(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightbookingrecordroundtrip(id);

                Response.Write("<script> alert('Flight booking removed')</script>");

                return RedirectToAction("RoundtripRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        public ActionResult RemoveRoundtripRecordsViewU(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightbookingrecordroundtrip(id);

                Response.Write("<script> alert('Flight booking removed')</script>");

                return RedirectToAction("RoundtripRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Remove Flight Booking Record - Oneway
        public ActionResult RemoveOnewayRecordsViewA(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightbookingrecordoneway(id);

                Response.Write("<script> alert('Flight booking removed')</script>");

                return RedirectToAction("OnewayRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        public ActionResult RemoveOnewayRecordsViewU(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightbookingrecordoneway(id);

                Response.Write("<script> alert('Flight booking removed')</script>");

                return RedirectToAction("OnewayRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        public ActionResult RenderBarcode()
        {
            string barcode = autogeneratebarcode(8);

            Image img = null;
            using (var ms = new MemoryStream())
            {
                var writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                writer.Options.Width = 280;
                writer.Options.Height = 80;
                writer.Options.PureBarcode = true;
                img = writer.Write(barcode);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return File(ms.ToArray(), "image/jpeg");
            }
        }
        public string autogeneratebarcode(int BarcodeLength)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random randombarcode = new Random();
            char[] chars = new char[BarcodeLength];
            int allowedcharscount = allowedchars.Length;
            for (int i = 0; i < BarcodeLength; i++)
            {
                chars[i] = allowedchars[(int)((allowedchars.Length) * randombarcode.NextDouble())];
            }
            return new string(chars);
        }


        //Flight Payment
        [HttpGet]
        public ActionResult FlightPaymentView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlightPaymentView(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightpayment(flightModel);

                Response.Write("<script> alert('Flight payment submitted')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(flightModel.Email);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SenderEmail.Address, Password)
                    };
                    using (var message = new MailMessage(SenderEmail, ReceiverEmail)
                    {
                        Subject = "Flight Payment",
                        Body = "Hello " + flightModel.Name + ", your flight payment has been recieved." + "\n \n" + "PAYMENT DETAILS \n \n" + "Name - " + flightModel.Name + "\n" + "Payment Method - " + flightModel.PaymentMethod + "\n" + "Amount - " + flightModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("", "");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return RedirectToAction("", "");
        }

        //Flight Payment Records
        public ActionResult FlightPaymentRecordsViewA()
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.FlightPaymentModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        public ActionResult FlightPaymentRecordsViewU()
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.FlightPaymentModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Remove Flight Payment Records
        public ActionResult RemoveFlightPaymentRecordA(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightpaymentrecord(id);

                Response.Write("<script> alert('Flight payment removed')</script>");

                return RedirectToAction("FlightPaymentRecordsViewA", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        public ActionResult RemoveFlightPaymentRecordU(int id)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.removeflightpaymentrecord(id);

                Response.Write("<script> alert('Flight payment removed')</script>");

                return RedirectToAction("FlightPaymentRecordsViewU", "Flights");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Flight Tickets
        [HttpGet]
        public ActionResult FlightTicketViewA()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlightTicketViewA(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightticket(flightModel);

                Response.Write("<script> alert('Flight ticket saved')</script>");

                return RedirectToAction("", "");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        [HttpGet]
        public ActionResult FlightTicketViewU()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlightTicketViewU(FlightsModel flightModel)
        {
            try
            {
                FlightsLayer flightLayer = new FlightsLayer();
                flightLayer.flightticket(flightModel);

                Response.Write("<script> alert('Flight ticket saved')</script>");

                return RedirectToAction("", "");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Flight Tickets Records
        public ActionResult FlightTicketsRecordsViewA()
        {
            try
            {
                isadministratorloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.FlightTicketModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        public ActionResult FlightTicketsRecordsViewU()
        {
            try
            {
                isuserloggedin();
                FlightsLayer flightLayer = new FlightsLayer();
                List<FlightsModel> flightModel = flightLayer.FlightTicketModel.ToList();

                return View(flightModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
    }
}
