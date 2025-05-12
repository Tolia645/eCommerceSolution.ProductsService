using System.Text.Json.Serialization;
using eCommerce.DAL;
using eCommerce.BLL;
using ecommerce.API_Layer.APIEndpoints;
using ecommerce.API_Layer.Middleware;
using eCommerce.BLL.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccesLayer(builder.Configuration);
builder.Services.AddBusinnesLayerServices();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(ProductAddRequestValidator).Assembly);

builder.Services.ConfigureHttpJsonOptions(options => { 
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(
                "http://localhost:7196", 
                "http://localhost:4200",
                "http://localhost:7122")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExeptionHandlingMiddleware();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductEndpoints();

Console.WriteLine("Hello");

app.Run();