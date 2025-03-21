using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Inferastructure.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public GenaricRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity=await _appDbContext.Set<T>().FindAsync(id);
             _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
            
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()=> await _appDbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
           var query=_appDbContext.Set<T>().AsQueryable();

            foreach (var item in includes) 
            { 
            query=query.Include(item);
            }
            return await query.ToListAsync();   
        }

        public async Task<T> GetByIDAsync(int id, params Expression<Func<T, object>>[] includes)
        {
         IQueryable<T>query=_appDbContext.Set<T>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") ==id);
            return entity;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
           _appDbContext.Entry(entity).State = EntityState.Modified;    
            _appDbContext.SaveChanges();
        }
    }
}
