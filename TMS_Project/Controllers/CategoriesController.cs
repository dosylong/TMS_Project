using System;
using System.Linq;
using System.Web.Mvc;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
	public class CategoriesController : Controller
	{
		private ApplicationDbContext _context;

		public CategoriesController()
		{
			_context = new ApplicationDbContext();
		}

		// Categories/Index
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index(string searchCategory)
		{
			var categories = _context.Categories.ToList();

			if (!String.IsNullOrEmpty(searchCategory))
			{
				categories = categories.FindAll(s => s.Name.Contains(searchCategory));
			}

			return View(categories);
		}

		// Create Category (Categories/Create)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckConditions/NullCategory.cshtml");
			}

			//Check if Category Name existed or not
			if (_context.Categories.Any(c => c.Name.Contains(category.Name)))
			{
				return View("~/Views/CheckConditions/CreateCategoryExist.cshtml");
			}

			var newCategory = new Category
			{
				Name = category.Name,
				Descriptions = category.Descriptions
			};

			_context.Categories.Add(newCategory);
			_context.SaveChanges();
			return View("~/Views/CheckConditions/CreateCategorySuccess.cshtml");


		}

		// Edit Category (Categories/Edit/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}
			return View(categoryInDb);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(Category category)
		{

			if (!ModelState.IsValid)
			{
				return View("~/Views/CheckConditions/NullCategory.cshtml");
			}

			//Check if Category Name existed or not
			if (_context.Categories.Any(c => c.Name.Contains(category.Name)))
			{
				return View("~/Views/CheckConditions/EditCategoryExist.cshtml");
			}

			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			_context.SaveChanges();
			return View("~/Views/CheckConditions/EditCategorySuccess.cshtml");
		}

		// Delete Category (Categories/Delete/Id/...)
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			_context.Categories.Remove(categoryInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}