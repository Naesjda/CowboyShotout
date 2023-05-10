using System;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Models.Dbo;
using CowboyShotout_DataLayer.Models.ViewModels;

namespace CowboyShotout_DataLayer.Services;

public class CowboyService : ICowboyService
{
    private readonly CowboyDbContext _dbContext;

    public CowboyService(CowboyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CowboyModel> GetCowboy(int id)
    {
        var cowboy = await _dbContext.CowboyModels.FindAsync(id);

        return cowboy;
    }

    public async Task<CowboyModel> CreateCowboy(CowboyViewModel cowboyViewModel)
    {
        CowboyModel cowboyModel = ViewModelToModel(cowboyViewModel);

        await _dbContext.CowboyModels.AddAsync(cowboyModel);
        await _dbContext.SaveChangesAsync();

        return cowboyModel;
    }

    private CowboyModel ViewModelToModel(CowboyViewModel cowboyViewModel)
    {
        throw new NotImplementedException();
    }
}