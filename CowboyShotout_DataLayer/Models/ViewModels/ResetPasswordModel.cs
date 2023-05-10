using System.ComponentModel.DataAnnotations;

namespace CowboyShotout_DataLayer.Models.ViewModels
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }
        [DataType(DataType.Password)] public string ConfirmPassword { get; set; }
    }
}