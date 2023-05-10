using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Models.Dbo;
using CowboyShotout_DataLayer.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Services;
public class CowboyService : ICowboyService
{
    private readonly CowboyDbContext _dbContext;

    public CowboyService(CowboyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CowboyModel>> GetAllCowboys()
    {
        // Fetch all cowboys from your database.
        var cowboys = await _dbContext.CowboyModels.ToListAsync();

        return cowboys;
    }

    public async Task<CowboyModel> GetCowboy(int id)
    {
        // Fetch the cowboy from your database using the provided ID.
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

    public async Task UpdateCowboy(int id, CowboyViewModel cowboyViewModel)
    {
        var cowboy = await _dbContext.CowboyModels.FindAsync(id);

        if (cowboy == null)
        {
            throw new Exception("Cowboy not found");
        }

        cowboy.UpdateDataObject(cowboyViewModel, _dbContext);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCowboy(int id)
    {
        var cowboy = await _dbContext.CowboyModels.FindAsync(id);

        if (cowboy == null)
        {
            throw new Exception("Cowboy not found");
        }

        _dbContext.CowboyModels.Remove(cowboy);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ShootGun(int id)
    {
        var cowboy = await _dbContext.CowboyModels.FindAsync(id);

        if (cowboy == null)
        {
            throw new Exception("Cowboy not found");
        }

        if (cowboy.Gun.BulletsLeft == 0)
        {
            return false;
        }

        cowboy.Gun.BulletsLeft--;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task ReloadGun(int id)
    {
        var cowboy = await _dbContext.CowboyModels.FindAsync(id);

        if (cowboy == null)
        {
            throw new Exception("Cowboy not found");
        }

        cowboy.Gun.BulletsLeft = cowboy.Gun.MaxBullets;

        await _dbContext.SaveChangesAsync();
    }

    private CowboyModel ViewModelToModel(CowboyViewModel cowboyViewModel)
    {
        // Implement the conversion from ViewModel to Model here.
        // This is a placeholder and needs to be replaced with actual conversion logic
        // if front-end is to be implemented.
        throw new NotImplementedException();
    }
}
