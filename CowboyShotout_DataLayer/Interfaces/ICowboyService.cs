using System.Collections.Generic;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Models.Dbo;

namespace CowboyShotout_DataLayer.Interfaces;

public interface ICowboyService
{
    Task<CowboyModel> GetCowboy(int id);
    Task<CowboyModel> CreateCowboy(CowboyModel cowboyModel);
    Task<IEnumerable<CowboyModel>> GetAllCowboys();
    Task UpdateCowboy(int id, CowboyModel cowboyModel);
    Task DeleteCowboy(int id);
    Task<bool> ShootGun(int id);
    Task ReloadGun(int id);
    Task<CowboyModel> CreateCowboyAsync();
    Task<double> DistanceBetweenCowboys(int cowboyId1, int cowboyId2);
}