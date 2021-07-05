using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DioIO.Business.Interface
{
    // Usando Pattern Repository
    // Repositório genérico, server para qualquer entidade
    // Ele está na camada de negócio pq a camada de negócio não conhece a camada de dados
    public interface IRepository<TEntity>
        : IDisposable // Realize dispose para liberar memória
        where TEntity : Entity // Essa TEntity só pode ser utilizada
                               // se ela for uma classe filha de Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetByID(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        //Possibilita colocar como parametro uma função lambida para pesquisar elementos 
        Task<int> SaveChanges();  // Retorna a quantidade de linhas afetadas
    }
}
