using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
	public class TrainerProfilesController : Controller
	{
		private ApplicationDbContext _context;
		public TrainerProfilesController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		// GET: TrainerProfiles
		public ActionResult Index(string searchTrainerProfile)
		{
			var trainerProfiles = _context.TrainerProfiles.Include(tp => tp.Trainer);

			if (!String.IsNullOrEmpty(searchTrainerProfile))
			{
				trainerProfiles = trainerProfiles.Where(
						s => s.Trainer.UserName.Contains(searchTrainerProfile) ||
						s.Trainer.Email.Contains(searchTrainerProfile));
			}
			return View(trainerProfiles);
		}

		[HttpGet]
		public ActionResult Create()
		{
			TrainerProfile trainerProfile = new TrainerProfile
			{
				Trainers = _context.Users.ToList(),
			};
			return View(trainerProfile);
		}

		[HttpPost]
		public ActionResult Create(TrainerProfile trainerProfile)
		{
			var getTrainerProfile = new TrainerProfile
			{
				TrainerId = trainerProfile.TrainerId,
				Full_Name = trainerProfile.Full_Name,
				External_Internal = trainerProfile.External_Internal,
				Education = trainerProfile.Education,
				Working_Place = trainerProfile.Working_Place,
				Phone_Number = trainerProfile.Phone_Number
			};

			_context.TrainerProfiles.Add(getTrainerProfile);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var trainerProfileInDb = _context.TrainerProfiles.SingleOrDefault(trdb => trdb.Id == id);
			if (trainerProfileInDb == null)
			{
				return HttpNotFound();
			}

			_context.TrainerProfiles.Remove(trainerProfileInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var trainerProfileInDb = _context.TrainerProfiles.SingleOrDefault(trdb => trdb.Id == id);

			var editTrainerProfile = new TrainerProfile
			{
				Trainers = _context.Users.ToList(),
			};

			if (trainerProfileInDb == null)
			{
				return HttpNotFound();
			}


			return View(editTrainerProfile);
		}

		[HttpPost]
		public ActionResult Edit(TrainerProfile trainerProfile)
		{
			var trainerProfileInDb = _context.TrainerProfiles.SingleOrDefault(trdb => trdb.Id == trainerProfile.Id);

			if (trainerProfileInDb == null)
			{
				return HttpNotFound();
			}

			trainerProfileInDb.TrainerId = trainerProfile.TrainerId;
			trainerProfileInDb.Full_Name = trainerProfile.Full_Name;
			trainerProfileInDb.External_Internal = trainerProfile.External_Internal;
			trainerProfileInDb.Education = trainerProfile.Education;
			trainerProfileInDb.Working_Place = trainerProfile.Working_Place;
			trainerProfileInDb.Phone_Number = trainerProfile.Phone_Number;
			_context.SaveChanges();

			if (User.IsInRole("TrainingStaff"))
			{
				return RedirectToAction("Index");
			}

			if (User.IsInRole("Trainer"))
			{
				return RedirectToAction("Mine");
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "Trainer")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var trainerProfiles = _context.TrainerProfiles
				.Where(c => c.TrainerId == userId)
				.Include(c => c.Trainer)
				.ToList();

			return View(trainerProfiles);
		}
	}
}