using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;

namespace CowboyShotout_DataLayer.Interfaces.BaseObject
{
    public interface IModel<in T> where T : IEntity
    {
        int Id { get; set; }
        bool UpdateDataObject(T dataObject, CowboyDbContext db);
        Task UpdateDataObjectAsync(T dataObject, CowboyDbContext db);
    }
}