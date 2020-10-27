using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			//Get Account Trainee
			var roleInDb = (from r in _context.Roles where r.Name.Contains("Trainee") select r)
									 .FirstOrDefault();

			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId)
														 .Contains(roleInDb.Id))
														 .ToList();
			//Get Course
			var courses = _context.Courses.ToList();

			var viewModel = new TraineeToCourseViewModel
			{
				Courses = courses,
				Trainees = users,
				TraineeToCourse = new TraineeToCourse()
			};

			return View(viewModel);
		}


		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(TraineeToCourse traineeToCourse)
		{

			/*var checkTraineeAndCourseExist = _context.TraineeToCourses.SingleOrDefault(
									c => c.CourseId == traineeToCourse.CourseId &&
									c.TraineeId == traineeToCourse.TraineeId);

			if (checkTraineeAndCourseExist != null)
			{
				ModelState.AddModelError("Email", "Course Name or Trainee Already Exists.");
				return View();
			}*/

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
		[Authorize(Roles = "TrainingStaff")]
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

		/*[HttpGet]
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
		}*/

		[HttpGet]
		[Authorize(Roles = "Trainee")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var traineeToCourses = _context.TraineeToCourses
				.Where(c => c.TraineeId == userId)
				.Include(c => c.Course)
				.Include(c => c.Trainee)
				.ToList();

			return View(traineeToCourses);
		}
	}
}