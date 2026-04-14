using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Repositories;
using TaskFlowAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string? stringDeConexao = builder.Configuration.GetConnectionString("StringConexaoBanco");
if(stringDeConexao is null)
{
    throw new Exception("A string de conexão não foi definida no appsettings");
}
builder.Services.AddDbContext<TaskFlowApiContext>(opt => opt.UseNpgsql(stringDeConexao));


//Adição dos Service para Injeção de dependencia
builder.Services.AddScoped<ProjetoService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<UserService>();

//Adição dos repositórios para Injeção de dependencia
builder.Services.AddScoped<ProjetoRepository>();
builder.Services.AddScoped<TarefaRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
