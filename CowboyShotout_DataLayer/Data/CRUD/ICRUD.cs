using System.Collections.Generic;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Models.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Interfaces.BaseObject
{
    public interface ICRUD
    {
        T AddNewObject<T>(DbSet<T> dbSet, IModel<T> model, CowboyDbContext db) where T : class, IEntity, new();
        T CreateDataObject<T>(IModel<T> model, CowboyDbContext db) where T : class, IEntity, new();

        Task<T> AddNewObjectAsync<T>(DbSet<T> dbSet, IModel<T> model, CowboyDbContext db)
            where T : class, IEntity, new();

        Task<T> CreateDataObjectAsync<T>(IModel<T> model, CowboyDbContext db) where T : class, IEntity, new();
        bool UpdateObject<T>(IModel<T> model, T exsistingObject, CowboyDbContext db) where T : class, IEntity, new();

        Task<bool> UpdateObjectAsync<T>(IModel<T> model, T exsistingObject, CowboyDbContext db)
            where T : class, IEntity, new();

        Task<bool> AddOrUpdateAsync<T>(CowboyDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName)
            where T : class, IEntity, new();

        bool AddOrUpdate<T>(CowboyDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName)
            where T : class, IEntity, new();

        Task<bool> Delete<T>(CowboyDbContext db, DbSet<T> dbSet, int id)
            where T : class, IEntity, new();

        bool AddOrUpdateMany<TEntity, TModel>(CowboyDbContext db, DbSet<TEntity> dbSet, List<TModel> models,
            string objectName)
            where TEntity : class, IEntity, new()
            where TModel : IModel<TEntity>;

        bool DeleteMany<T>(CowboyDbContext db, DbSet<T> dbSet, List<int> ids, string objectName)
            where T : class, IEntity;

        Task<List<T>> GetAllItemsAsync<T>(DbSet<T> dbset) where T : class, IEntity;
        IEnumerable<T> GetItemById<T>(DbSet<T> dbSet, int Id) where T : class, IEntity;
        Task<T> GetItemByIdAsync<T>(DbSet<T> dbSet, int Id) where T : class, IEntity;
        IEnumerable<T> GetAllItems<T>(DbSet<T> dbset) where T : class, IEntity;
        T GetItem<T>(DbSet<T> dbset, T filter) where T : class, IEntity;
    }
}