

using GreeneKing.Talks.SpeakerRegistration.Domain.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RegistrationStandardRules>(builder.Configuration.GetSection("SpeakerRegistrationStandardRules"));
builder.Services.Configure<RegistrationFeeRules>(builder.Configuration.GetSection("SpeakerRegistrationFeeRules"));

var app = builder.Build();

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
