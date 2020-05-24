using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Reflection;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
	public class InMemoryRepository<T> where T:BaseEntity
	{
		public ObjectCache cache = MemoryCache.Default;
		List<T> list;
		string className;
		public InMemoryRepository()
		{
			className = typeof(T).Name.ToString();
			list = cache[className] as List<T>;

			if (list == null)
				list = new List<T>();
		}

		public void Commit()
		{
			cache[className] = list;
		}
		public IQueryable<T> Collection()
		{
			return list.AsQueryable();
		}
		public void Insert(T tEntity)
		{
			list.Add(tEntity);
		}

		public void Update(T tEntity)
		{
			T entityToUpdate = list.FirstOrDefault(x => x.Id == tEntity.Id);
			if (entityToUpdate != null)
				entityToUpdate = tEntity;
			else
				throw new Exception(className + " Entity with Id = " + tEntity.Id.ToString() + " not found "); 

		}

		public T Find(string Id)
		{
			T tEntity = list.FirstOrDefault(x => x.Id == Id);
			if (tEntity != null)
				return tEntity;
			else
				throw new Exception(className + " Entity with Id = " + Id.ToString() + " not found ");

		}

		public void Delete(string Id)
		{
			T tEntityToDelete = list.FirstOrDefault(x => x.Id == Id);
			if (tEntityToDelete != null)
				list.Remove(tEntityToDelete);
			else
				throw new Exception(className + " Entity with Id = " + Id.ToString() + " not found ");

		}
	}
}
