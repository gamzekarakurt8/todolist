using Microsoft.EntityFrameworkCore;
using todolist.data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // View destekli MVC
builder.Services.AddControllers(); // API deste�i i�in gerekli

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<todocontext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Varsay�lan rotay� ayarla
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=index}/{action=Giris}/{id?}" // MVC i�in varsay�lan rota
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
