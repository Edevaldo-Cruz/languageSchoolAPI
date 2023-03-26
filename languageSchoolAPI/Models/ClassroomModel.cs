namespace languageSchoolAPI.Models
{
    public class ClassroomModel
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Teacher { get; set; }
        public int ProficiencyLevel { get; set; }
        public string Schedule { get; set; }
        public string Language { get; set; }
        public string RoomNumber { get; set; }
    }

}
