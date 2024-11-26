using AgendaContatoApi.Data;
using AgendaContatoApi.Ferramentas;
using AgendaContatoApi.Interface.Repositories;
using AgendaContatoApi.Interface.Services;
using AgendaContatoApi.Repository.Agenda;
using AgendaContatoApi.Services.Agenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

SQLitePCL.Batteries.Init(); // Inicializa o provedor SQLite

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlite("Data Source=agenda.db")); // Configuração do SQLite

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Agenda de Contatos", Version = "v1" });
    c.SchemaFilter<PadraoSwagger>();
});

builder.Services.AddScoped<IAgendaRepository, AgendaContatoRepository>();
builder.Services.AddScoped<IAgendaService, AgendaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AgendaContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();