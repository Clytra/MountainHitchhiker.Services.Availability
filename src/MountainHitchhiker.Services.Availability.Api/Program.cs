using Convey;
using Convey.WebApi;
using MountainHitchhiker.Services.Availability.Api.Endpoints;
using MountainHitchhiker.Services.Availability.Application;
using MountainHitchhiker.Services.Availability.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddConvey()
    .AddWebApi()
    .AddApplication()
    .AddInfrastructure();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseInfrastructure();

app.MapResourceEndpoints();

app.Run();