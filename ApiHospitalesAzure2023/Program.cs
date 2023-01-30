using ApiHospitalesAzure2023.Data;
using ApiHospitalesAzure2023.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("sqlhospital");
builder.Services.AddTransient<RepositoryHospital>();
builder.Services.AddDbContext<HospitalContext>
    (options => options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Api Hospitales Lunes 2023",
         Description = "Estamos a lunes trabajando con Api en Azure",
         Version = "v1",
         Contact = new OpenApiContact()
         {
              Name = "Paco Garcia Serrano",
              Email = "pacoserranox@gmail.com"
         }
    });
});

var app = builder.Build();

app.UseSwagger();
//VAMOS A INDICAR QUE LA PAGINA INICIAL DE NUESTRO SERVICIO EN 
//AZURE SERA LA DOCUMENTACION
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
