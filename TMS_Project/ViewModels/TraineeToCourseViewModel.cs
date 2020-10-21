using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS_Project.Models;

namespace TMS_Project.ViewModels
{
	public class TraineeToCourseViewModel
	{
		public Course Course { get; set; }
		public TraineeToCourse TraineeToCourse { get; set; }
		public IEnumerable<Course> Courses { get; set; }
		public IEnumerable<ApplicationUser> Trainees { get; set; }

	}
}