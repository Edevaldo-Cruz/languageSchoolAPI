using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Models
{
    public class TeacherModel
    {
        [Key]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo CPF deve conter exatamente 11 caracteres.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo Telefone deve conter no máximo 20 caracteres.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo Endereço deve conter no máximo 200 caracteres.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Email deve conter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "O campo Nacionalidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nacionalidade deve conter no máximo 100 caracteres.")]
        public string Nationality { get; set; }

        [StringLength(500, ErrorMessage = "O campo Observação deve conter no máximo 500 caracteres.")]
        public string Observation { get; set; }
    }

}
