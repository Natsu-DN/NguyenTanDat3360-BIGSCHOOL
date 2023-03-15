using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        //[Authorize]
        public ActionResult Create()
        {
            var viewModels = new CourseViewModels
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModels);
        }

        [Authorize]
        [HttpPost]
        public ActionResult create(CourseViewModels viewmodels)
        {
            if (!ModelState.IsValid)
            {
                viewmodels.Categories = _dbContext.Categories.ToList();
                return View("Create", viewmodels);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewmodels.GetDateTime(),
                CategoryId = viewmodels.Category,
                Place = viewmodels.Place,
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("index", "home");
        }
    }
}

