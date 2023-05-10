using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using CowboyShotout_DataLayer.Models.Dbo;

namespace CowboyShotout_DataLayer.Models.ViewModels;

/// <summary>
/// Only view model for testing purposes
/// </summary>
public class CowboyViewModel : IModel<CowboyModel>
{
    public int Id { get; set; }

    public bool UpdateDataObject(CowboyModel dataObject, CowboyDbContext db)
    {
        throw new System.NotSupportedException();
    }

    public Task<bool> UpdateDataObjectAsync(CowboyModel dataObject, CowboyDbContext db)
    {
        throw new System.NotSupportedException();
    }
}