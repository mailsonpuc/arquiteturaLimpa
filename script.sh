# add migration
dotnet ef migrations add Inicial --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI

# atualizar database
dotnet ef database update --project CleanArchMvc.Infra.Data --startup-project CleanArchMvc.WebUI
