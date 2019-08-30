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
using Xunit.Frameworks.Autofac;

public class TestFixture
{
    public TestFixture()
    {
        var services = new ServiceCollection();
        services.AddScoped<IUserHelper, WebUserHelper>();
        services.AddScoped<IUserProvider, SqlUserProvider>();
        services.AddScoped<IAccountHelper, WebAccountHelper>();
        services.AddScoped<IAccountProvider, SqlAccountProvider>();
        services.AddScoped<IAreaHelper, WebAreaHelper>();
        services.AddScoped<IAreaProvider, SqlAreaProvider>();
        services.AddScoped<IRoleProvider, SqlRoleProvider>();
        services.AddScoped<ITechnologyHelper, WebTechnologyHelper>();
        services.AddScoped<ITechnologyProvider, SqlTechnologyProvider>();
        services.AddScoped<IPossibleRoleProvider, SqlPossibleRoleProvider>();


        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new SqlProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        var connection = @"data source=srv-db-dev;initial catalog=Test42_Copia_Test;persist security info=True;user id=Test42User;password=Test42Password;";
        services.AddDbContext<SqlModelsContext>
            (options => options
                       .UseLazyLoadingProxies()
                       .UseSqlServer(connection));


        ServiceProvider = services.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; private set; }
}