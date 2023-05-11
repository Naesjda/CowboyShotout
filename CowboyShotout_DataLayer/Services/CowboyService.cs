using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CowboyShotout_DataLayer.Data;
using CowboyShotout_DataLayer.Interfaces;
using CowboyShotout_DataLayer.Models.Dbo;
using CowboyShotout_DataLayer.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CowboyShotout_DataLayer.Services;
public class CowboyService : ICowboyService
{
    private readonly AppDbContext _dbContext;
    private readonly HttpClient _httpClient;

    public CowboyService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<CowboyModel>> GetAllCowboys()
    {
        // Fetch all cowboys from your database.
        var cowboys = await _dbContext.CowboyModels.Where(x => x.IsValid != 0).ToListAsync();

        return cowboys;
    }

    public async Task<CowboyModel> GetCowboy(int id)
    {
        // Fetch the cowboy from your database using the provided ID.
        var cowboy = await _dbContext.CowboyModels.FirstOrDefaultAsync(x => x.Id == id);

        return cowboy;
    }

    public async Task<CowboyModel> CreateCowboy(CowboyModel cowboyModel)
    {
        _dbContext.CowboyModels.Add(cowboyModel);
        await _dbContext.SaveChangesAsync();

        return cowboyModel;
    }

    public async Task UpdateCowboy(int id, CowboyModel cowboyModel)
    {
        var cb = await _dbContext.CowboyModels.FirstOrDefaultAsync(x => x.Id == id);
        _dbContext.CowboyModels.Update(cowboyModel);
        await _dbContext.SaveChangesAsync();
        if (cb == null)
        {
            throw new Exception("Cowboy not found");
        }

        cb.UpdateDataObject(cowboyModel, _dbContext);

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
        var cowboy = await _dbContext.CowboyModels.FirstOrDefaultAsync(x => x.Id == id);

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

    public async Task<CowboyModel> CreateCowboyAsync()
    {
        var cowboy = new CowboyModel();

        // Get random data for cowboy from OpenAI's GPT-3 API
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions",
            new StringContent(JsonConvert.SerializeObject(new
            {
                prompt =
                    "Generate random cowboy data\nName:\nAge:\nHeight:\nHair:\nPosition:\nHealth:\nSpeed:\nHitrate:\nGunname:\nMax bullets:\nBullets left:\n",
                max_tokens = 1024,
                n = 1,
                temperature = 0.5,
                stop = "\n"
            })));

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(responseBody).choices[0].text.ToString().Split("\n");

        cowboy.Name = GetValueFromPrompt("Name:", data);
        cowboy.Age = int.Parse(GetValueFromPrompt("Age:", data));
        cowboy.Height = double.Parse(GetValueFromPrompt("Height:", data));
        cowboy.Hair = GetValueFromPrompt("Hair:", data);
        // cowboy.Position = new CowboyModel.Position(cowboy.)
        cowboy.Health = GetValueFromPrompt("Health:", data);
        cowboy.Speed = double.Parse(GetValueFromPrompt("Speed:", data));
        cowboy.HitRate = double.Parse(GetValueFromPrompt("Hitrate:", data));

        cowboy.Gun = new GunModel()
        {
            GunName = GetValueFromPrompt("Gunname:", data),
            MaxBullets = int.Parse(GetValueFromPrompt("Max bullets:", data)),
            BulletsLeft = int.Parse(GetValueFromPrompt("Bullets left:", data))
        };

        return cowboy;
    }

    private string GetValueFromPrompt(string prompt, string[] data)
    {
        return data[Array.IndexOf(data, prompt) + 1];
    }
    public async Task<double> DistanceBetweenCowboys(int cowboyId1, int cowboyId2)
    {
        var cowboy1 = await _dbContext.CowboyModels.FirstOrDefaultAsync(x => x.Id == cowboyId1);
        var cowboy2 = await _dbContext.CowboyModels.FirstOrDefaultAsync(x => x.Id == cowboyId2);
        if (cowboy1 == null || cowboy2 == null)
        {
            throw new ArgumentNullException("Both cowboys must be non-null.");
        }


        return cowboy1.Position.DistanceTo(cowboy2.Position);
    }

}
