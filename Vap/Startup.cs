using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Helper.Web;
using Helper.Web.Contextes;
using Helper.Web.HatHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.OAuth;
using Models;
using Models.Interfaces.Helpers;
using Models.Interfaces.Providers;
using Models.Resources;
using Newtonsoft.Json;
using Provider.Sql;
using Provider.Sql.SqlProviders;
using Provider.Sql.SqlProviders.SqlContextesProvider;
using Vap.Extensions;

using Vap.Profile;
using IModelBinder = Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder;
using IModelBinderProvider = Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinderProvider;

namespace Vap
{
    public class Startup
    {
        
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        public Startup(IConfiguration configuration)
        {
            PublicClientId = "web";
            this.Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<IUserHelper, WebUserHelper>();
            services.AddScoped<IUserProvider, SqlUserProvider>();
            services.AddScoped<IAccountHelper, WebAccountHelper>();
            services.AddScoped<IAccountProvider, SqlAccountProvider>();
            services.AddScoped<IResolutorFacade, ResolutorFacade>();
            services.AddScoped<IRequestHelper, WebRequestHelper>();
            services.AddScoped<IRequestProvider, SqlRequestProvider>();
            services.AddScoped<IVeicleHelper, WebVeicleHelper>();
            services.AddScoped<IVeicleProvider, SqlVeicleProvider>();
            services.AddScoped<IFileService, BlobService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SqlProfile());
                mc.AddProfile(new FrontEndProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var connectionStringSql = Configuration.GetConnectionString("DefaultConnection");
            var connectionBlobCloud = Configuration.GetConnectionString("CloudConnection");


            /*   string storageConnectionString = Environment.GetEnvironmentVariable("DefaultEndpointsProtocol=https;AccountName=vapdev;AccountKey=28hpcto4rpAQJcK/Zqmk5sa1Qm6OaKODliNulnwLl7FYcNBFeoGa5WKcaGYcHT0k1Q2oTYyCVmyhMCry2UetXA==;EndpointSuffix=core.windows.net");

               // Check whether the connection string can be parsed.
               CloudStorageAccount storageAccount;
               if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
               {
               }
               else
               {
                   // Otherwise, let the user know that they need to define the environment variable.
                   Console.WriteLine(
                       "A connection string has not been defined in the system environment variables. " +
                       "Add an environment variable named 'storageconnectionstring' with your storage " +
                       "connection string as a value.");
                   Console.WriteLine("Press any key to exit the sample application.");
                   Console.ReadLine();
               }
               */


            //var connection = @"data source=srv-db-dev;initial catalog=Test42;persist security info=True;user id=Test42User;password=Test42Password;";
            //var testConnectionString = @"data source=srv-db-dev;initial catalog=Test42_Copia_Test;persist security info=True;user id=Test42User;password=Test42Password;";
            //OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            //{

            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    Provider = new ApplicationOAuthProviderApi(),
            //    AuthorizeEndpointPath = new PathString("/api/AccountApi/login"),
            //    ApplicationCanDisplayErrors = true,

            //    RefreshTokenProvider = new SimpleRefreshTokenProvider(),
            //    AccessTokenFormat = new CustomJwtFormat(), //localhost?


            //};
            //services.AddAuthentication().AddOAuth("", services => Oa);
            services.AddDbContext<SqlModelsContext>
                (options => options
                           .UseLazyLoadingProxies()
                           .UseSqlServer(connectionStringSql));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect((Action<OpenIdConnectOptions>)(options =>
            {
                options.Authority = "https://login.microsoftonline.com/99169c38-99a6-4511-9596-51869cca9f6e";  //endpoint OIDC che voglio utilizzare ok
                options.ClientId = "10415ac4-186b-4ce6-954a-e234a72f0dd0";    //ok
                options.CallbackPath = "/Auth/Logged";      //url di risposta ed è pannello URL di risposta in Azure AD
                options.SignedOutRedirectUri = "/Auth/LoggedOut";
                options.Events.OnTicketReceived = async ctx =>
                  {
                      string authenticationType = "authenticationType";
                      var myServices = ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(services);
                      IUserHelper userHelper = myServices.GetService<IUserHelper>();
                      IResolutorFacade resolutorFacade = myServices.GetService<IResolutorFacade>();
                      //services.ApplicationServices
                      //userHelper = DependencyResolver.Current.GetService<IUserHelper>();
                      // userHelper = this.Configuration.Get<IUserHelper>();
                      ClaimsIdentity claims = new ClaimsIdentity(authenticationType);



                      var email = ctx.Principal.FindFirstValue(ClaimTypes.Upn);
                      //var ajeje = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Upn).Value;

                      //ctx.Principal.FindFirst(a=>a.)
                      var user = await resolutorFacade.UserAsync(email);
                      // ci.AddClaim(new Claim(accounts, ajeje));
                      claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                      claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                      claims.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));
                      claims.AddClaim(new Claim(ClaimTypes.Thumbprint, user.Image));

                      var accounts = user.Assignements.Select(x => x.Account);

                      if (accounts.Count() == 0)
                      {
                          throw new Exception(Resource.not_authorized);
                      }

                      //var authClaims = "claims";

                      foreach (var account in accounts)
                      {
                          //ci.Add(new ClaimsPrincipal(accounts));
                          claims.AddClaim(new Claim("account", JsonConvert.SerializeObject(account)));
                      }


                      var defaultAccount = accounts.FirstOrDefault(x => x.IsDefault);
                      if (defaultAccount == null)
                      {
                          defaultAccount = accounts.FirstOrDefault();
                      }

                      claims.AddClaim(new Claim("selectedAccountId", defaultAccount.Id));
                      claims.AddClaim(new Claim("selectedAccountEmail", defaultAccount.Email));

                      //foreach(var role in defaultAccount.Roles)
                      //{
                      //    claims.AddClaim(new Claim(ClaimTypes.Role, role.DefaultRole.Name));
                      //}

                      claims.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(AccountantTypes), defaultAccount.AccountType)));

                      claims.AddClaim(new Claim("selectedAccount", JsonConvert.SerializeObject(defaultAccount)));

                      ctx.Principal.AddIdentity(claims);

                  };
            }))
             .AddCookie(options =>
             {
                 options.LoginPath = "/auth/login";
             });

            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });
            services.AddMvc(
            options =>
            {
                options.ModelBinderProviders.Insert(0, new MyBinderProvider());

                options.AllowValidatingTopLevelNodes = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id= UrlParameter.Optional}");
            });



        }
    }

    public class MyBinderProvider : IModelBinderProvider
    {
        public Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else if (context.Metadata.ModelType == typeof(Account))
            {
                return new AccountBinder();
            }
            else if (context.Metadata.ModelType == typeof(Assignement))
            {
                return new AssignementBinder();
            }
            return null;
        }
    }

    public class AccountBinder : IModelBinder
    {
        public Task BindModelAsync(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext bindingContext)
        {
            var discriminator = bindingContext.ValueProvider.GetValue("AccountType").ToString();
            var myType = AccountExtensions.Type(discriminator);
            var result = Account.GetInstanceOf(myType);
            result.Id = bindingContext.ValueProvider.GetValue("Id").ToString();
            result.Nickname = bindingContext.ValueProvider.GetValue("Nickname").ToString();
            result.Email = bindingContext.ValueProvider.GetValue("Email").ToString();
            //  result.User.Id = bindingContext.ValueProvider.GetValue("User.Id").ToString();
            //bindingContext.ModelState.SetModelValue(
            //        bindingContext.ModelName, );
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
            //return ModelBindingResult.Success(result);
        }


    }
    public class AssignementBinder : IModelBinder
    {
        public Task BindModelAsync(Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext bindingContext)
        {
            var discriminator = bindingContext.ValueProvider.GetValue("Account.AccountType").ToString();
            var myType = AccountExtensions.Type(discriminator);
            var r = new Assignement
            {
                Account = Account.GetInstanceOf(myType)
            };
            r.Account.Id = bindingContext.ValueProvider.GetValue("Account.Id").ToString();
            r.Account.Nickname = bindingContext.ValueProvider.GetValue("Account.Nickname").ToString();
            r.Account.Email = bindingContext.ValueProvider.GetValue("Account.Email").ToString();
            r.User.Name = bindingContext.ValueProvider.GetValue("User.Name").ToString();
            r.User.Surname = bindingContext.ValueProvider.GetValue("User.Surname").ToString();
            //var result = Account.GetInstanceOf(discriminator);
            //result.Id = bindingContext.ValueProvider.GetValue("Account.Id").ToString();
            //result.Nickname = bindingContext.ValueProvider.GetValue("Account.Nickname").ToString();
            //result.Email = bindingContext.ValueProvider.GetValue("Account.Email").ToString();
            //  result.User.Id = bindingContext.ValueProvider.GetValue("User.Id").ToString();
            //bindingContext.ModelState.SetModelValue(
            //        bindingContext.ModelName, );
            bindingContext.Result = ModelBindingResult.Success(r);
            return Task.CompletedTask;
            //return ModelBindingResult.Success(result);
        }


    }
}
