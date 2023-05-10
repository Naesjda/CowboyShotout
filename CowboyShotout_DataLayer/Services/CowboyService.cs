using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Data.CRUD;
using CowboyShotout_DataLayer.Interfaces.BaseObject;
using CowboyShotout_DataLayer.Models.Dbo;
using CowboyShotout_DataLayer.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CowboyShotout_DataLayer.Services;
public class CowboyService : ICowboyService
{
    private readonly CowboyDbContext _dbContext;
    private readonly ICRUD _crud;


    public CowboyService(CowboyDbContext dbContext, ICRUD crud)
    {
        _dbContext = dbContext;
        _crud = crud;
    }

    public async Task<IEnumerable<CowboyModel>> GetAllCowboys()
    {
        // Fetch all cowboys from your database.
        var cowboys = await _crud.GetAllItemsAsync(_dbContext.CowboyModels);

        return cowboys;
    }

    public async Task<CowboyModel> GetCowboy(int id)
    {
        // Fetch the cowboy from your database using the provided ID.
        var cowboy = await _crud.GetItemByIdAsync(_dbContext.CowboyModels, id);

        return cowboy;
    }

    public async Task<CowboyModel> CreateCowboy(CowboyModel cowboyModel)
    {
        await _crud.AddNewObjectAsync<CowboyModel>(_dbContext.CowboyModels, cowboyModel, _dbContext);

        return cowboyModel;
    }

    public async Task UpdateCowboy(int id, CowboyViewModel cowboyViewModel)
    {
        var cb = await _crud.GetItemByIdAsync(_dbContext.CowboyModels, id);
        var result = _crud.UpdateObject(cowboyViewModel, cb, _dbContext);
        if (cb == null)
        {
            throw new Exception("Cowboy not found");
        }

        cb.UpdateDataObject(cowboyViewModel, _dbContext);

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

    public async Task<double> DistanceBetweenCowboys(int cowboyId1, int cowboyId2)
    {
        var cowboy1 = await _crud.GetItemByIdAsync(_dbContext.CowboyModels, cowboyId1);
        var cowboy2 = await _crud.GetItemByIdAsync(_dbContext.CowboyModels, cowboyId1);
        if (cowboy1 == null || cowboy2 == null)
        {
            throw new ArgumentNullException("Both cowboys must be non-null.");
        }


        return cowboy1.Position.DistanceTo(cowboy2.Position);
    }

}
