using System.ComponentModel.DataAnnotations;
public class StudentModel
{
    [Key]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [StringLength(11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [StringLength(20, ErrorMessage = "O campo Telefone deve ter no máximo 20 caracteres.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo Endereço deve ter no máximo 200 caracteres.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo E-mail deve ter no máximo 100 caracteres.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    public DateTime Birthdate { get; set; }

    [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
    public int GenderId { get; set; }
    //public Gender Gender { get; set; }    

    [Required(ErrorMessage = "O campo Nacionalidade é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nacionalidade deve ter no máximo 100 caracteres.")]
    public string Nationality { get; set; }

    [StringLength(500, ErrorMessage = "O campo Observação deve ter no máximo 500 caracteres.")]
    public string Observation { get; set; }
    public bool English { get; set; }
    public int ProficiencyLevelEnglish { get; set; }
    public bool Spanish { get; set; }
    public int ProficiencyLevelSpanish { get; set; }
    public bool French { get; set; }
    public int ProficiencyLevelFrench { get; set; }

    
}

