using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TMS_Project.Models
{
	public class TraineeToCourse
	{
		public int Id { get; set; }
		public string TraineeId { get; set; }
		public ApplicationUser Trainee { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}