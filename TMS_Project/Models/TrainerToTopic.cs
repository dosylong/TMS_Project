using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS_Project.Models
{
	public class TrainerToTopic
	{
		public int Id { get; set; }

		[Required]
		public string TrainerId { get; set; }

		public ApplicationUser Trainer { get; set; }
		public int TopicId { get; set; }
		public Topic Topic { get; set; }
	}
}