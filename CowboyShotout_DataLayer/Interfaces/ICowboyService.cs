using System.Threading.Tasks;
using CowboyShotout_DataLayer.Models.Dbo;
using CowboyShotout_DataLayer.Models.ViewModels;

namespace CowboyShotout_DataLayer.Services;

public interface ICowboyService
{
    Task<CowboyModel> GetCowboy(int id);
    Task<CowboyModel> CreateCowboy(CowboyModel cowboyModel);
}