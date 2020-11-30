using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        Log.Logger = new LoggerConfiguration()
          .Enrich.WithProperty("Application", "Calculator")
          .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
          .WriteTo.Console()
          .WriteTo.File("calculator.log")
          .CreateLogger();

        Log.Information("Application started");
    }
}