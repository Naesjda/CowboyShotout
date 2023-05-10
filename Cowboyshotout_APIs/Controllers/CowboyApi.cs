using CowboyShotout_DataLayer.Models.Dbo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cowboyshotout_APIs.Controllers;

[Microsoft.AspNetCore.Components.Route("api/v1/Cowboy")] //api/v1/register
public class CowboyApi
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _openaiApiKey;

    public CowboyApi(string openaiApiKey)
    {
        _openaiApiKey = openaiApiKey;
    }

    [HttpPost("createcowboy")]
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
}