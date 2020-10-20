using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS_Project.Models;
using System.Data.Entity;
using TMS_Project.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Management;
using System.Data.Entity.Migrations;

namespace TMS_Project.Controllers
{
    public class TraineeToCoursesController : Controller
    {
        private ApplicationDbContext _context;
        public TraineeToCoursesController()
		{
            _context = new ApplicationDbContext();
		}

        [HttpGet]
        // GET: TraineeToCourses
        public ActionResult Index(string searchTrainee)
        {
            var traineetocourses = _context.TraineeToCourses
                                   .Include(tr => tr.Course)
                                   .Include(tr => tr.Trainee);

            if (!String.IsNullOrEmpty(searchTrainee))
			{
                traineetocourses = traineetocourses.Where(
                        s => s.Trainee.UserName.Contains(searchTrainee) ||
                        s.Trainee.Email.Contains(searchTrainee));
			}

            return View(traineetocourses);
        }

        [HttpGet]
        public ActionResult Create()
		{
            var viewModel = new TraineeToCourseViewModel
            {
                Courses = _context.Courses.ToList()
            };

            // Lấy data
            // Lấy toàn bộ thể loại:
            List<ApplicationUser> applicationUsers = _context.Users.ToList();

            // Tạo SelectList
            SelectList traineeList = new SelectList(applicationUsers, "Id", "Email");

            // Set vào ViewBag

            ViewBag.TraineeList = traineeList;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
		{
            TraineeToCourse traineeToCourse = new TraineeToCourse
            {
                TraineeId = User.Identity.GetUserId(),
                CourseId = course.Id
            };

            _context.TraineeToCourses.Add(traineeToCourse);
            _context.SaveChanges();
            return RedirectToAction("Index");
		}

        [HttpGet]
        public ActionResult Delete(int id)
		{
            var traineeInDb = _context.TraineeToCourses.SingleOrDefault(tr => tr.Id == id);
            if (traineeInDb == null)
			{
                return HttpNotFound();
			}

            _context.TraineeToCourses.Remove(traineeInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
		}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var traineeInDb = _context.TraineeToCourses.SingleOrDefault(tr => tr.Id == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TraineeToCourseViewModel
            {
                Courses = _context.Courses.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TraineeToCourse traineeToCourse)
		{
            if (!ModelState.IsValid)
            {
                return View();
            }

            var traineeInDb = _context.TraineeToCourses.SingleOrDefault(tr => tr.Id == traineeToCourse.Id);
            if (traineeInDb == null)
			{
                return HttpNotFound();
			}

            traineeInDb.Course.Id = traineeToCourse.Course.Id;
            traineeInDb.Course.Name = traineeToCourse.Course.Name;

            _context.TraineeToCourses.AddOrUpdate(traineeInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}