// See https://aka.ms/new-console-template for more information

using Cowboyshotout_APIs.Controllers;

Cowboyshotout_APIs.Controllers.CowboyApi api = new CowboyApi("");
var cowboy = api.CreateCowboyAsync();
Console.WriteLine(cowboy);
Console.ReadLine();