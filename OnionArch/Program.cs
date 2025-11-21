using API.Extensions;
using Microsoft.OpenApi;
using Presentation.Filters;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

// ✅ Proper Swagger configuration with version info
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API documentation for my Onion Architecture project."
    });

    // ✅ Optionally include XML comments (recommended)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});
// register App DI registrations.
builder.Services.AddApplicationRequiredServices(builder.Configuration);

// register the api filters and reference the presntation layer to use the controllers.
builder.Services.AddControllers(
    options => { options.Filters.Add<ValidateModelAttribute>(); }
    ).AddApplicationPart(typeof(Presentation.AssemplyReference).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(op =>
    {
        op.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1;
       
    });
    app.UseSwaggerUI(op=>
    {
        
    });
    app.UseDeveloperExceptionPage();
}
// call to use the custom Global Exception Handler Middleware
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
