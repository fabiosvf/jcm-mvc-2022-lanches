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
