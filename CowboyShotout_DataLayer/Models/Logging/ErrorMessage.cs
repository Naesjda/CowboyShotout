using System.ComponentModel.DataAnnotations;

namespace CowboyShotout_DataLayer.Models.Logging
{
    public class ErrorMessage
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string FileName { get; set; }
        public int? FileLine { get; set; }
        public string ExceptionType { get; set; }
    }
}