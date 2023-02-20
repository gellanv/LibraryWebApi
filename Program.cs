using Library.Data;
using Library.Mappings;
using Library.Middleware;
using LibraryWebApi.Extentions;
using LibraryWebApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApiDBContext>(o => o.UseInMemoryDatabase("MyDatabase"));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCustomServices();

var app = builder.Build();
MockData.AddCustomerData(app);

if (app.Environment.IsDevelopment())
{ }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();
app.Run();
