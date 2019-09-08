using Autofac;
using AutoMapper;
using AutoMapper.Configuration;
using Helper.Web;
using Helper.Web.Contextes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using Provider.Sql;
using Provider.Sql.SqlProviders;
using Provider.Sql.SqlProviders.SqlContextesProvider;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

public class TestFixture
{
    public TestFixture()
    {
        var services = new ServiceCollection();
        services.AddScoped<IUserHelper, WebUserHelper>();
        services.AddScoped<IUserProvider, SqlUserProvider>();
        services.AddScoped<IAccountHelper, WebAccountHelper>();
        services.AddScoped<IAccountProvider, SqlAccountProvider>();
        services.AddScoped<IRequestHelper, WebRequestHelper>();
        services.AddScoped<IRequestProvider, SqlRequestProvider>();
        services.AddScoped<IVeicleHelper, WebVeicleHelper>();
        services.AddScoped<IVeicleProvider, SqlVeicleProvider>();


        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new SqlProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        var connection = @"Server=tcp:lillo-server.database.windows.net,1433;Initial Catalog=AzureDb;Persist Security Info=False;User ID=Lillo;Password=Oronzo123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        services.AddDbContext<SqlModelsContext>
            (options => options
                       .UseLazyLoadingProxies()
                       .UseSqlServer(connection));


        ServiceProvider = services.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; private set; }
}