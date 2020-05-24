using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
	public class ProductCategoryRepository
	{
		ObjectCache cache = MemoryCache.Default;
		List<ProductCategory> categories;
		/// <summary>
		/// Initialises new instance of <b>ProductCategoryRepository</b> class.
		/// </summary>
		public ProductCategoryRepository()
		{
			categories = cache["ProductCategory"] as List<ProductCategory>;
			if (categories == null)
				categories = new List<ProductCategory>();
		}

		public void Commit()
		{
			cache["ProductCategory"] = categories;
		}

		public IQueryable<ProductCategory> Collection()
		{
			return categories.AsQueryable();
		}

		public void Insert(ProductCategory productCategory)
		{
			categories.Add(productCategory);
		}

		public void Update(ProductCategory productCategory)
		{
			ProductCategory cateGoryToUpdate = Find(productCategory.Id);
			cateGoryToUpdate= productCategory;
		}

		public void Delete(string Id)
		{
			ProductCategory cateGoryToDelete = Find(Id);
			categories.Remove(cateGoryToDelete);
		}

		public ProductCategory Find(string Id)
		{
			ProductCategory productCategory = categories.FirstOrDefault(x => x.Id == Id);

			if (productCategory == null)
				throw new Exception("Product not found");
			else
				return productCategory;
		}

	}
}
