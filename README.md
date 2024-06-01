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
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```
  - Esse comando irá instalar a versão mais recente da biblioteca informada
  - Para especificar uma versão basta incluir a opção `--version` e o número da versão ao final da linha de comando. Por exemplo:
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.6
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
