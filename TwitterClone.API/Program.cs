using Microsoft.EntityFrameworkCore;
using TwitterClone.Application.Services;
using TwitterClone.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);
// Configura CORS para permitir todos los orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()   // Permite cualquier origen
            .AllowAnyHeader()   // Permite cualquier encabezado
            .AllowAnyMethod();  // Permite cualquier método (GET, POST, PUT, etc.)
    });
});

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TweetService>();
builder.Services.AddScoped<FollowersService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 32)), optionsBuilder =>
        {
            optionsBuilder.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        } ));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Development",
        policy  =>
        {
            policy.WithOrigins("http://localhost:4200/");
        });
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Aplicar migraciones automáticamente
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate(); 
    }
    
    Console.WriteLine($"Swagger enabled");

    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Development");

app.Run();