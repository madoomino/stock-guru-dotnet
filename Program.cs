using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockGuru.Data;
using StockGuru.Interfaces;
using StockGuru.Repo;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddControllers().AddNewtonsoftJson(opts =>
    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
  );
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  builder.Services.AddDbContext<ApplicationDbContext>(opts =>
  {
    opts.UseSqlite($"Data Source={Environment.GetEnvironmentVariable("DB_URI")}");
  });
  builder.Services.AddScoped<IStockRepo, StockRepo>();
  builder.Services.AddScoped<ICommentRepo, CommentRepo>();
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
