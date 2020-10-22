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
		public ActionResult Create()
		{
			var newCreate = new TraineeProfile
			{
				Trainees = _context.Users.ToList(),
			};
			return View(newCreate);
		}

		[HttpPost]
		public ActionResult Create(TraineeProfile traineeProfile)
		{
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
		public ActionResult Edit(int id)
		{
			var traineeProfileInDb = _context.TraineeProfiles.SingleOrDefault(trdb => trdb.Id == id);

			var editTraineeProfile = new TraineeProfile
			{
				Trainees = _context.Users.ToList(),
			};

			if (traineeProfileInDb == null)
			{
				return HttpNotFound();
			}


			return View(editTraineeProfile);
		}

		[HttpPost]
		public ActionResult Edit(TraineeProfile traineeProfile)
		{
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
			return RedirectToAction("Index");
		}
	}
}