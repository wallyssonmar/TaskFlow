using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskFlowAPI.Authentication;
using TaskFlowAPI.Data;
using TaskFlowAPI.Repositories;
using TaskFlowAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.TokenValidationParameters = TokenHelpers.GetTokenValidationParameters(builder.Configuration);
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine(context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthentication();

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
builder.Services.AddScoped<AuthService>();

//Adição dos repositórios para Injeção de dependencia
builder.Services.AddScoped<ProjetoRepository>();
builder.Services.AddScoped<TarefaRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<RefreshTokenRepository>();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
                )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
