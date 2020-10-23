using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS_Project.Models;

namespace TMS_Project.ViewModels
{
	public class TrainerToTopicViewModel
	{
		public Topic Topic { get; set; }

		public TrainerToTopic TrainerToTopic { get; set; }

		public IEnumerable<Topic> Topics { get; set; }

		public IEnumerable<ApplicationUser> Trainers { get; set; }
	}
}