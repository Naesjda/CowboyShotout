using System.ComponentModel.DataAnnotations;

namespace CowboyShotout_DataLayer.Models.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}