using System;

namespace CowboyShotout_DataLayer.Interfaces.BaseObject
{
    public interface IEntity
    {
        public int Id { get; set; }
        public byte IsValid { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ChangedByUserId { get; set; }
    }
}