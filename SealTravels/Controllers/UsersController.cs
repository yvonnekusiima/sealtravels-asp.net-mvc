using SealTravels.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SealTravels.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        private void isuserloggedin()
        {
            if (Session["userloggedin"] == null || !Convert.ToBoolean(Session["userloggedin"]))
            {
                Response.Redirect("UserLoginView");
            }
        }
        //User Login
        [HttpGet]
        public ActionResult UserLoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLoginView(UsersModel usersModel)
        {
            try
            {
                Session["userloggedin"] = false;
                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    string login = "select Username, Password from USERS where Username = @Username and Password = @Password";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(login, conn);
                    cmd.Parameters.AddWithValue("@Username", usersModel.Username);
                    cmd.Parameters.AddWithValue("@Password", decryptpassword(usersModel.Password.Trim()));

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        Session["Username"] = usersModel.Username.ToString();

                        if (usersModel.Captcha.ToLower() == Session["CaptchaVerify"].ToString())
                        {
                            Session["userloggedin"] = true;
                            return RedirectToAction("UserPageView", "Users");
                        }
                        else
                        {
                            Response.Write("<script> alert('Captcha code incorrect')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script> alert('Username or Password incorrect')</script>");
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
        }
        private string decryptpassword(string password)
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

        //User Page
        [HttpGet]
        public ActionResult UserPageView()
        {
            isuserloggedin();
            return View();
        }

        //Change Password
        [HttpGet]
        public ActionResult ChangePasswordView()
        {
            isuserloggedin();
            return View();
        }
        byte b;
        [HttpPost]
        public ActionResult ChangePasswordView(UsersModel usersModel)
        {
            try
            {
                string s = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(s))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Users", conn);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        if (decryptpassword(usersModel.CurrentPassword) == sdr["Password"].ToString())
                        {
                            b = 1;
                        }
                    }
                    sdr.Close();
                    conn.Close();
                    if (b == 1)
                    {
                        conn.Open();
                        cmd = new SqlCommand("update Users set Password=@Password where Username='" + usersModel.Username + "'", conn);
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50));
                        cmd.Parameters["@Password"].Value = encryptpassword(usersModel.NewPassword);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Response.Write("<script> alert('Password changed successfully') </script>");
                    }
                    else
                    {
                        Response.Write("<script> alert('Current Password incorrect') </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
            return View();
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

        //Captcha
        [HttpGet]
        public ActionResult UserCaptchaView()
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
        public ActionResult UserCaptchaView(UsersModel usersModel)
        {
            return View();
        }
    }
}
