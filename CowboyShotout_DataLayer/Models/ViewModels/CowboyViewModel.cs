using System;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using CowboyShotout_DataLayer.Models.Dbo;

namespace CowboyShotout_DataLayer.Models.ViewModels;

/// <summary>
/// Only view model for testing purposes
/// </summary>
public class CowboyViewModel : CowboyModel, IEntity
{
    public int Id { get; set; }
    public DateTime? CreatedTime { get; set; }

    public void UpdateDataObject(CowboyModel dataObject, CowboyDbContext db)
    {
        this.Health = dataObject.Health;
    }

    public Task<bool> UpdateDataObjectAsync(CowboyModel dataObject, CowboyDbContext db)
    {
        throw new System.NotSupportedException();
    }
}