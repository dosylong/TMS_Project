using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS_Project.Models
{
	public class TraineeProfile
	{
		public int Id { get; set; }
		public string TraineeId { get; set; }
		public IEnumerable<ApplicationUser> Trainees { get; set; }
		public ApplicationUser Trainee { get; set; }
		public string Full_Name { get; set; }
		public string Education { get; set; }
		public string Programming_Language { get; set; }
		public string Experience_Details { get; set; }
		public string Location { get; set; }
	}
}