using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicoss.Components;
using RegistroTecnicoss.DAL;
using RegistroTecnicoss.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredToast();

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<ClienteService>();   


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Contexto>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts ();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
