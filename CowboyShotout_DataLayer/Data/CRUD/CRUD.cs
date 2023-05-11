using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client;
using static System.Console;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace CowboyShotout_DataLayer.Data.CRUD
{
    /// <summary>
    ///     TODO : Needs to implement interface for this class when it is completed.
    /// </summary>
    public class CRUD : ICRUD
    {
        private static CancellationTokenSource _tokenSource;
        private static CancellationToken _token;
        private readonly ILogger _logger = new Logger<ICRUD>(new NullLoggerFactory());

        public T AddNewObject<T>(DbSet<T> dbSet, IModel<T> model, AppDbContext db) where T : class, IEntity, new()
        {
            var newObject = CreateDataObject(model, db);
            newObject.CreatedTime = DateTime.Now;

            _ = dbSet.Add(newObject);
            return newObject;
        }

        public T CreateDataObject<T>(IModel<T> model, AppDbContext db) where T : class, IEntity, new()
        {
            var dataObject = new T();
            dataObject.IsValid = 1;
            _ = model.UpdateDataObject(dataObject, db);
            return dataObject;
        }

        public async Task<T> AddNewObjectAsync<T>(DbSet<T> dbSet, IModel<T> model, AppDbContext db)
            where T : class, IEntity, new()
        {
            var newObject = await CreateDataObjectAsync(model, db);
            newObject.CreatedTime = DateTime.Now;
            _ = await dbSet.AddAsync(newObject);
            return newObject;
        }

        public async Task<T> CreateDataObjectAsync<T>(IModel<T> model, AppDbContext db)
            where T : class, IEntity, new()
        {
            var dataObject = new T();
            dataObject.IsValid = 1;
            await model.UpdateDataObjectAsync(dataObject, db);
            return dataObject;
        }

        public bool UpdateObject<T>(IModel<T> model, T exsistingObject, AppDbContext db)
            where T : class, IEntity, new()
        {
            exsistingObject.ChangedAt = DateTime.Now;
            var taskSuccess = model.UpdateDataObject(exsistingObject, db);
            return taskSuccess;
        }

        public async Task<bool> UpdateObjectAsync<T>(IModel<T> model, T exsistingObject, AppDbContext db)
            where T : class, IEntity, new()
        {
            exsistingObject.ChangedAt = DateTime.Now;
            await model.UpdateDataObjectAsync(exsistingObject, db);
            return true;
        }

        public async Task<bool> AddOrUpdateAsync<T>(AppDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName) where T : class, IEntity, new()
        {
            //New object 
            if (model.Id <= 0)
                try
                {
                    var newObject = AddNewObject(dbSet, model, db);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error,
                        "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                    return false;
                }

            try
            {
                var exsistingObject = dbSet.FirstOrDefault(a => a.Id == model.Id);
                if (exsistingObject != null)
                {
                    var result = UpdateObject(model, exsistingObject, db);
                    if (result)
                    {
                        var saveRes = await db.SaveChangesAsync();
                    }
                }
            }
            catch (DbException ex)
            {
                WriteLine(ex);
                return false;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return false;
            }

            return true;
            // var a = db.ChangeTracker.Context.Database.CurrentTransaction;
            // var lagret = db.SaveChanges();
            // if (lagret > 0) return true;
            // //Exsisting object 
            // else
            // {
            //     return false;
            // }
        }

        public bool AddOrUpdate<T>(AppDbContext db, DbSet<T> dbSet, IModel<T> model,
            string objectName) where T : class, IEntity, new()
        {
            //New object 
            if (model.Id <= 0)
                try
                {
                    var newObject = AddNewObject(dbSet, model, db);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error,
                        "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);

                    return false;
                }

            try
            {
                var exsistingObject = dbSet.FirstOrDefault(a => a.Id == model.Id);
                if (exsistingObject != null)
                {
                    var result = UpdateObject(model, exsistingObject, db);
                    var saveRes = db.SaveChanges();
                }
            }
            catch (DbException ex)
            {
                WriteLine(ex);
                return false;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Delete<T>(AppDbContext db, DbSet<T> dbSet, int id)
            where T : class, IEntity, new()
        {
            try
            {
                var item = dbSet.FirstOrDefault(x => x.Id == id);
                if (item == null) return false;

                item.IsValid = 0;
                item.ChangedAt = DateTime.Now;
                _ = await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return false;
            }
        }

        public bool AddOrUpdateMany<TEntity, TModel>(AppDbContext db, DbSet<TEntity> dbSet, List<TModel> models,
            string objectName) where TEntity : class, IEntity, new() where TModel : IModel<TEntity>
        {
            var modelSet = new ConcurrentBag<TModel>(models);
            try
            {
                lock (modelSet)
                {
                    var opt = new ParallelOptions();
                    _tokenSource = new CancellationTokenSource();
                    _token = _tokenSource.Token;
                    opt.MaxDegreeOfParallelism = 10;
                    opt.CancellationToken = _token;


                    // var task = Parallel.For(0, models.Count, opt, async i =>
                    var task = Parallel.ForEach(modelSet, model =>
                    {
                        opt.CancellationToken.ThrowIfCancellationRequested();
                        if (model.Id <= 0)
                        {
                            try
                            {
                                lock (dbSet)
                                {
                                    var newObject = AddNewObject(dbSet, model, db);
                                }
                            }
                            catch (Exception e)
                            {
                                _logger.Log(LogLevel.Error,
                                    "Exception message : " + e.Message + " InnerException : " +
                                    e.InnerException.Message);
                            }
                        }
                        else
                        {
                            var exsisitingObject = dbSet.FirstOrDefault(x => x.Id == model.Id);
                            if (exsisitingObject != null)
                                try
                                {
                                    lock (dbSet)
                                    {
                                        var exsisitingObjectStatus = UpdateObject(model, exsisitingObject, db);
                                    }
                                }
                                catch (Exception e)
                                {
                                    _logger.Log(LogLevel.Error,
                                        "Exception message : " + e.Message + " InnerException : " +
                                        e.InnerException.Message);
                                }
                        }
                    });
                    if (task.IsCompleted)
                    {
                        var a = db.SaveChanges();
                    }

                    return task.IsCompleted;
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return false;
            }
        }

        public bool DeleteMany<T>(AppDbContext db, DbSet<T> dbSet, List<int> ids, string objectName)
            where T : class, IEntity
        {
            try
            {
                var entities = dbSet.Where(x => ids.Contains(x.Id)).ToList();
                entities.ForEach(x => x.IsValid = 0);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return false;
            }
        }

        public async Task<List<T>> GetAllItemsAsync<T>(DbSet<T> dbset) where T : class, IEntity
        {
            try
            {
                var items = await dbset.Where(x => x.IsValid == (byte)1).ToListAsync();
                return items;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,
                    "Exception message : " + e.Message + " InnerException : " + e.InnerException.Message);
                return null;
            }
        }


        public IEnumerable<T> GetAllItems<T>(DbSet<T> dbset) where T : class, IEntity
        {
            var items = dbset.Where(x => x.IsValid == (byte)1).ToList();
            return items;
        }

        public IEnumerable<T> GetItemById<T>(DbSet<T> dbSet, int Id) where T : class, IEntity
        {
            var Items = dbSet.Where(x => x.Id == Id && x.IsValid == (byte)1).ToList();
            if (Items.Count >= 1)
                return Items;
            return new List<T>();
        }

        public async Task<T> GetItemByIdAsync<T>(DbSet<T> dbSet, int Id) where T : class, IEntity
        {
            var Items = await dbSet.FirstOrDefaultAsync(x => x.Id == Id && x.IsValid == (byte)1);
            if (Items.Id > 0)
                return Items;
            return null;
        }

        public T GetItem<T>(DbSet<T> dbset, T filter) where T : class, IEntity
        {
            var type = filter.GetType();

            if (type == typeof(int))
            {
                var item = dbset.Where(x => x.Id == Convert.ToInt32(filter));
                return item.FirstOrDefault();
            }

            if (type == typeof(string))
            {
                var item = dbset.Where(x => x.Id.ToString() == filter.ToString());
                return item.FirstOrDefault();
            }

            return filter;
        }
    }
}