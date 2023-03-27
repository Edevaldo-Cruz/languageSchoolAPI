using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Models
{
    public class ClassroomModel
    {
        [Key]
        public int ClassroomId { get; set; }
        public string Course { get; set; }
        public int Teacher { get; set; }
        public int ProficiencyLevel { get; set; }
        public string Time { get; set; }
        public string Language { get; set; }
        public string RoomNumber { get; set; }
    }

}
