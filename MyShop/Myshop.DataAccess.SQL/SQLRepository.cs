using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.DataAccess.SQL
{
	public class SQLRepository<T> : IRepository<T> where T : BaseEntity
	{
		DataContext context;
		DbSet<T> dbSet;

		public SQLRepository(DataContext context)
		{
			this.context = context;
			dbSet = context.Set<T>();
		}
		public IQueryable<T> Collection()
		{
			return dbSet;
		}

		public void Commit()
		{
			context.SaveChanges();
		}

		public void Delete(string Id)
		{
			T t = Find(Id);
			if (context.Entry(t).State == EntityState.Detached)
				dbSet.Attach(t);

			dbSet.Remove(t);
		}

		public T Find(string Id)
		{
			T t = dbSet.FirstOrDefault<T>(x => x.Id == Id);

			if (t == null)
				throw new Exception("Not found");
			else
				return t;
		}

		public void Insert(T tEntity)
		{
			dbSet.Add(tEntity);
		}

		public void Update(T tEntity)
		{
			dbSet.Attach(tEntity);
			context.Entry(tEntity).State = EntityState.Modified;
		}
	}
}
