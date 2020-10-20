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
		public IEnumerable<Course> Courses { get; set; }

	}
}