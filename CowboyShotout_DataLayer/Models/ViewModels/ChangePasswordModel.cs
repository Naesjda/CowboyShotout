using System.ComponentModel.DataAnnotations;

namespace CowboyShotout_DataLayer.Models.ViewModels
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}