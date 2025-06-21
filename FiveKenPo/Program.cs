using FiveKenPo.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços pro container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adiciona como singletron para garantir uso do mesmo objeto.
builder.Services.AddSingleton<GameService>();

var app = builder.Build();

// Configura o canal para requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
