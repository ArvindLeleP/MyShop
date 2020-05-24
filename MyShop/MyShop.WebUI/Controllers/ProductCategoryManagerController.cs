using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
namespace MyShop.WebUI.Controllers
{
	public class ProductCategoryManagerController : Controller
	{
		InMemoryRepository<ProductCategory> context;
		public ProductCategoryManagerController()
		{
			context = new InMemoryRepository<ProductCategory>();
		}
		// GET: ProductCategoryManager
		public ActionResult Index()
		{
			List<ProductCategory> categories = context.Collection().ToList();
			return View(categories);
		}

		public ActionResult Create()
		{
			ProductCategory productCategory = new ProductCategory();
			return View(productCategory);
		}

		[HttpPost]
		public ActionResult Create(ProductCategory productCategory)
		{
			if (ModelState.IsValid)
			{
				context.Insert(productCategory);
				context.Commit();
				return RedirectToAction("Index");
			}
			else
			{
				return View(productCategory);
			}
		}

		public ActionResult Edit(ProductCategory productCategory)
		{
			ProductCategory productCategoryToEdit = context.Find(productCategory.Id);
			if (productCategoryToEdit == null)
				return HttpNotFound();
			else
				return View(productCategoryToEdit);


		}

		[HttpPost]
		public ActionResult Edit(ProductCategory productCategory, string Id)
		{
			ProductCategory productCategoryToEdit = context.Find(Id);
			if (productCategoryToEdit == null)
				return HttpNotFound();
			else
			{
				if (ModelState.IsValid)
				{
					productCategoryToEdit.Category = productCategory.Category;
					context.Commit();
					return RedirectToAction("Index");
				}
				else
					return View(productCategory);
			}


		}

		public ActionResult Delete(string Id)
		{
			ProductCategory productCategoryToDelete = context.Find(Id);
			if (productCategoryToDelete == null)
				return HttpNotFound();
			else
				return View(productCategoryToDelete);


		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(string Id)
		{
			ProductCategory productCategoryToDelete = context.Find(Id);
			if (productCategoryToDelete == null)
				return HttpNotFound();
			else
			{
				context.Delete(Id);
				context.Commit();
				return RedirectToAction("Index");
			}
		}
	}
}