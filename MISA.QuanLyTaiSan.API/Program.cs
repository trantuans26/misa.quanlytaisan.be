using MISA.QuanLyTaiSan.BL;
using MISA.QuanLyTaiSan.BL.BaseBL;
using MISA.QuanLyTaiSan.DL;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Dependency injection
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IFixedAssetBL, FixedAssetBL>();
builder.Services.AddScoped<IFixedAssetDL, FixedAssetDL>();

// Lấy dữ liệu connection string từ file appsettings.Development.json
DataContext.ConnectionString = builder.Configuration.GetConnectionString("MySQL");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    //build.WithOrigins("http://localhost:8080", "http://localhost:28533");
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
