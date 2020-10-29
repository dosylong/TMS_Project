using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;
using TMS_Project.ViewModels;

namespace TMS_Project.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext _context;

		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}

		// Courses/Index
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index(string searchCourse)
		{
			var courses = _context.Courses.Include(co => co.Category);

			if (!String.IsNullOrEmpty(searchCourse))
			{
				courses = courses.Where(
					s => s.Name.Contains(searchCourse) ||
					s.Category.Name.Contains(searchCourse));
			}

			return View(courses);
		}

		// Create Course (Courses/Create)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			var viewModel = new CourseCategoryViewModel
			{
				Categories = _context.Categories.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckCourseConditions/CreateNullCourse.cshtml");
			}

			//Check if Course Name existed or not
			if (_context.Courses.Any(c => c.Name == course.Name &&
										  c.CategoryId == course.CategoryId))
			{
				return View("~/Views/CheckCourseConditions/CreateExistCourse.cshtml");
			}

			var newCourse = new Course
			{
				Name = course.Name,
				Descriptions = course.Descriptions,
				CategoryId = course.CategoryId,
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();

			return View("~/Views/CheckCourseConditions/CreateCourseSuccess.cshtml");
		}

		// Edit Course (Courses/Edit/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CourseCategoryViewModel
			{
				Course = courseInDb,
				Categories = _context.Categories.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckCourseConditions/EditNullCourse.cshtml");
			}

			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == course.Id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			courseInDb.Name = course.Name;
			courseInDb.Descriptions = course.Descriptions;
			courseInDb.CategoryId = course.CategoryId;

			_context.SaveChanges();
			return View("~/Views/CheckCourseConditions/EditCourseSuccess.cshtml");
		}

		// Delete Course (Courses/Delete/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(co => co.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}