using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace Myshop.DataAccess.InMemory
{
	public class ProductRepository
	{
		ObjectCache cache = MemoryCache.Default;
		List<Product> products;

		public ProductRepository()
		{
			products = cache["Products"] as List<Product>;

			if (products == null)
				products = new List<Product>();

		}

		public void Commit()
		{
			cache["Products"] = products;
		}

		public void Insert(Product product)
		{
			products.Add(product);
		}

		public void Update(Product product)
		{
			Product productToUpdate = products.FirstOrDefault(x => x.Id == product.Id);

			if (productToUpdate != null)
				productToUpdate = product;
			else
				throw new Exception("Product not found");
		}

		public Product Find(string Id)
		{
			Product product = products.FirstOrDefault(x => x.Id == Id);

			if (product != null)
				return product;
			else
				throw new Exception("Product not found");
		}

		public IQueryable Collection()
		{
			return products.AsQueryable();
		}
		public void Delete(string Id)
		{
			Product productToDelete = Find(Id);

			products.Remove(productToDelete);
		}
	}
}
