using GameAPI.Background;
using GameAPI.DataStorage;
using GameAPI.Options;
using GameAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServiceSettings>
    (builder.Configuration.GetSection(nameof(ServiceSettings)));
builder.Services.AddSingleton<ISettingsServices, SettingsServices>();

builder.Services.AddSingleton<IGameLobbysStorage, GameLobbysStorage>();

builder.Services.AddHostedService<GameLobbysDeleterBackground>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();