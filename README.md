languageSchoolAPI

Esta é uma API desenvolvida em C# usando o framework ASP.NET Core para gerenciar escolas de idiomas.
Configuração
Para configurar o projeto, siga os passos abaixo:
1.	Clone o repositório do projeto para a sua máquina local. 

https://github.com/Edevaldo-Cruz/languageSchoolAPI.git

2.	Abra o projeto em um editor de código de sua escolha (exemplo: Visual Studio, VSCode, etc.).

3.	Configure a string de conexão com o banco de dados no arquivo appsettings.json.

4.	Execute as migrações para criar as tabelas do banco de dados.

Atenção, para esse projeto foram criados dois bancos de dados, é recomendado executar os seguintes comandos no terminal para o funcionamento correto da aplicação.

•	dotnet ef migrations add <nomeDaMigração> --context LanguageSchoolContext
•	dotnet ef database update --context LanguageSchoolContext

•	dotnet ef migrations add <nomeDaMigração> --context LogEntryContext
•	dotnet ef database update --context LogEntryContext

5.	Execute a API usando o comando dotnet run no terminal.

Endpoints disponíveis
A API disponibiliza os seguintes endpoints:
Estudantes
StudentsController é responsável por lidar com as solicitações referentes aos alunos. Possui os seguintes métodos:
•	CreateStudent: método HTTP POST que recebe um objeto StudentModel e cria um novo registro de aluno no banco de dados. Realiza validação do objeto e verifica se o aluno deve ser matriculado em turmas de inglês, espanhol ou francês. Em caso positivo, cria uma matrícula no banco de dados por meio do método CreateEnrollment da classe EnrollmentController e registra uma entrada no log do sistema.

•	EditStudent: método HTTP PUT que recebe um objeto StudentModel e um identificador de aluno id e atualiza os dados do aluno no banco de dados, realiza validação e registra uma entrada no log do sistema.


•	GetAllStudents: método HTTP GET que retorna uma lista de todos os registros de alunos armazenados no banco de dados.

•	GetStudentById: método HTTP GET que recebe um identificador de aluno id e retorna o registro correspondente do banco de dados.

•	HttpGet("GetStudentByName/{name}"): este endpoint é responsável por receber uma requisição HTTP GET e retorna uma lista de objetos StudentModel que contém o nome passado como parâmetro na URL. Ele utiliza o método Where para filtrar os alunos que contêm o nome informado, retorna uma mensagem de erro caso não encontre nenhum aluno.

•	HttpDelete("DeleteStudent/{id}"): este endpoint é responsável por receber uma requisição HTTP DELETE e remover o aluno do banco de dados que possui o ID informado como parâmetro na URL. Ele verifica se o aluno existe no banco de dados antes de remover e retorna uma mensagem de erro caso não encontre o aluno. Além disso, este endpoint faz uso de um método CreateLogEntry para registra a exclusão do registro na tabela LogEntry.

•	ValidateStudent: este é um método privado que é chamado internamente para validar um objeto StudentModel. Ele recebe como parâmetro o objeto StudentModel que deve ser validado e um parâmetro opcional studentId que representa o ID do aluno que está sendo atualizado. Este método verifica se já existe um aluno com o mesmo CPF e retorna uma mensagem de erro caso exista. Além disso, ele também verifica se o gênero do aluno é válido e retorna uma mensagem de erro caso não seja. Este método é utilizado internamente por outros métodos do controlador para garantir a validação dos dados do aluno antes de inseri-lo ou atualizá-lo no banco de dados.


Professores
TeachersController é um controle que gerencia as solicitações HTTP para manipular dados de professores no banco de dados. Ela permite criar, atualizar, excluir e recuperar dados de professores. Além disso, a classe realiza validações dos dados e registra as ações em um log de eventos. 
•	CreateTeacher: método POST que recebe um objeto TeacherModel e tenta adicioná-lo ao banco de dados. Primeiro, ela valida os dados do professor usando a função de validação ValidateTeacher e retorna um erro de validação se os dados não estiverem corretos. Caso contrário, ela adiciona o professor ao contexto do banco de dados, salva as alterações e registra uma entrada de log.

•	EditTeacher: método PUT que recebe um id e um objeto TeacherModel e tenta atualizar os dados do professor correspondente no banco de dados. Primeiro, ela recupera o registro do professor do banco de dados usando o id fornecido. Em seguida, ela valida os dados do professor usando a função ValidateTeacher e retorna um erro de validação se os dados não estiverem corretos. Se o professor não for encontrado, ela retorna um BadRequest. Caso contrário, ela atualiza o registro do professor no contexto do banco de dados, salva as alterações e registra uma entrada de log .

•	GetAllTeachers: método GET que retorna uma lista de todos os professores no banco de dados.

•	GetTeacherById: método GET que recebe um id e retorna os dados do professor correspondente.

•	GetTeacherByName: método GET que recebe um nome e retorna uma lista de todos os professores que contêm o nome fornecido em seu nome.

•	TeacherRecord: método GET que recebe um id como entrada e retorna um objeto TeacherDetailsViewModel que contém informações detalhadas do professor e todas as salas de aula que ele ensina. Se o professor não for encontrado, ele retorna uma resposta 404 Not Found.

•	DeleteTeacher: método DELETE que recebe um id e exclui o registro do professor correspondente no banco de dados. Se o professor não for encontrado, ela retorna um BadRequest. Caso contrário, ela exclui o registro do professor no contexto do banco de dados, salva as alterações e registra uma entrada de log .
Além disso, há uma função de validação de professor chamada ValidateTeacher, que valida se os dados do professor estão corretos antes de adicionar ou atualizar o registro no banco de dados. Ele verifica se o CPF do professor não está duplicado e se o ID do gênero fornecido está correto.

Turmas
ClassroomsController é a Controller responsável por gerenciar as operações relacionadas às turmas, incluindo a criação, edição e busca de turmas existentes.
•	CreateClassroom: Método responsável por criar uma turma na base de dados. Recebe parâmetros referentes às informações da turma, incluindo nome do curso, id do professor, nível de proficiência, horário, idioma e número da sala.

•	EditClassroom: Método responsável por editar uma turma existente na base de dados. Recebe o id da turma a ser editada e parâmetros referentes às informações da turma, incluindo nome do curso, id do professor, nível de proficiência, horário, idioma e número da sala.

•	GetAllClassrooms: Método responsável por buscar todas as turmas existentes na base de dados. 

•	GetClassroomById: Método responsável por buscar uma turma existente na base de dados por meio de seu id. 

•	GetClassroomByRoomNumber: Método responsável por buscar uma turma existente na base de dados por meio de seu número de sala.

•	GetClassroomByLanguage: Método HTTP GET que retorna todas as turmas que possuem o idioma especificado como parâmetro.

•	DeleteClassroom: Método HTTP DELETE que exclui uma turma pelo seu ID. Se a turma for encontrada, ela é excluída do banco de dados. Se ocorrer um erro durante a exclusão, uma mensagem de erro é retornada. Além disso, um registro de log é criado para rastrear a exclusão da turma.

Mátriculas

EnrollmentController gerencia as operações relacionadas a matrículas de alunos em turmas de uma escola de idiomas, incluindo criação, edição, exclusão e busca de matrículas, além de validações necessárias para essas operações. As operações são registradas em um log de atividades.

•	CreateEnrollment: é responsável por criar uma matrícula. Ele valida se o aluno e a turma são válidos, verifica se a matrícula já existe e se a turma está cheia antes de criar uma matrícula.

•	EditEnrollment é responsável por atualizar uma matrícula existente. Ele verifica se o ID de matrícula fornecido é válido e, em seguida, atualiza as informações do aluno e da turma.

•	GetAllEnrollments é responsável por retornar todas as matrículas no banco de dados.

•	GetEnrollmentById é responsável por retornar uma matrícula específica com base no ID da matrícula fornecido.

•	DeleteEnrollment é responsável por excluir uma matrícula existente com base no ID da matrícula fornecido. Ele também registra a exclusão no log de atividades.

Logs de Entradas
LogEntryController lida com operações relacionadas às entradas de registro (logs) no sistema, incluindo criar uma entrada de log, obter todas as entradas de log, obter uma entrada de log por ID e obter uma lista de entradas de log por tipo.

•	CreateLogEntry: recebe uma descrição e um tipo, cria um novo registro de log com a data atual e salva no banco de dados. 

•	GetAllLogEntry: retorna todos os registros de log existentes no banco de dados. 

•	GetLogEntryById": recebe um ID como parâmetro e retorna o registro de log correspondente, se existir. 

•	GetLogEntryByType": recebe um tipo como parâmetro e retorna todos os registros de log que contenham aquele tipo. Se nenhum registro for encontrado, retorna uma mensagem de erro.



Tecnologias utilizadas
•	ASP.NET Core com SDK Microsoft.NET.Sdk.Web
•	Entity Framework Core 7.0.4
•	SQL Server Provider for Entity Framework Core 7.0.4
•	Microsoft.EntityFrameworkCore.Tools 6.0.15
•	Microsoft.VisualStudio.Web.CodeGeneration.Design 6.0.13
•	Swashbuckle.AspNetCore 6.5.0
•	NSwag.Annotations 13.18.2

Desenvolvedor:
Edevaldo Cruz Antonio
