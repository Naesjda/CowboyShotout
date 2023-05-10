namespace CowboyShotout_DataLayer.Models.Security
{
    public abstract class ExtendedUserModel //: IdentityUser
    {
        public string Location { get; set; } = "No-NB";

        public string OrgId { get; set; }
    }
}