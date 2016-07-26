using Nop.Core;
using Nop.Core.Domain.Customers;
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
        private readonly IWorkContext _workContext;

        public HomeController(IStudentService studentService, IWorkContext workContext)
        {
            this.studentService = studentService;
            this._workContext = workContext;
        }

        public ActionResult Index()
        {
            Customer customer = _workContext.CurrentCustomer;
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