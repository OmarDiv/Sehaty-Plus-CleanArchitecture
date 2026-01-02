using Hangfire;
using HangfireBasicAuthenticationFilter;
using Scalar.AspNetCore;
using Sehaty_Plus;
using Sehaty_Plus.Application;
using Sehaty_Plus.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentation(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHangfireDashboard("/Jobs",
    new DashboardOptions
    {
        Authorization = new[]
        {
            new HangfireCustomBasicAuthenticationFilter
            {
                User = builder.Configuration.GetValue<string>("HangfireSettings:user"),
                Pass = builder.Configuration.GetValue<string>("HangfireSettings:password")
            }
        }
    }
);
//Apply pending migrations at application startup With Docker
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseCors();
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();

app.Run();
