using Microsoft.EntityFrameworkCore;
using PRIMERA_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<PARCIAL1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PARCIAL1Context") ?? throw new InvalidOperationException("Connection string 'PARCIAL1Context' not found.")));
builder.Services.AddDbContext<PARCIAL1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PARCIAL1Context")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
