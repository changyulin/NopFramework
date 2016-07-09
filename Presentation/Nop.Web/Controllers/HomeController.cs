using Nop.Services;
using Nop.Web.Extensions;
using Nop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService studentService;

        public HomeController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddStudent()
        {
            var model = new StudentModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddStudent(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                var student = model.ToEntity();
                this.studentService.InsertStudent(student);
                return View("Index");
            }
            return View(model);
        }
    }
}