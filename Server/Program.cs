using Microsoft.EntityFrameworkCore;
using Server.Data;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Server.Services;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ProposalService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<BonusService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
// Configure PostgreSQL database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services in the container.
builder.Services.AddRazorPages(); // For Razor Pages support
builder.Services.AddControllersWithViews(); // For MVC support (Controllers and Views)
builder.Services.AddCors(options => 
{
	options.AddPolicy("Dev", builder =>
	builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

// Register other services like repositories, services, etc. if needed
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();
app.UseCors("Dev"); // Apply CORS policy globally
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages(); // If you have Razor Pages

app.Run();
