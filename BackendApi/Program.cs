using Microsoft.EntityFrameworkCore;
using WEBAPIDEMO;
using WEBAPIDEMO.Data;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Repository;
Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:6005");

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddTransient<Seed>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDBContext<DataContext>;
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


//args → condition → scope → Seed → DbContext → SaveChanges

//args- command line argument passed when staring the app 
//args.length == 1 - exactly one aegument was provided 
// args[0].toLower() == "seeddata"- 
//seeddata(app)- calls the seeding methods 
if (args.Length == 1 && args[0].ToLower() == "seeddata")// condiatioanlly sending trigger 
    SeedData(app); // this checks how the app was started 

void SeedData(IHost app) // receives applicarion host . this gives an access to dependecy injection , application servicr app lifetime 
{// seed depennds on datacontext , datacontext is registered as scoped , scoped services must run inside a scope 
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
//scope here one logical unit of work , one lifetime  like dbcontext 
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}
// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

