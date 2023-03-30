using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Models
{
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string GenderId { get; set; }
        public string Nationality { get; set; }
        public string Observation { get; set; }
    }
}
