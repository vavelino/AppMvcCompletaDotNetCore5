
using DevIO.Business.Models;
using DevIO.Data.Context;
using DioIO.Business.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity,new()
    {
        protected readonly MyDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MyDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        // DbSet
        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            //As no Trackin tem mais performace não coloca na mémoria para ser rastreado. É apenas leitura
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> GetByID(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<List<TEntity>> GetAll()
        {
            // return await DbSet.AsNoTracking().ToListAsync();
            return await DbSet.ToListAsync();
        }
        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            //DbSet.Remove(await DbSet.FindAsync(id));
            //var entity = new TEntity { Id = id };
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            Db?.Dispose();
            //? => Se ele existir faz o dispose se não existir não faca nada
            // Evitar exception
        }
    }
}
