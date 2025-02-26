﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;
using TMS_Project.ViewModels;

namespace TMS_Project.Controllers
{
	public class TopicsController : Controller
	{
		private ApplicationDbContext _context;

		public TopicsController()
		{
			_context = new ApplicationDbContext();
		}

		// Topics/Index
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index(string searchTopic)
		{
			var topics = _context.Topics.Include(t => t.Course);

			if (!String.IsNullOrEmpty(searchTopic))
			{
				topics = topics.Where(
					s => s.Name.Contains(searchTopic) ||
					s.Course.Name.Contains(searchTopic));
			}

			return View(topics);
		}

		// Create Topic (Topics/Create)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			var viewModel = new TopicCourseViewModel
			{
				Courses = _context.Courses.ToList()
			};
			return View(viewModel);
		}


		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(Topic topic)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckTopicConditions/CreateNullTopic.cshtml");
			}

			//Check if Topic Name existed or not
			if (_context.Topics.Any(c => c.Name == topic.Name &&
										  c.CourseId == topic.CourseId))
			{
				return View("~/Views/CheckTopicConditions/CreateExistTopic.cshtml");
			}

			var newTopic = new Topic
			{
				Name = topic.Name,
				Descriptions = topic.Descriptions,
				CourseId = topic.CourseId,
			};

			_context.Topics.Add(newTopic);
			_context.SaveChanges();

			return View("~/Views/CheckTopicConditions/CreateTopicSuccess.cshtml");
		}

		// Edit Topic (Topics/Edit/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TopicCourseViewModel
			{
				Topic = topicInDb,
				Courses = _context.Courses.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(Topic topic)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckTopicConditions/EditNullTopic.cshtml");
			}

			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == topic.Id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			topicInDb.Name = topic.Name;
			topicInDb.Descriptions = topic.Descriptions;
			topicInDb.CourseId = topic.CourseId;

			_context.SaveChanges();
			return View("~/Views/CheckTopicConditions/EditTopicSuccess.cshtml");
		}

		// Delete Topic (Topics/Delete/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == id);

			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			_context.Topics.Remove(topicInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}