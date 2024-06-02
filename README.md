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
