using System;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces.BaseObject;

namespace CowboyShotout_DataLayer.Models.Dbo;

public class GunModel : IEntity
{
    public string GunName { get; set; }
    public int MaxBullets { get; set; }
    public int BulletsLeft { get; set; }

    public bool UpdateDataObject(GunModel dataObject, AppDbContext db)
    {
        // Here you should implement the logic to update the 'dataObject' using the properties of this model.
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateDataObjectAsync(GunModel dataObject, AppDbContext db)
    {
        // Here you should implement the logic to update the 'dataObject' using the properties of this model.
        throw new NotImplementedException();
    }

    public int Id { get; set; }
    public byte IsValid { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? ChangedAt { get; set; }
}