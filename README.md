#### Curso de ASP.NET Core MVC (.NET 6) - José Carlos Macoratti

# Projeto - Site de Venda de Lanches Online 🍔🍟

## Ferramentas utilizadas
- Microsoft Visual Studio Community 2022
- Microsoft SQL Server 2022 Express
- Microsoft SQL Server Management Studio v20.1

## Instalação do Microsoft Visual Studio Community 2022
- Instalar as seguintes cargas de trabalho:
  - ASP .NET e desenvolvimento Web
  - Desenvolvimento de .NET Multi-Platform App UI
  - Processamento e armazenamento de dados

## Criação do Projeto
- Crie um projeto do tipo:
  - ASP.NET Core Web App (Model-View-Controller) C#

## Instalando o Entity Framework Core
- Acesse o menu `Tools`
- Em seguida `NuGet Package Manager`
- Para instalar via linha de comando siga o procedimento abaixo:
  - Clique na opção `Package Manager Console`
  - E digite a seguinte linha de comando:
```
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
```
  - Esse comando irá instalar a versão mais recente da biblioteca informada
  - Para especificar uma versão basta incluir a opção `--version` e o número da versão ao final da linha de comando. Por exemplo:
```
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.6
```
- Para instalar via interface visual siga o procedimento abaixo:
  - Clique na opção `Manage NuGet Packages for Solution...`
  - Em seguida clique na aba `Brownse`
  - E digite no campo de pesquisa o nome da biblioteca, que no nosso caso é `Microsoft.EntityFrameworkCore.SqlServer`
  - Do lado direito da tela marque o projeto onde deseja instalar a biblioteca, que no nosso caso é o `LanchesMac`
  - Por fim, selecione a versão, clique em `Install` e confirme as demais telas para finalizar a instalação
- Siga os mesmos passos para as demais bibliotecas:
  - Microsoft.EntityFrameworkCore.Tools
  - Microsoft.EntityFrameworkCore.Design

## Migrations
- As `Migrations` são um recurso do `Entity Framework Core` que permite a partir de um modelo de classes com propriedades, mapear o banco de dados e gerar scripts dinâmicos para criação de tabelas, campos, carga de dados e qualquer outro tipo de alterações no banco de dados de forma automatizada.

### Passos necessários
- Para que as `Migrations` funcionem, é necessário se preocupar com os seguintes passos:
  - Instalação das bibliotecas `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools` e `Microsoft.EntityFrameworkCore.Design`
  - Criação de um modelo de entidades que nada mais são do que classes (que representam as tabelas do banco de dados) com as propriedades (que representam os campos das tabelas). No caso deste projeto, esse modelo é representado pelos arquivos `Categoria.cs` e `Lanche.cs` disponíveis na pasta `Models` na raiz do projeto
  - Criação de uma classe de contexto que herda de `DbContext` e `DbSets` para mapear as entidades. Isso foi feito no arquivo `AppDbContext.cs` disponível na pasta `Context` na raiz do projeto
  - Definição da **String de Conexão** no arquivo `appsettings.json` disponível na raiz do projeto
  - Registro do contexto como um serviço usando `AddDbContext` disponível no arquivo `Program.cs` na raiz do projeto aplicando a **String de Conexão** definida

### Como funciona
- Após criar os modelos de entidade, seguindo todos os passos necessários citados no item anterior, é necessário **criar o arquivo de migração** e em seguida **aplicar o arquivo de migração** no banco de dados.
- Para **criar o arquivo de migração**, abra o terminal no menu `Tools > Nuget Package Manager > Package Manager Console`
- Em seguida digite o seguinte comando:
  - Modelo `Add-Migration NomeDaMigracao [options]`
```
PM> Add-Migration MigracaoInicial
```
- Para rodar o arquivo de migração, digite o seguinte comando no terminal:
  - Modelo `Update-Database [options]`
```
PM> Update-Database
```
- Toda vez que forem feitas alterações nos modelos de entidades de acordo com os passos citados no item anterior, é necessário criar o arquivo de migração e depois aplica-lo pra refletir o ajuste da aplicação no banco de dados.
- Para remover uma migração que foi aplicada incorretamente, digite o seguinte comando no terminal:
```
PM> Remove-Migration
```

## Padrão `Repository`
- O padrão `Repository` é um padrão de projeto utilizado para acessar os dados à partir do banco de dados, eliminando o acoplamento, centralizando a logica de acesso a dados e tornando o código mais fácil de dar manutenção.

### Vantagens
- Essa abordagem possui inúmeras vantagens, dentre elas:
  - Desacoplar a sua aplicação da lógica de acesso a dados
  - Centralizar a lógica de acesso a dados
  - Facilitar a realização de testes
  - Facilitar a manutenção do código
  - Minimizar a duplicação de código nas consultas e comandos

### Implementação
- E pra implementar esse padrão no projeto é muito simples. É necessário:
  - Criar uma interface com o contrato da lógica de acesso aos dados
  - Criar uma classe concreta que implementa o contrato da interface
  - Registrar o `Repository` na sessão `Services` utilizando `Injeção de Dependência`
- Utilizando o nosso exemplo de `Categorias` e `Lanches`, criaremos a interface `ICategoriaRepository` e implementaremos a lógica na classe `CategoriaRepository`. Da mesma forma, criaremos a interface `ILancheRepository` e implementaremos a lógica na classe `LancheRepository`.

#### Injeção de Dependência
- `Injeção de Dependência` é um tipo de `Inversão de Controle` e significa que uma classe não mais é responsável por criar ou buscar os objetos dos quais depende. Você coloca a responsabilidade das classes externas na classe que está chamando e não na classe chamada.
- A `Injeção de Dependência` apenas injeta a dependência de uma classe para outra classe. A `Inversão de Controle` deixa de ter a dependência internamente da classe e passa para uma classe externa.
- Nós já implementamos a injeção de dependência para a nossa classe `AppDbContext` (que faz o acesso ao banco de dados) através do método `AddDbContext` do `builder.Services` a partir do arquivo `Program.cs`
- Isso significa que toda vez que alguma classe espera receber uma instância da classe `AppDbContext` no construtor, essa implementação se encarrega de fornecer uma instância da classe.
- Seguindo essa mesma lógica, será necessário implementar esse registro também para as interfaces e classes `Repository`, e isso será feito através dos métodos `AddTransient<Interface, Class>`, `AddScoped<Interface, Class>` e/ou `AddSingleton<Interface, Class>` também do `builder.Services` a partir do arquivo `Program.cs`
- Cada um destes métodos possui um escopo de serviço diferente, ou seja, o tempo de vida útil. Esses escopos afetam como o serviço é resolvido e descartado pelo provedor de serviços.
  - Transient: `builder.Services.AddTransient<Interface, Class>();`
    - Uma nova instância do serviço é criada cada vez que um serviço é solicitado do provedor de serviços.
    - Se o serviço for descatável, o escopo do serviço monitorará todas as instâncias do serviço e destruirá todas as instâncias do serviço criadas nesse escopo quando o escopo do serviço for descartado.
  - Scoped: `builder.Services.AddScoped<Interface, Class>();`
    - Uma nova instância do serviço é criada em cada `request`.
    - A cada requisição temos uma nova instância do serviço.
    - Se o serviço for descartável, ele será descartado quando o escopo do serviço for descartado.
  - Singleton: `builder.Services.AddSingleton<Interface, Class>();`
    - Apenas uma instância do serviço é criada se ainda não estiver registrada como uma instância.
    - Um objeto do serviço é criado e fornecido para todas as requisições.
    - Todas as requisições obtém o mesmo objeto.
- No caso deste projeto iremos utilizar a seguinte implementação:
```cs
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
```

## Trabalhando com `Session`
- O protocolo `HTTP` é um protocolo sem estado, dessa forma um servidor web trata cada solicitação HTTP como uma solicitação independente e não retém valores do usuário de requisições anteriores
- Na ASP .NET Core o estado da sessão é um recurso que podemos utilizar para salvar e armazenar dados do usuário enquanto navega na aplicação. Esse recurso está presente no pacote `Microsoft.AspNetCore.Session`
- Com base em um dicionário ou tabela hash no servidor, o estado da sessão persiste os dados através das requisições de um navegador.
- O ASP .NET Core mantém o estada da sessão, dando ao cliente um cookie que contém o ID da sessão, que é enviado ao servidor com cada solicitação.
- O servidor mantém uma sessão por tempo limitado após a última requisição.
- Se um valor não for definido, o valor padrão de 20 minutos é definido automaticamente.
- O estado da sessão é ideal para armazenar dados do usuário específicos de uma determinada sessão.
- Esses dados ficam armazenados no cache e serão excluídos em duas ocasiões:
  - Quando a sessão expirar atingindo o tempo limite da sessão
  - Ou quando o comando `Session.Clear()` for acionado
 
### Configurando a Sessão
- O recurso `Session` está disponível no pacote `Microsoft.AspNetCore.Session`
- Esse pacote fornece o `Middleware` para gerenciar o estado da sessão.
- Para habilitar o `Middleware` da sessão precisamos definir a seguinte configuração no arquivo `Program.cs`:
  - Registrar a interface `IHttpContextAccessor` para a Injeção de Dependência através de `builder.Services` para que possamos armazenar ou acessar os dados armazenados na sessão.
```cs
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
```
  - Configurar o serviço de qualquer um dos caches de memória `IMemoryCache` chamando o método `AddMemoryCache` através de `builder.Services`.
```cs
builder.Services.AddMemoryCache();
```
  - Configurar o serviço de implementação da `Session` chamando o método `AddSession` através de `builder.Services`.
```cs
builder.Services.AddSession();
```
  - Ativar o `Middleware` chamando o método `UseSession` através de `app`.
```cs
app.UseSession();
```

### Atribuindo e Obtendo dados da Sessão
- Para atribuir e/ou obter dados da sessão nós utilizamos a sessão do contexto através de `HttpContext`:
  - Os dados podem ser atribuído à sessão da seguinte forma:
```
HttpContext.Session.SetString("_Nome", "Macoratti");
HttpContext.Session.SetInt32("_Idade", 23);
```
  - Os dados podem ser obtidos da sessão da seguinte forma:
```
var nome = HttpContext.Session.GetString("_Nome");
var idade = HttpContext.Session.GetString("_Idade");
```

## Trabalhando com `ViewComponents`
- As `ViewComponents` são componentes reutilizáveis no `ASP.NET Core MVC` que encapsulam a lógica de apresentação e podem renderizar uma parte da interface do usuário. Eles são semelhantes a mini-controllers e podem executar lógica complexa antes de renderizar uma view.

### Diferenças entre `ViewComponents` e `PartialViews`

#### 1. Encapsulamento de Lógica
- **ViewComponents:** Podem incluir lógica C# complexa, semelhante a um controller. Eles têm seu próprio método `Invoke` ou `InvokeAsync`.
- **PartialViews:** São fragmentos de view que não contém lógica de execução própria e são renderizadas pelo controller ou pela view principal.

#### 2. Independência
- **ViewComponents:** São independentes e podem ser invocados diretamente sem depender de um controller.
- **PartialViews:** Dependem de serem invocadas por uma view principal ou controller.

#### 3. Teste e Reutilização
- **ViewComponents:** Mais fáceis de testar e reutilizar devido à sua independência e encapsulamento de lógica.
- **PartialViews:** Menos independentes e podem ser mais difíceis de testar isoladamente.

### Capacidades Adicionais de `ViewComponents`
- **Execução de Lógica Complexa:** Podem executar consultas a banco de dados, chamadas a serviços, e outras operações antes de renderizar a view.
- **Independência Total:** Podem ser invocados diretamente de uma view, sem passar por um controller.

### Implementação de um `ViewComponent`

#### 1. Criar a Classe do `ViewComponent`
- Crie uma nova classe que herda de `ViewComponent`.
- Implemente o método `Invoke` ou `InvokeAsync`.
```csharp
public class MyViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var model = // Obtenha ou prepare o modelo necessário
        return View(model); // Retorna a view com o modelo
    }
}
```

#### 2. Criar a `View` do `ViewComponent`
- Crie uma view na pasta `Views/Shared/Components/MyViewComponent/Default.cshtml`
```csharp
@model YourModelType
<div>
    <!-- Renderize seu conteúdo aqui -->
</div>
```

#### 3. Invocar o `ViewComponent` na View Principal
- Use o método `Component.InvokeAsync` na view principal para renderizar o `ViewComponent`
```csharp
@await Component.InvokeAsync("MyViewComponent")
```

## Trabalhando com `Identity` para Autenticação e Autorização

### Autenticação
- A Autenticação é a verificação de uma identidade feita pela comparação das credenciais apresentadas com outras pré-definidas. (Quem é você?)

### Autorização
- A Autorização ocorre após a autenticação e permite atribuir e definir privilégios ao sujeito autenticado. (O que você pode fazer?)

### Tipos de Autenticação
- Existem vários tipos de autenticação. São elas:
  - None (No Authentication)
  - Individual (Individual Authentication)
  - IndividualB2C (Individual Authentication with Azuer AD B2C)
  - SingleOrg (Organizational Authentication for a Single Tenant)
  - MultiOrg (Organization Authentication for Multiple Tenants)
  - Windows (Windows Authentication)
- Para esse projeto iremos utilizar o `Individual`, também conhecido como `Individual Accounts`

### Trabalhando com a API `ASP .NET Core Identity`
- O `ASP .NET Core Identity` é uma `API` que suporta a funcionalidade de logon da interface do usuário (UI).
- Gerencia usuários, senhas, dados de perfil, funções, declarações, tokens, confirmação por email e muito mais.
- Para criar um projeto do zero em Visual Studio 2022, já utilizando um dos tipos de autenticação, via linha de comando, digite:
```
PM> dotnet new mvc --auth Individual -o mvc1
```
- Ou, para criar o mesmo projeto utilizando o assistente do Visual Studio 2022, siga os passos:
  - Abra o `Visual Studio 2022`
  - Clique na opção `Create a new project`
  - Selecione ou filtre pelo projeto `ASP.NET Core Web App (Model-View-Controller)` e clique em `Next`
  - Na tela `Configure your new project`, preencha os campos:
    - Nome do projeto em `Project name`
    - O caminho do disco para salvar o projeto em `Location`
    - Nome da solution em `Solution name`
    - Em seguida clique em `Next`
  - Na tela `Additional information`, preencha os campos:
    - Versão do Framework em `Framework`, no caso desse projeto estou utilizando a versão `.NET 8.0 (Long-term support)`
    - E o tipo de autenticação em `Authentication type`, no caso desse projeto estou utilizando a opção `Individual Accounts`
    - Mantenha o campo `Configure for HTTPS` marcado
    - Mantenha o campo `Enable Docker` desmarcado
    - Em seguida clique em `Create`
- O projeto criado, já inclui duas bibliotecas padrão, necessárias para o funcionamento da estrutura de autenticação e autorização. São elas:
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
  - `Microsoft.AspNetCore.Identity.UI`
- A lib `Microsoft.AspNetCore.Identity.UI` possui um conjunto de Views que podem ser personalizadas. Para incluir uma ou mais páginas dessa biblioteca, siga os passos:
  - Clique com o botão direito do mouse sobre o Nome do Projeto
  - Selecione a opção `Add` e depois `New Scaffolded Item...`
  - Na tela `Add New Scaffolded Item` selecione a categoria `Identity` na lateral esquerda, selecione a oção `Identity` na área principal e clique em `Add`
  - Na próxima tela aberta chamada `Add Identity` é listada todas as `Views` da estrutura de autenticação da biblioteca.
    - Esse recurso permite selecionar qualquer página, ou todas se achar necessário, e personaliza-las de acordo com as preferências do cliente
  - No campo `Data context class` selecione o `Context` para acesso ao banco de dados via `EntityFramework`
  - E por fim clique em `Add` para adicionar os arquivos selecionados dentro projeto
  - Esses arquivos serão criados dentro da estrutura de pastas `Areas\Identity\Pages\Account` utilizando um recurso chamado `Razor Pages`

## Adaptando um projeto do zero para trabalhar com a API `ASP .NET Core Identity`

### Instalando as bibliotecas
- Em um primeiro momento será necessário instalar o pacote:
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- Esse pacote pode ser instalado através da tela `Tools > NuGet Package Manager > Package Manager Console` digitando a linha de comando:
```
PM> Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```
- Ou utilizando a interface visual acessada a partir de `Tools > NuGet Package Manager > Manage NuGet Packages for Solution...` e filtrando pela biblioteca na aba `Browse`
- Após pesquisar, selecione a biblioteca e na lateral direita, selecione o projeto ao qual deseja aplicar a biblioteca e também a versão desejada e em seguida clique em `Install`

### Configurando o `Identity` no projeto
- Será necessário alterar o arquivo `Context\AppDbContext` para herdar de `IdentityDbContext<IdentityUser>` ao invés de diretamente do `DbContext`
  - Esse ajuste é necessário porque iremos utilizar a estrutura de tabelas de usuário e regras de acesso do próprio `Identity` e precisamos que essas tabelas sejam criadas dentro do nosso banco de dados.
  - Será necessário referenciar os seguintes `namespaces`:
    - `Microsoft.AspNetCore.Identity`
    - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- O próximo arquivo a ser alterado é o `Program.cs` na raiz do projeto:
  - Na parte onde é registrado o contexto, será necessário incluir o seguinte código para registrar o `Identity`:
```
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
```
- E pra finalizar, logo abaixo da parte onde é registrado o `app.UseSession` garanta que as seguintes linhas de código sejam implementadas obedecendo essas mesma ordem:
```
app.UseAuthentication();
app.UseAuthorization();
```
- Agora, pra criar uma nova Migration para a criação da estrutura de tabelas para autenticação dentro do nosso banco de dados, digite no terminal a linha de comando:
```
PM> add-migration AdicionarIdentity
```
- Se tiver atualizações para fazer, esse comando vai sugerir a necessidade de atualizar as bibliotecas antes de prosseguir.
- E para aplicar as `Migrations` no banco de dados, digite:
```
PM> update-datebase
```

### Implementando a Tela de Login
- Crie a ViewModel `ViewModel\LoginViewModel` qwe será responsável por representar os campos na tela de Login
- Crie a Controller `Controllers\AccountController` responsável pelo Login
  - Aqui será necessário criar duas variáveis do tipo `private readonly` que são `UserManager<IdentityUser>` e `SignInManager<IdentityUser>`
  - Em seguida, também será necessário configurar a inicialização das variáveis através do recurso de Injeção de Dependência no construtor da classe.
  - Além disso, crie o método `Login` tanto como `HttpGet` quanto como `HttpPost`
- Crie a View `Views\Account\Login`
- Para mais detalhes consulte o código fonte

### Implementando a Tela de Registro de Usuário
- No arquivo `Controllers\AccountController` crie o método `Register` tanto como `HttpGet` quanto como `HttpPost`
- No método `Register` do tipo `HttpPost` implemente o `DataAnnotation` chamado `ValidateAntiForgeryToken`
- Crie a View `View\Account\Register`
- Para mais detalhes consulte o código fonte

### Entendendo o recurso `AntiForgeryToken`
- Este recurso é importante para evitar ataques do tipo `CSRF - Cross Site Request Forgery`, que quer dizer `Falsificação de requisições entre site`
- A `falsificação de requisição entre sites` é uma técnica que um hacker usa para obter a identidade e os privilégios de usuários que estão autenticados de forma legítima no site, e a seguir ele vai executar qualquer ação que aquele usuário autenticado possa ter direito.
- Sendo assim, para evitar esse tipo de ataque, foi criado o recurso `AntiForgeryToken` que funciona da seguinte forma:
  - O cliente solicita uma página HTML que contém um formulário
  - A ASP.NET Core inclui dois tokens no response:
    - Um token é enviado como um cookie HTTP cifrado
    - O outro é colocado em um campo oculto do formulário (hidden)
  - Os tokens são gerados aleatoriamente para que um hacker não consiga adivinhar os valores
  - Quando o cliente envia o formulário, ele deve enviar os dois tokens de volta ao servidor
  - O cliente envia o token do cookie e o token do formulário dentro dos dados do formulário.
  - Se uma solicitação não incluir os dois tokens que devem ser iguais, o servidor não permitirá a solicitação.
  - O atributo `[ValidateAntiForgeryToken]` é usado para validar o token gerado na view e assim evitar esses ataques.
