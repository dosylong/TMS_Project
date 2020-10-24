using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;
using TMS_Project.ViewModels;

namespace TMS_Project.Controllers
{
	public class TrainerToTopicsController : Controller
	{
		private ApplicationDbContext _context;

		public TrainerToTopicsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		// GET: TrainerToTopics
		public ActionResult Index(string searchTrainer)
		{
			var trainertotopicss = _context.TrainerToTopics
								   .Include(tr => tr.Topic)
								   .Include(tr => tr.Trainer);

			if (!String.IsNullOrEmpty(searchTrainer))
			{
				trainertotopicss = trainertotopicss.Where(
						s => s.Trainer.UserName.Contains(searchTrainer) ||
						s.Trainer.Email.Contains(searchTrainer));
			}

			return View(trainertotopicss);
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			//Get Account Trainer
			var roleInDb = (from r in _context.Roles where r.Name.Contains("Trainer") select r)
									 .FirstOrDefault();

			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId)
														 .Contains(roleInDb.Id))
														 .ToList();

			//Get Topic
			var topics = _context.Topics.ToList();

			var viewModel = new TrainerToTopicViewModel
			{
				Topics = topics,
				Trainers = users,
				TrainerToTopic = new TrainerToTopic()
			};

			return View(viewModel);
		}


		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(TrainerToTopic trainerToTopic)
		{
			var newTrainerToTopic = new TrainerToTopic
			{
				TrainerId = trainerToTopic.TrainerId,
				TopicId = trainerToTopic.TopicId
			};

			_context.TrainerToTopics.Add(newTrainerToTopic);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var trainerInDb = _context.TrainerToTopics.SingleOrDefault(trdb => trdb.Id == id);
			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			_context.TrainerToTopics.Remove(trainerInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		/*[HttpGet]
		public ActionResult Edit(int id)
		{
			var trainerInDb = _context.TrainerToTopics.SingleOrDefault(trdb => trdb.Id == id);

			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TrainerToTopicViewModel
			{
				TrainerToTopic = trainerInDb,
				Topics = _context.Topics.ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(TrainerToTopic trainerToTopic)
		{
			var trainerInDb = _context.TrainerToTopics.SingleOrDefault(trdb => trdb.Id == trainerToTopic.Id);

			if (trainerInDb == null)
			{
				return HttpNotFound();
			}

			trainerInDb.TopicId = trainerToTopic.TopicId;

			_context.SaveChanges();
			return RedirectToAction("Index");
		}*/

		[HttpGet]
		[Authorize(Roles = "Trainer")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var trainerToTopics = _context.TrainerToTopics
				.Where(c => c.TrainerId == userId)
				.Include(c => c.Topic)
				.Include(c => c.Trainer)
				.ToList();

			return View(trainerToTopics);
		}
	}
}