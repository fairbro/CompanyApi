using CompanyApi.Middleware;
using CompanyApi.Options;
using CompanyApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddOptions<CompanyApiOptions>()
    .Bind(builder.Configuration.GetSection("CompanyApi"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<Microsoft.Extensions.Options.IOptions<CompanyApiOptions>>().Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Map("/health", () => Results.Ok());

app.MapGet("/companies/{id}", async (
    int id,
    ICompanyService companyService,
    CancellationToken cancellationToken) =>
{
    var company = await companyService.GetCompanyAsync(id, cancellationToken);

    return Results.Ok(company);
})
.WithName("companies");

app.Run();
