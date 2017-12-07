using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ChatController : Controller
    {
        ChatEntities _db;

        public ChatController() {
            _db = new ChatEntities();
        }

        [Authorize]
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            List<AspNetUsers> persons = _db.AspNetUsers.ToList();
            foreach (var person in persons)
            {
                users.Add(new User
                {
                    Id = person.Id,
                    name = person.UserName
                });
            }
            
            return View(users);
        }

        [Authorize]
        public ActionResult Groups()
        {
            List<GroupChats> groupChats = _db.GroupChats.ToList();
            List<Models.Group> groups = new List<Models.Group>();
            foreach (var group in groupChats)
            {
                groups.Add(new Models.Group
                {
                    Name = group.Name,
                    Image = group.Image,
                    Id = group.Id
                });
            }
            return View(groups);
        }

        [Authorize]
        public ActionResult GroupChat(String id)
        {
            ICollection<Messages> messages = _db.Messages.ToList();
            List<Messages> msgs = new List<Messages>();
            string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            int numVal = Int32.Parse(id);
            foreach (var msg in messages.ToList())
            {
                if (msg.GroupID == numVal)
                {
                    msgs.Add(msg);
                }
            }
            Models.Group group = new Models.Group();
            List<GroupChats> persons = _db.GroupChats.ToList();
            foreach (var person in persons)
            {
                if (person.Id == numVal)
                {
                    group.Name = person.Name;
                    group.Id = person.Id;
                    group.Image = person.Image;
                }
            }
            List<Messages> SortedList = msgs.OrderBy(o => o.Date).ToList();
            GroupResponse response = new GroupResponse(group, SortedList);
            response.myId = UserId;
            return View(response);
        }

        [Authorize]
        public ActionResult Chat(String id)
        {
            ICollection<Messages> messages = _db.Messages.ToList();
            List<Messages> msgs = new List<Messages>();
            string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Response response;
            foreach (var msg in messages.ToList())
            {
                if (msg.To != null)
                {
                    if ((msg.To.Equals(id) && msg.From.Equals(UserId))
                        || (msg.From.Equals(id) && msg.To.Equals(UserId)))
                    {
                        msgs.Add(msg);
                    }
                }
            }
            User user = new Models.User();
            List<AspNetUsers> persons = _db.AspNetUsers.ToList();
            foreach (var person in persons)
            {
                if (person.Id.Equals(id))
                {
                    user.name = person.UserName;
                    user.Id = person.Id;
                    user.status = person.Status;
                }
            }
            List<Messages> SortedList = msgs.OrderBy(o => o.Date).ToList();
            response = new Response(user, SortedList);
            return View(response);
        }

            // GET: Chat/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet] 
        public ActionResult CreateGroup()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(Models.Group group)
        {
            string fileName = Path.GetFileNameWithoutExtension(group.ImageFile.FileName);
            string extension = Path.GetExtension(group.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            group.Image = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            group.ImageFile.SaveAs(fileName);
            _db.GroupChats.Add(new GroupChats {
                Name = group.Name,
                Id = _db.GroupChats.Count() + 1,
                Image = group.Image
            });
            _db.SaveChanges();
            return RedirectToAction("Groups");
        }
    }
}
