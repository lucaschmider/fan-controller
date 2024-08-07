using DataCollector.Repository.Database;
using DataCollector.Repository.Serial;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDatabase(defaultConnectionString);

var serialModuleConfiguration = builder.Configuration
    .GetSection("Serial")
    .Get<SerialModuleConfiguration>()!;
builder.Services.AddSerial(serialModuleConfiguration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();