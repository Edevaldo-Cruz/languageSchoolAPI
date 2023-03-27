using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Models
{
    public class LogEntryModel
    {
        [Key]
        public int LogEntryId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }


}
