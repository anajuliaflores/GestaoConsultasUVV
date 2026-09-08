# Sistema de Gestão de Consultas UVV
Aplicação web desenvolvida em C# com ASP.NET Core (MVC) para gerenciamento de usuários e registro de consultas, utilizando Entity Framework Core e SQL Server.
Trabalho da disciplina de Desenvolvimento Web Back-end.
**Autora:** Ana Julia Flores
## Tecnologias utilizadas
- C# / ASP.NET Core MVC (.NET 10)
- Entity Framework Core (abordagem Code First)
- SQL Server (LocalDB)
- Autenticação por Cookies
## Funcionalidades
- Cadastro de usuário
- Login e logout (autenticação)
- Registro de consultas (criar, listar, editar e excluir)
- Rotas de consulta protegidas: apenas usuários autenticados têm acesso
- Cada usuário visualiza somente as suas próprias consultas
## Como configurar e executar o projeto
1. Clone o repositório:https://github.com/anajuliaflores/GestaoConsultasUVV.git
2. Abra o projeto no Visual Studio 2022 (ou superior).
3. Confira a string de conexão no arquivo appsettings.json. Por padrão, o projeto utiliza o SQL Server LocalDB: "Server=(localdb)\MSSQLLocalDB;Database=GestaoConsultasUVV;Trusted_Connection=True;MultipleActiveResultSets=true"
4. No Console do Gerenciador de Pacotes (Ferramentas > Gerenciador de Pacotes do NuGet > Console), execute o comando para criar o banco de dados: Update-Database
5. Execute o projeto (F5). O sistema abrirá no navegador.
## Estrutura (padrão MVC)
- **Models**: Usuario e Consulta (com validações via Data Annotations)
- **Views**: telas de cadastro, login e o CRUD de consultas
- **Controllers**: UsuariosController (cadastro/login) e ConsultasController (CRUD protegido)
- **Data**: AppDbContext (contexto do Entity Framework), registrado por injeção de dependência no Program.cs
## Vídeo demonstrativo
🎥 [Link do vídeo demonstrativo](https://youtu.be/DyPM3K2cp8k)    
