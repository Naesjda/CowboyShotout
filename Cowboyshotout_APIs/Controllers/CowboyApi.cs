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

    
}