using System.ComponentModel.DataAnnotations;

namespace CowboyShotout_DataLayer.Models.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)] public string Password { get; set; }
    }
}