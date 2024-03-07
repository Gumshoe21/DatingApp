using API.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// we need to add our DataContext as a service, 
// we want to be able to inject it into other parts of our application 
// so when we want to get something from our database, then we're going to need access to that DB context class.
// So we're going to add it as a service inside our program class.
// AddDbContext is an "Extension Method"
// <> will contain the type of thing we want our DbContext to be
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); // takes a configuration connection string
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
var app = builder.Build();
// strategic placement of UseCors right after we build our app.
// UseCors... specify to builder that we allow any header and any method with the origins specified
// Adds the "Access-Control-Allow-Origin" header of "http://localhost:4200" to our response headers, so if you go to localhost:4200, open up Network tab in devtools, under "users" on the left it will show you Headers on the right

// regarding Google Chrome not trusting my ssl cert... someone said:
// "I did find workaround by enabling chrome://flags/#allow-insecure-localhost disabled. This removes the 'connection not secure warning' in my case and a grey lock with indicates connection is secure and certificate is valid can be seen."
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

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
