using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.ViewModels
{
	public class ProductManagerViewModel
	{
		public Product Product { get; set; }
		public List<ProductCategory> ProductCategories { get; set; }
	}
}
