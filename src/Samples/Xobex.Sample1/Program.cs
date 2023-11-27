// <copyright file="Program.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Xobex.Domain.Common;
using Xobex.Mediator;
using Xobex.Mediator.Implementation;
using Xobex.Mes.Application.Core.DataTypes;
using Xobex.Mes.Infrastucture.Database;

namespace Xobex.Sample1;

internal class Program
{
    private static IHost s_host = null!;

    private static async Task<int> Main(string[] args)
    {
        try
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            IHostBuilder b = Host.CreateDefaultBuilder(args);
            Console.OutputEncoding = Encoding.UTF8;
            IHostBuilder builder = CreateHostBuilder(args);
            builder.UseSerilog();
            s_host = builder.Build();
            using IHost host = s_host;
            await host.StartAsync();
            await host.WaitForShutdownAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            return ex.HResult;
        }
    }

    public static IConfiguration Configuration { get; private set; } = null!;

    private static void Configure(IHostEnvironment env, IServiceCollection services)
    {
        services.AddLogging((configure) =>
        {
            configure.AddSerilog();
        });
        services.Configure<LoggerConfiguration>(Configuration.GetSection("Serilog"));
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

        string connectionString = "Data Source=localhost;Initial Catalog=MesSample;Trusted_Connection=true;Application Name=Lepton;Trust Server Certificate=true";

        services.AddMesSqlServerDatabase(connectionString);

        services.AddMediator(provider =>
        {
            provider
                .AddAssembly(typeof(Program).Assembly)
                .AddAssembly(typeof(InitializeDataTypesCommandHandler).Assembly);
        });

        services.AddSingleton<IUser>(serviceProvider =>
        {
            return new User([
                new Claim(JwtRegisteredClaimNames.Sub, "1"),
                new Claim(JwtRegisteredClaimNames.Name, "administrator"),
                new Claim(JwtRegisteredClaimNames.Email, "administrator@dykbits.net")
                ]);
        });

        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddHostedService<Worker>();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        IHostBuilder builder = new HostBuilder();
        builder.UseContentRoot(Directory.GetCurrentDirectory());
        builder.ConfigureHostConfiguration(delegate (IConfigurationBuilder config)
        {
            config.AddEnvironmentVariables("DOTNET_");
            if (args != null)
            {
                config.AddCommandLine(args);
            }
        });
        builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            IHostEnvironment env = hostingContext.HostingEnvironment;

            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                Assembly appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                if (appAssembly != null)
                {
                    config.AddUserSecrets(appAssembly, optional: true);
                }
            }
            config.AddEnvironmentVariables();
            if (args != null)
            {
                config.AddCommandLine(args);
            }
        })
        .ConfigureLogging((hostingContext, loggingBuilder) =>
        {
            loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            loggingBuilder.AddEventSourceLogger();
            loggingBuilder.AddDebug();
        })
        .ConfigureServices((context, services) =>
        {
            Configuration = context.Configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            Configure(context.HostingEnvironment, services);
        })
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        });
        return builder;
    }

}
