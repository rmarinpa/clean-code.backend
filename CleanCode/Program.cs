using System.Globalization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddHttpClient()
    .AddInfraestructuraServices(builder.Configuration)
    .Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "es-CL", uiCulture: "es-CL");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture("es-CL")
    .AddSupportedCultures(new[] { "es-CL" })
    .AddSupportedUICultures(new[] { "es-CL" }));

app.UseAuthentication();

app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.UseHealthChecks("/healtz");
app.Map("/", context =>
{
    context.Response.StatusCode = (int)HttpStatusCode.OK;
    return Task.CompletedTask;
});

app.Run();

