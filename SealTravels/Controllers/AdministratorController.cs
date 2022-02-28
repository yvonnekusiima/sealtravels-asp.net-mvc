using SealTravels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Data;

namespace SealTravels.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        private void isadministratorloggedin()
        {
            if (Session["administratorloggedin"] == null || !Convert.ToBoolean(Session["administratorloggedin"]))
            {
                Response.Redirect("AdministratorLoginView");
            }
        }
        //Administrator Login
        [HttpGet]
        public ActionResult AdministratorLoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdministratorLoginView(AdministratorModel administratorModel)
        {
            try
            {
                Session["administratorloggedin"] = false;
                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    string login = "select AdministratorId, Password from ADMINISTRATOR where AdministratorId = @AdministratorId and Password = @Password";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(login, conn);
                    cmd.Parameters.AddWithValue("@AdministratorId", administratorModel.AdministratorId);
                    cmd.Parameters.AddWithValue("@Password", administratorModel.Password);

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        Session["AdministratorId"] = administratorModel.AdministratorId.ToString();

                        if (administratorModel.Captcha.ToLower() == Session["CaptchaVerify"].ToString())
                        {
                            Session["administratorloggedin"] = true;
                            return RedirectToAction("AdministratorPageView", "Administrator");
                        }
                        else
                        {
                            Response.Write("<script> alert('Captcha code incorrect')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script> alert('Administrator Id or Password incorrect')</script>");
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('ERROR: " + ex.Message + "') </script>");
            }
            return View();
        }

        //Administrator Page
        [HttpGet]
        public ActionResult AdministratorPageView()
        {
            isadministratorloggedin();
            return View();
        }

        //Captcha
        [HttpGet]
        public ActionResult AdministratorCaptchaView()
        {
            Bitmap objBitmap = new Bitmap(200, 60);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush =
                default(Brush);

            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };

            RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);

            string captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));

            Session["CaptchaVerify"] = captchaText.ToLower();
            Font objFont = new Font("Heebo", 15, FontStyle.Bold);

            objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
            objBitmap.Save(Response.OutputStream, ImageFormat.Gif);

            return View();
        }
        [HttpPost]
        public ActionResult AdministratorCaptchaView(AdministratorModel administratorModel)
        {
            return View();
        }

        //Add Users
        [HttpGet]
        public ActionResult AddUserView()
        {
            isadministratorloggedin();
            return View();
        }
        [HttpPost]
        public ActionResult AddUserView(AdministratorModel administratorModel)
        {
            try
            {
                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    SqlCommand cmd = new SqlCommand("spUSERS", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = administratorModel.Email;
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramUsername = new SqlParameter();
                    paramUsername.ParameterName = "@Username";
                    paramUsername.Value = administratorModel.Username;
                    cmd.Parameters.Add(paramUsername);

                    string autogeneratedpassword = administratorModel.Password = autogeneratepassword(8);

                    SqlParameter paramPassword = new SqlParameter();
                    paramPassword.ParameterName = "@Password";
                    paramPassword.Value = encryptpassword(autogeneratedpassword.Trim());
                    cmd.Parameters.Add(paramPassword);

                    SqlParameter paramDateAdded = new SqlParameter();
                    paramDateAdded.ParameterName = "@DateAdded";
                    paramDateAdded.Value = administratorModel.DateAdded;
                    cmd.Parameters.Add(paramDateAdded);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    Response.Write("<script> alert('User Added') </script>");

                    MailMessage mail = new MailMessage();
                    mail.To.Add(administratorModel.Email);
                    mail.From = new MailAddress("vonworkug@gmail.com", "VONWORK UG");
                    mail.Subject = "Seal Travels Login Credentials";
                    mail.Body = "Your username is " + administratorModel.Username + "\n" + "Your password is " + autogeneratedpassword;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("vonworkug@gmail.com", "vonworkug00!");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    Response.Write("<script> alert('Email sent') </script>");

                    return RedirectToAction("UserRecordsView", "Administrator");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('ERROR: " + ex.Message + "') </script>");
            }
            return View();
        }
        public string autogeneratepassword(int PasswordLength)
        {
            string allowedchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random randompassword = new Random();
            char[] chars = new char[PasswordLength];
            int allowedcharscount = allowedchars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = allowedchars[(int)((allowedchars.Length) * randompassword.NextDouble())];
            }
            return new string(chars);
        }
        private string encryptpassword(string password)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }

        //User Records
        [HttpGet]
        public ActionResult UserRecordsView()
        {
            try
            {
                isadministratorloggedin();
                AdministratorLayer administratorLayer = new AdministratorLayer();
                List<AdministratorModel> administrator = administratorLayer.administratormodel.ToList();

                return View(administrator);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Edit Users
        [HttpGet]
        public ActionResult EditUserView(int id)
        {
            try
            {
                isadministratorloggedin();
                AdministratorLayer administratorLayer = new AdministratorLayer();
                AdministratorModel administratorModel = administratorLayer.administratormodel.Single(user => user.Id == id);

                return View(administratorModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditUserView(AdministratorModel administratorModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdministratorLayer administratorLayer = new AdministratorLayer();
                    administratorLayer.edituser(administratorModel);

                    return RedirectToAction("UserRecordsView", "Administrator");
                }
                return View(administratorModel);
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }

        //Remove Users
        [HttpGet]
        public ActionResult RemoveUser(int id)
        {
            try
            {
                AdministratorLayer administratorLayer = new AdministratorLayer();
                administratorLayer.removeuser(id);

                return RedirectToAction("UserRecordsView", "Administrator");
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
    }
}
