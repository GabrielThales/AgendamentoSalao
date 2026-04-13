var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a exploração de endpoints (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o Swagger (útil para testarmos quando o Docker subir)
app.UseSwagger();
app.UseSwaggerUI();

// Um endpoint simples de Healthcheck para provar que a API subiu
app.MapGet("/", () => "API do Salão de Beleza está rodando perfeitamente!");

app.Run();