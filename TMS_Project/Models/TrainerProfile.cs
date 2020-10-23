using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TMS_Project.Models
{
	public class TrainerProfile
	{
		public int Id { get; set; }

		[DisplayName("Trainer ID")]
		public string TrainerId { get; set; }
		public IEnumerable<ApplicationUser> Trainers { get; set; }

		public ApplicationUser Trainer { get; set; }

		[DisplayName("Full Name")]
		public string Full_Name { get; set; }

		[DisplayName("External or Internal Type")]
		public string External_Internal { get; set; }

		public string Education { get; set; }

		[DisplayName("Working Place")]
		public string Working_Place { get; set; }

		[DisplayName("Phone Number")]
		public int Phone_Number { get; set; }

	}
}