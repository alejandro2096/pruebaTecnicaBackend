using Microsoft.EntityFrameworkCore;
using StoreApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura el DbContext (StoreContext) para usar SQLite o cualquier otra base de datos
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // Si usas SQL Server, usa UseSqlServer en lugar de UseSqlite

// Agregar los servicios necesarios para controladores
builder.Services.AddControllers();

// Agregar Swagger para la documentación de la API (Opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración de Swagger para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// // Configuración para manejo de errores HTTP y HTTPS redireccionamiento
// app.UseHttpsRedirection();

// Habilita el enrutamiento de la API
app.UseRouting();

// Autoriza las solicitudes (si hay autenticación en tu API)
app.UseAuthorization();

// Mapea los controladores para que manejen las rutas de la API
app.MapControllers();

// Ejecuta la aplicación
app.Run();
