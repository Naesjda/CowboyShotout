using System;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces.BaseObject;

namespace CowboyShotout_DataLayer.Models.Logging
{
    /// <summary>
    ///     This was made for Demo purposes only.
    /// </summary>
    public class ErrorLog : IModel<ErrorLog>, IEntity
    {
        public ErrorMessage Message { get; set; }
        public byte IsValid { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ChangedByUserId { get; set; }
        public string CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public int? ChangedBy { get; set; }
        public int Id { get; set; }

        public bool UpdateDataObject(ErrorLog dataObject, CowboyDbContext db)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDataObjectAsync(ErrorLog dataObject, CowboyDbContext db)
        {
            throw new NotImplementedException();
        }
    }
}