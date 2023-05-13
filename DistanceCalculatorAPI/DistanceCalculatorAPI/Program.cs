using DistanceCalculator.Model.ViewModels;
using DistanceCalculator.Repository.IRepository;
using DistanceCalculator.Repository.Repository;
using DistanceCalculator.Service.IService;
using DistanceCalculator.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("DistanceCalculatorDatabase"));

// Dependency Injection here 
# region DI
builder.Services.AddTransient<IDistanceClculatorRepository, DistanceClculatorRepository>();
builder.Services.AddTransient<IDistanceCalculatorService, DistanceCalculatorService>();
#endregion

//Enable Cors 
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
