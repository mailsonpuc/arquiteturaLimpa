# CleanArchMvc — Exemplo de Arquitetura Limpa (Clean Architecture)

Uma implementação de referência de uma aplicação ASP.NET Core MVC demonstrando princípios de Arquitetura Limpa, CQRS com MediatR, AutoMapper, e Identity.

> Este projeto foi criado para aprender e aplicar conceitos de Arquitetura Limpa, separando a aplicação em camadas Domain / Application / Infra / WebUI. Aqui eu aprendi a estruturar responsabilidades, usar injeção de dependências, e integrar autenticação e autorização.

## Estrutura do repositório

- `CleanArchMvc.Domain/` — Entidades do domínio, interfaces e contratos (sem dependências de infraestrutura).
- `CleanArchMvc.Application/` — Casos de uso, DTOs, comandos/queries e handlers (CQRS), serviços de aplicação e mapeamentos (AutoMapper).
- `CleanArchMvc.Infra.Data/` — Implementações de persistência (EF Core), Identity e repositórios.
- `CleanArchMvc.Infra.IoC/` — Registro de serviços e configuração de DI (método `AddInfrastructure`).
- `CleanArchMvc.WebUI/` — Projeto ASP.NET Core MVC: controllers, views, assets e configuração do `Program.cs`.

## Principais tecnologias

- .NET 10 (ASP.NET Core MVC)
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity (Authentication/Authorization)
- JWT Bearer authentication (configurado como opção para APIs)
- MediatR (CQRS)
- AutoMapper
- Razor Views (MVC)

## Recursos implementados

- CRUD de `Categories` e `Products` com views MVC.
- Upload de imagens para `wwwroot/images` (Create / Edit de produtos).
- Exibição de preço formatado em `pt-BR` nas views.
- Separação por camadas seguindo Clean Architecture.
- CQRS: comandos/queries e handlers em `CleanArchMvc.Application`.
- Identity com seed de roles/usuários (Admin e User).
- Suporte a autenticação por cookie (UI) e configuração de JWT para APIs.

## Como executar

Pré-requisitos:

- .NET 10 SDK
- SQL Server (ou ajuste a string de conexão em `CleanArchMvc.WebUI/appsettings.json`)

Passos rápidos:

```bash
cd cleanArq
dotnet build cleanArq.sln
dotnet run --project CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj
```

Abra no navegador: `https://localhost:5001` (ou a URL mostrada no terminal).

### Executar com Docker (opção)

O repositório inclui arquivos Docker para executar a aplicação e um SQL Server via Docker Compose.

Passos:

```bash
# constrói e sobe containers (primeira vez pode demorar)
docker compose up --build

# acessa a aplicação via HTTP
http://localhost:5000
```

Observações:

- O `docker-compose.yml` expõe a API na porta `5000` e o SQL Server na porta `1433`.
- A variável `ConnectionStrings__DefaultConnection` é injetada no container `web` para apontar para o container `db`.
- O `Dockerfile` usa imagens `.NET 10` SDK/ASP.NET para corresponder ao target `net10.0`. Ajuste se necessário.

## Configuração de banco e Identity

A string de conexão está em `CleanArchMvc.WebUI/appsettings.json` na chave `ConnectionStrings:DefaultConnection`.

O projeto contém um serviço de seed para criar papéis (`Admin`, `User`) e dois usuários de exemplo (`admin@localhost`, `user@localhost`). A implementação está em `CleanArchMvc.Infra.Data/Identity/SeedUserRoleInitial.cs`.

Para executar o seed automaticamente durante o startup (recomendado para ambiente de desenvolvimento), adicione uma chamada a `ISeedUserRoleInitial` no `Program.cs` do WebUI. Exemplo:

```csharp
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();
    seeder.SeedRoles();
    seeder.SeedUsers();
}
```

Coloque esse bloco antes de `app.Run();` para garantir que as roles/usuários existam.

Senha padrão usada no seed (apenas em dev):

- Admin: `Admin#2025`
- User: `User#2025`

> Importante: altere essas senhas e gerencie segredos corretamente em produção (usar User Secrets ou variáveis de ambiente).

## Autenticação — Cookie (UI) e JWT (API)

O projeto mantém a autenticação por cookie (SignInManager / UserManager) para a interface de usuário. O `AccountController` usa `IAuthenticate` (`AuthenticationService`) para registrar, autenticar e deslogar usuários.

Além disso, foi adicionada configuração básica para JWT Bearer em `CleanArchMvc.Infra.IoC/DependencyInjection.cs` usando as configurações em `appsettings.json` (`JwtSettings`). Para emitir tokens JWT (úteis para APIs/clients), você pode:

1. Implementar geração de token em `AuthenticationService` — criar um método que, ao autenticar com sucesso, gere um JWT com `JwtSecurityTokenHandler` usando a chave em `JwtSettings:Key`.
2. Expor um endpoint API que retorna o token para o client.

Exemplo de configuração `JwtSettings` em `appsettings.json` (já incluída no projeto):

```json
"JwtSettings": {
  "Key": "YOUR_LONG_SECRET_KEY_CHANGE_ME",
  "Issuer": "CleanArchMvc",
  "Audience": "CleanArchMvc",
  "DurationInMinutes": 60
}
```

E lembre-se de substituir `Key` por um segredo forte em produção.

## Protegendo rotas

Para exigir autenticação via JWT em controladores/ações API, aplique o atributo:

```csharp
[Authorize(Policy = "Bearer")]
public class MyApiController : ControllerBase { }
```

Para rotas de UI que usam cookie auth, use `[Authorize]` normalmente.

## Observações de desenvolvimento

- O layout (`Views/Shared/_Layout.cshtml`) já inclui links para `Login`, `Register` e `Logout`. Depois de fazer login (cookie auth), o `HomeController` mostra o nome do usuário usando `User.Identity.Name`.
- As views antigas que eram Razor Pages foram convertidas para MVC quando necessário (removendo `@page`), evitando conflitos de roteamento.

## Testes

O projeto contém testes unitários básicos em `CleanArchMvc.Domain.Tests`. Rode os testes com:

```bash
dotnet test
```

## Contribuição

- Sinta-se à vontade para abrir issues ou pull requests.
- Se quiser melhorar: adicionar validação de tipos/tamanhos para upload de imagens, apagar imagens antigas ao atualizar, emitir tokens JWT no login, adicionar testes de integração.

## Licença

Este repositório é um projeto de aprendizado — adapte-o conforme sua necessidade. Não inclui uma licença explícita.

---

Eu aprendi e apliquei conceitos de Arquitetura Limpa, separando responsabilidades por camadas e usando práticas modernas do ecossistema .NET. 