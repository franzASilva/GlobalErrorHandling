using GlobalErrorHandling.API.Endpoints;
using GlobalErrorHandling.API.Exceptions;
using GlobalErrorHandling.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1); // removes schemas
    });
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.RegisterEndpoints();
app.Run();
