using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TMS_Project.Models
{
	public class TraineeProfile
	{
		public int Id { get; set; }

		[DisplayName("Trainee ID")]
		public string TraineeId { get; set; }

		public IEnumerable<ApplicationUser> Trainees { get; set; }

		public ApplicationUser Trainee { get; set; }

		[DisplayName("Full Name")]
		public string Full_Name { get; set; }

		public string Education { get; set; }

		[DisplayName("Programming Language")]
		public string Programming_Language { get; set; }

		[DisplayName("Experience Details")]
		public string Experience_Details { get; set; }

		public string Location { get; set; }
	}
}