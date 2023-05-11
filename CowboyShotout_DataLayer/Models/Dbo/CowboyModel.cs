using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using CowboyShotout_DataLayer.Models.ViewModels;
using WebSharper.JavaScript.Geolocation;

namespace CowboyShotout_DataLayer.Models.Dbo;

public class CowboyModel : IModel<CowboyModel>, IEntity
{
    public int Id { get; set; }

    public bool UpdateDataObject(CowboyModel dataObject, AppDbContext db)
    {
        return false;
    }

    public Task UpdateDataObjectAsync(CowboyModel dataObject, AppDbContext db)
    {
        return null;
    }

    public byte IsValid { get; set; }
    public DateTime? ChangedAt { get; set; }
    public string CreatedBy { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? ChangedByUserId { get; set; }
    public DateTime? CreatedTime { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public string Hair { get; set; }
    public string Health { get; set; }
    public double Speed { get; set; }
    public double HitRate { get; set; }
    public GunModel Gun { get; set; }
    [NotMapped] public virtual Position Position { get; set; }

    /// <summary>
    /// Examplary usage:
    /// Position p1 = new Position(1.0, 2.0, 3.0);
    /// Position p2 = new Position(4.0, 5.0, 6.0);
    /// double distance = p1.DistanceTo(p2);
    /// Console.WriteLine("Distance between p1 and p2: {0}", distance);
    /// Distance between p1 and p2: 5.196152422706632
    /// </summary>

    
    
}

public class Position
{
    
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Position(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double DistanceTo(Position other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        double dz = Z - other.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}
