using Microsoft.EntityFrameworkCore;
using StockGuru.Data;
using StockGuru.Interfaces;
using StockGuru.Repo;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    {
        opts.UseSqlServer(Environment.GetEnvironmentVariable("DB_URI"));
    });
    builder.Services.AddScoped<IStockRepo, StockRepo>();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseHttpsRedirection();

    app.MapControllers();
    
    app.Run();
}