var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true; // Optional: To respect the client's Accept header
})
.AddJsonOptions(options =>
{
    // Optional: Customize JSON options here if needed
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // If you want to avoid camel case naming
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


