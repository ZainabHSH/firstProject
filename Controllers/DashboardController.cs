using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test Message", "faabosaid18fa@gmail.com"));
            message.To.Add(MailboxAddress.Parse("zhaa1419@gmail.com"));
            message.Subject = "Test Email From My Project in Asp.net Core MVC";
            message.Body = new TextPart("Plaint")
            {
                Text = "Welcome to My App"
            };

            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("faabosaid18fa@gmail.com", "qoruxpjukuviqvnj");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
                catch(Exception e)
                {
                    return e.Message.ToString();
                }
            }

            return "Ok";
        }
        public IActionResult Update(Hotel hotel)
        {
            _context.hotel.Update(hotel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == Id);
            return View(hoteledit);
        }

        public IActionResult Delete(int Id)
        {
            var hotelDel = _context.hotel.SingleOrDefault(x => x.Id == Id);
            if (hotelDel != null)
            {
                _context.hotel.Remove(hotelDel);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }

            return RedirectToAction("Index");
        }

        //qoruxpjukuviqvnj
        [Authorize]
        public IActionResult Index()
        {

            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.currentuser = currentuser;
            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddMinutes(20);
            //Response.Cookies.Append("UserName", currentuser, option);

            HttpContext.Session.SetString("UserName", currentuser);
            var hotel = _context.hotel.ToList();
            return View(hotel);

        }

        public IActionResult CreateNewRooms(Rooms rooms)
        {
            _context.rooms.Add(rooms);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }

        public IActionResult CreateNewRoomDetails(RoomDetails roomDetails)
        {
            _context.roomDetails.Add(roomDetails);
            _context.SaveChanges();
            return RedirectToAction("RoomDetails");
        }

        [HttpPost]
        public IActionResult Index(string city)
        {
            var findhotel = _context.hotel.Where(x => x.City.Contains(city));
            ViewBag.hotel = findhotel;

            return View(findhotel);
        }

        

        public IActionResult Rooms()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;
            //ViewBag.currentuser = Request.Cookies["UserName"];
            ViewBag.currentuser = HttpContext.Session.GetString("UserName");
            var rooms = _context.rooms.ToList();
            return View(rooms);

        }

        public IActionResult RoomDetails()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var rooms = _context.rooms.ToList();
            ViewBag.rooms = rooms;

            ViewBag.currentuser = HttpContext.Session.GetString("UserName");
            var roomDetails = _context.roomDetails.ToList();
            return View(roomDetails);
        }

        public IActionResult CreateNewHotel(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel = _context.hotel.ToList();
            return View("Index", hotel);

        }

    }
}
