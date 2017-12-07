using AspNetDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetDemoProject.Controllers
{
    public class StudentsController : Controller
    {
        TestDBEntities _db;

        public StudentsController()
        {
            _db = new TestDBEntities();
        }

        // GET: Students
        public ActionResult Index()
        {
            List<People> persons = _db.People.ToList();
            List<Student> students = new List<Student>();
            foreach (var person in persons)
            {
                students.Add(new Student
                {
                    Id = person.Id,
                    Name = person.Name,
                    LastName = person.LastName,
                    DisplayBirthDate = person.BirthDate.ToShortDateString(),
                    BirthDate = person.BirthDate,
                    City = person.City,
                    Email = person.Email
                });
            }

            return View(students);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddStudent(Student student)
        {
            // Save
            _db.People.Add(new People
            {
                Name = student.Name,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                City = student.City,
                Email = student.Email
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var person = _db.People.Find(id);
            Student student = new Student
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                DisplayBirthDate = person.BirthDate.ToShortDateString(),
                BirthDate = person.BirthDate,
                City = person.City,
                Email = person.Email
            };

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int id)
        {
            var person = _db.People.Find(id);
            if (person != null)
            {
                _db.People.Remove(person);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var person = _db.People.Find(id);
            Student student = new Student{
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                DisplayBirthDate = person.BirthDate.ToShortDateString(),
                BirthDate = person.BirthDate,
                City = person.City,
                Email = person.Email
            };
            
            return View(student);
        }

        public ActionResult EditStudent(Student student) {
            // Edit
            var person = _db.People.Find(student.Id);

            if (person != null)
            {
                person.Name = student.Name;
                person.LastName = student.LastName;
                person.BirthDate = Convert.ToDateTime(student.DisplayBirthDate);
                person.City = student.City;
                person.Email = student.Email;

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var person = _db.People.Find(id);
            Student student = new Student
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                DisplayBirthDate = person.BirthDate.ToShortDateString(),
                BirthDate = person.BirthDate,
                City = person.City,
                Email = person.Email
            };

            return View(student);
        }
    }
}