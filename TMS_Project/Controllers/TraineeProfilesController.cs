using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
	public class TraineeProfilesController : Controller
	{
		private ApplicationDbContext _context;
		public TraineeProfilesController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		// GET: TraineeProfiles
		public ActionResult Index(string searchTraineeProfile)
		{
			var traineeProfiles = _context.TraineeProfiles.Include(tp => tp.Trainee);

			if (!String.IsNullOrEmpty(searchTraineeProfile))
			{
				traineeProfiles = traineeProfiles.Where(
						s => s.Trainee.UserName.Contains(searchTraineeProfile) ||
						s.Trainee.Email.Contains(searchTraineeProfile));
			}
			return View(traineeProfiles);
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

			var traineeProfiles = _context.TraineeProfiles.ToList();

			var traineeProfile = new TraineeProfile
			{
				Trainees = users,

			};
			return View(traineeProfile);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(TraineeProfile traineeProfile)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckTraineeProfileConditions/CreateNullTraineeProfile.cshtml");
			}

			//Check if Trainee Profile existed or not
			if (_context.TraineeToCourses.Any(c => c.TraineeId == traineeProfile.TraineeId))
			{
				return View("~/Views/CheckTraineeProfileConditions/CreateExistTraineeProfile.cshtml");
			}

			var getTraineeProfile = new TraineeProfile
			{
				TraineeId = traineeProfile.TraineeId,
				Full_Name = traineeProfile.Full_Name,
				Education = traineeProfile.Education,
				Programming_Language = traineeProfile.Programming_Language,
				Experience_Details = traineeProfile.Experience_Details,
				Location = traineeProfile.Location
			};

			_context.TraineeProfiles.Add(getTraineeProfile);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var traineeProfileInDb = _context.TraineeProfiles.SingleOrDefault(trdb => trdb.Id == id);
			if (traineeProfileInDb == null)
			{
				return HttpNotFound();
			}

			_context.TraineeProfiles.Remove(traineeProfileInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(int id)
		{
			var traineeProfileInDb = _context.TraineeProfiles.SingleOrDefault(trdb => trdb.Id == id);

			if (traineeProfileInDb == null)
			{
				return HttpNotFound();
			}

			return View(traineeProfileInDb);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(TraineeProfile traineeProfile)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var traineeProfileInDb = _context.TraineeProfiles.SingleOrDefault(trdb => trdb.Id == traineeProfile.Id);

			if (traineeProfileInDb == null)
			{
				return HttpNotFound();
			}

			traineeProfileInDb.TraineeId = traineeProfile.TraineeId;
			traineeProfileInDb.Full_Name = traineeProfile.Full_Name;
			traineeProfileInDb.Education = traineeProfile.Education;
			traineeProfileInDb.Programming_Language = traineeProfile.Programming_Language;
			traineeProfileInDb.Experience_Details = traineeProfile.Experience_Details;
			traineeProfileInDb.Location = traineeProfile.Location;

			_context.SaveChanges();
			return View("~/Views/CheckTraineeProfileConditions/EditTraineeProfileSucess.cshtml");
		}

		[HttpGet]
		[Authorize(Roles = "Trainee")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var traineeProfiles = _context.TraineeProfiles
				.Where(c => c.TraineeId == userId)
				.Include(c => c.Trainee)
				.ToList();

			return View(traineeProfiles);
		}
	}
}