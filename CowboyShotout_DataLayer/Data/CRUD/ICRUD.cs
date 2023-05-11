using System.Collections.Generic;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Data.CRUD
{
    public interface ICRUD
    {
        T AddNewObject<T>(DbSet<T> dbSet, IModel<T> model, AppDbContext db) where T : class, IEntity, new();
        T CreateDataObject<T>(IModel<T> model, AppDbContext db) where T : class, IEntity, new();

        Task<T> AddNewObjectAsync<T>(DbSet<T> dbSet, IModel<T> model, AppDbContext db)
            where T : class, IEntity, new();

        Task<T> CreateDataObjectAsync<T>(IModel<T> model, AppDbContext db) where T : class, IEntity, new();
        bool UpdateObject<T>(IModel<T> model, T exsistingObject, AppDbContext db) where T : class, IEntity, new();

        Task<bool> UpdateObjectAsync<T>(IModel<T> model, T exsistingObject, AppDbContext db)
            where T : class, IEntity, new();

        Task<bool> AddOrUpdateAsync<T>(AppDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName)
            where T : class, IEntity, new();

        bool AddOrUpdate<T>(AppDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName)
            where T : class, IEntity, new();

        Task<bool> Delete<T>(AppDbContext db, DbSet<T> dbSet, int id)
            where T : class, IEntity, new();

        bool AddOrUpdateMany<TEntity, TModel>(AppDbContext db, DbSet<TEntity> dbSet, List<TModel> models,
            string objectName)
            where TEntity : class, IEntity, new()
            where TModel : IModel<TEntity>;

        bool DeleteMany<T>(AppDbContext db, DbSet<T> dbSet, List<int> ids, string objectName)
            where T : class, IEntity;

        Task<List<T>> GetAllItemsAsync<T>(DbSet<T> dbset) where T : class, IEntity;
        IEnumerable<T> GetItemById<T>(DbSet<T> dbSet, int Id) where T : class, IEntity;
        Task<T> GetItemByIdAsync<T>(DbSet<T> dbSet, int Id) where T : class, IEntity;
        IEnumerable<T> GetAllItems<T>(DbSet<T> dbset) where T : class, IEntity;
        T GetItem<T>(DbSet<T> dbset, T filter) where T : class, IEntity;
    }
}