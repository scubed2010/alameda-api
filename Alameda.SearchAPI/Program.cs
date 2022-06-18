using Alameda.Business.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var localPolicy = "AlamedaCountyOriginsLocal";
var prodPolicy = "AlamedaCountOriginsProd";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localPolicy,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .WithHeaders(HeaderNames.ContentType);
                      });

    options.AddPolicy(name: prodPolicy,
                      policy =>
                      {
                          policy.WithOrigins("http://search.alameda.gov")
                          .WithHeaders(HeaderNames.ContentType);
                      });
});

// Add services to the container.
builder.Services.AddScoped<TextSearchService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(localPolicy);
}

if (app.Environment.IsProduction())
{
    app.UseCors(prodPolicy);
}

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
