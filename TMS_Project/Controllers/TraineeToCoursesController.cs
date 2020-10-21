using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;
using TMS_Project.ViewModels;

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
				Courses = _context.Courses.ToList(),
				Trainees = _context.Users.ToList()
			};

			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(TraineeToCourse traineeToCourse)
		{
			var newTraineeToCourse = new TraineeToCourse
			{
				TraineeId = traineeToCourse.TraineeId,
				CourseId = traineeToCourse.CourseId
			};

			_context.TraineeToCourses.Add(newTraineeToCourse);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var traineeInDb = _context.TraineeToCourses.SingleOrDefault(trdb => trdb.Id == id);
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
			var traineeInDb = _context.TraineeToCourses.SingleOrDefault(trdb => trdb.Id == id);

			if (traineeInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TraineeToCourseViewModel
			{
				TraineeToCourse = traineeInDb,
				Courses = _context.Courses.ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(TraineeToCourse traineeToCourse)
		{
			var traineeInDb = _context.TraineeToCourses.SingleOrDefault(trdb => trdb.Id == traineeToCourse.Id);

			if (traineeInDb == null)
			{
				return HttpNotFound();
			}

			traineeInDb.CourseId = traineeToCourse.CourseId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}