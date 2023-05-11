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

    public void UpdateDataObject(CowboyModel dataObject, AppDbContext db)
    {
        dataObject.Name = this.Name;
        dataObject.Age = this.Age;
        dataObject.Height = this.Height;
        dataObject.Hair = this.Hair;
        dataObject.Health = this.Health;
        dataObject.Speed = this.Speed;
        dataObject.HitRate = this.HitRate;
        dataObject.Gun = this.Gun;
    }

    public Task<bool> UpdateDataObjectAsync(CowboyModel dataObject, AppDbContext db)
    {
        throw new System.NotSupportedException();
    }
}