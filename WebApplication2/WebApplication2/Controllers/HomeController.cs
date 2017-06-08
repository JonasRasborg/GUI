using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Teacher = "Puol Ejnar";
            var students = new List<Student>();
            var st1 = new Student() {Name = "Jonas", Grade = 12};
            var st2 = new Student() { Name = "John", Grade = 02 };
            var st3 = new Student() { Name = "Anders", Grade = -3 };

            students.Add(st1);students.Add(st2);students.Add(st3);

            return View(students);
        }

        public ActionResult MotherF(int id = 0)
        {
            return View(id);
        }
    }
}