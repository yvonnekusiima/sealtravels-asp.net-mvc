using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SealTravels.Models;

namespace SealTravels.Controllers
{
    public class HotelsController : Controller
    {
        // GET: Hotels
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
        //Hotel Booking Records
        [HttpGet]
        public ActionResult HotelBookingRecordsViewA()
        {
            try
            {
                isadministratorloggedin();
                HotelsLayer hotelLayer = new HotelsLayer();
                List<HotelsModel> hotelsModel = hotelLayer.hotelsmodel.ToList();

                return View(hotelsModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult HotelBookingRecordsViewU()
        {
            try
            {
                isuserloggedin();
                HotelsLayer hotelsLayer = new HotelsLayer();
                List<HotelsModel> hotelsModel = hotelsLayer.hotelsmodel.ToList();

                return View(hotelsModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Hotel Booking
        [HttpGet]
        public ActionResult HotelBookingViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpGet]
        public ActionResult HotelBookingViewU()
        {
            isuserloggedin();
            return View();
        }

        //EDIT HOTELRECORDS
        [HttpGet]
        public ActionResult EditHotelBookingRecordsViewA(int id)
        {
            try
            {
                isadministratorloggedin();
                HotelsLayer hotelsLayer = new HotelsLayer();
                HotelsModel hotelModel = hotelsLayer.hotelsmodel.Single(hotel => hotel.Id == id);

                return View(hotelModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditHotelBookingRecordsViewA(HotelsModel hotelsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HotelsLayer hotelsLayer = new HotelsLayer();
                    hotelsLayer.edithotelbookingrecords(hotelsModel);

                    Response.Write("<script> alert('Hotel booking updated')</script>");

                    return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
                }
                return View(hotelsModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditHotelBookingRecordsViewU(int id)
        {
            try
            {
                isuserloggedin();
                HotelsLayer hotelsLayer = new HotelsLayer();
                HotelsModel hotelsModel = hotelsLayer.hotelsmodel.Single(hotel => hotel.Id == id);

                return View(hotelsModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditHotelBookingRecordsViewU(HotelsModel hotelsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HotelsLayer hotelsLayer = new HotelsLayer();
                    hotelsLayer.edithotelbookingrecords(hotelsModel);

                    Response.Write("<script> alert('Hotel booking updated')</script>");

                    return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
                }
                return View(hotelsModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Remove Hotel Booking
        [HttpGet]
        public ActionResult RemoveHotelBookingRecordA(int id)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.removehotelbookingrecord(id);

                Response.Write("<script> alert('Hotel booking removed')</script>");

                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult RemoveHotelBookingRecordU(int id)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.removehotelbookingrecord(id);

                Response.Write("<script> alert('Hotel booking removed')</script>");

                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Eko Hotel
        [HttpGet]
        public ActionResult EkoHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult EkoHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }                   
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EkoHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult EkoHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Hotel Brussels
        [HttpGet]
        public ActionResult HotelBrusselsViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult HotelBrusselsViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult HotelBrusselsViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult HotelBrusselsViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //The Monarch Hotel
        [HttpGet]
        public ActionResult TheMonarchHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult TheMonarchHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult TheMonarchHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult TheMonarchHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //The Oberoi
        [HttpGet]
        public ActionResult TheOberoiViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult TheOberoiViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult TheOberoiViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult TheOberoiViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Mestil Hotel
        [HttpGet]
        public ActionResult MestilHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult MestilHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult MestilHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult MestilHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Kigali Marriott Hotel
        [HttpGet]
        public ActionResult KigaliMarriottHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult KigaliMarriottHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult KigaliMarriottHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult KigaliMarriottHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Lagoon Hotel
        [HttpGet]
        public ActionResult LagoonHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult LagoonHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult LagoonHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult LagoonHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Shangri-la Hotel
        [HttpGet]
        public ActionResult ShangrilaHotelViewA()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult ShangrilaHotelViewA(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewA", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ShangrilaHotelViewU()
        {
            isuserloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult ShangrilaHotelViewU(HotelsModel hotelsModel)
        {
            try
            {
                HotelsLayer hotelsLayer = new HotelsLayer();
                hotelsLayer.hotelbooking(hotelsModel);

                Response.Write("<script> alert('Booked')</script>");

                if (ModelState.IsValid)
                {
                    var SenderEmail = new MailAddress("vonworkug@gmail,com", "VONWORK UG");
                    var Password = "vonworkug00!";

                    var ReceiverEmail = new MailAddress(hotelsModel.Email);

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
                        Subject = "Hotel Booking",
                        Body = "Hello " + hotelsModel.Name + ", your booking at " + hotelsModel.Hotel + " has been recieved." + "\n \n" + "BOOKING DETAILS \n" + "Name - " + hotelsModel.Name + "\n" + "Travelers - " + hotelsModel.Travelers + "\n" + "Rooms - " + hotelsModel.Rooms + "\n" + "Room Type - " + hotelsModel.RoomType + "Check-in - " + hotelsModel.CheckIn + "\n" + "Check-out - " + hotelsModel.CheckOut + "\n" + "Contact - " + hotelsModel.Contact + "\n" + "Amount - " + hotelsModel.Amount
                    })
                    {
                        smtp.Send(message);
                    }
                }
                return RedirectToAction("HotelBookingRecordsViewU", "Hotels");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
    }
}
