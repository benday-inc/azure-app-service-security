using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Benday.EasyAuthDemo.Api.DataAccess;
using Benday.EasyAuthDemo.Api;
using Benday.EasyAuthDemo.Api.ServiceLayers;
using Benday.EasyAuthDemo.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;
using Benday.EasyAuthDemo.Api.DataAccess.SqlServer;
using Benday.EasyAuthDemo.Api.AzureStorage;
using Benday.EasyAuthDemo.Api.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Benday.EasyAuthDemo.WebUi.Models;
using Benday.EasyAuthDemo.WebUi.Security;
using Benday.EasyAuthDemo.Api.Logging;

namespace Benday.EasyAuthDemo.WebUi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            ConfigureSecurity(services);
            
            QueueTypeRegistrations(services);
            
            RegisterTypesForDbContexts(services);
            RegisterTypesForAzureStorageBlobs(services);
            RegisterTypesForEmail(services);
            RegisterQueuedTypes(services);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            ConfigureLogging(app, loggerFactory);
            
            ConfigureHttps(app);
            
            app.UseStaticFiles();
            
            app.UseRouting();
            
            ConfigureSecurity(app);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Person}/{action=Index}/{id?}");
            });
        }
        
        partial void BeforeQueueTypeRegistrations(IServiceCollection services);
        partial void AfterQueueTypeRegistrations(IServiceCollection services);
        
        protected virtual void QueueTypeRegistrations(IServiceCollection services)
        {
            BeforeQueueTypeRegistrations(services);
            QueueTypeRegistration<IHttpContextAccessor, HttpContextAccessor>(ServiceLifetime.Singleton);
            
            QueueTypeRegistration<ISecurityConfiguration, SecurityConfiguration>();
            QueueTypeRegistration<IClaimsAccessor, HttpContextClaimsAccessor>();
            // QueueTypeRegistration<IUsernameProvider, HttpContextUsernameProvider>();
            QueueTypeRegistration<IUserInformation, UserInformation>();
            QueueTypeRegistration<IUsernameProvider, UserInformation>();
            QueueTypeRegistration<ISearchStringParserStrategy, DefaultSearchStringParserStrategy>();
            QueueTypeRegistrationsForPopulateClaimsMiddleware();
            QueueTypeRegistrationsForServiceLayers();
            QueueTypeRegistrationsForValidators();
            QueueTypeRegistrationsForRepositories();
            
            AfterQueueTypeRegistrations(services);
        }
        
        protected virtual void RegisterTypesForDbContexts(IServiceCollection services)
        {
            services.AddDbContext<EasyAuthDemoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("default"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            
            QueueTypeRegistration<IEasyAuthDemoDbContext, EasyAuthDemoDbContext>();
        }
        
        protected List<ITypeRegistrationItem> _QueuedTypeRegistrations = new List<ITypeRegistrationItem>();
        
        protected virtual void QueueTypeRegistration<TService>(
            ServiceLifetime instancingMode = ServiceLifetime.Transient)
            where TService : class
        {
            _QueuedTypeRegistrations.Add(new TypeRegistrationItem<TService, TService>(instancingMode));
        }
        
        protected virtual void QueueTypeRegistration<TService, TImplementation>(
            ServiceLifetime instancingMode = ServiceLifetime.Transient)
            where TService : class
            where TImplementation : class, TService
        {
            _QueuedTypeRegistrations.Add(new TypeRegistrationItem<TService, TImplementation>(instancingMode));
        }
        
        partial void BeforeRegisterQueuedTypes(IServiceCollection services);
        partial void AfterRegisterQueuedTypes(IServiceCollection services);
        
        protected virtual void RegisterQueuedTypes(IServiceCollection services)
        {
            BeforeRegisterQueuedTypes(services);
            
            foreach (var item in _QueuedTypeRegistrations)
            {
                if (item.Lifetime == ServiceLifetime.Transient)
                {
                    services.AddTransient(item.ServiceType, item.ImplementationType);
                }
                else if (item.Lifetime == ServiceLifetime.Scoped)
                {
                    services.AddScoped(item.ServiceType, item.ImplementationType);
                }
                else if (item.Lifetime == ServiceLifetime.Singleton)
                {
                    services.AddSingleton(item.ServiceType, item.ImplementationType);
                }
                else
                {
                    throw new InvalidOperationException($"Unsupported instancing mode '{item.Lifetime}'.");
                }
            }
            
            AfterRegisterQueuedTypes(services);
        }
        
        protected virtual void ConfigureSecurity(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UsePopulateClaimsMiddleware();
            app.UseAuthorization();
        }
        
        protected virtual void ConfigureSecurity(IServiceCollection services)
        {
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
        }
        
        protected virtual void ConfigureHttps(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
        }
        
        protected virtual void RegisterTypesForEmail(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            
            services.AddOptions<EmailConfigurationOptions>().Configure(options =>
            {
                options.FromEmail = Configuration.GetValue<string>("Email:FromEmail");
                options.FromName = Configuration.GetValue<string>("Email:FromName");
                options.SendGridApiKey = Configuration.GetValue<string>("Email:SendGridApiKey");
            });
            //services.AddTransient<IPaymentOrderPermissionsService, PaymentOrderPermissionsService>();
        }
        
        protected virtual void QueueTypeRegistrationsForRepositories()
        {
            var lifetime = ServiceLifetime.Scoped;
            
            QueueTypeRegistration<IConfigurationItemRepository, SqlEntityFrameworkConfigurationItemRepository>(lifetime);
            QueueTypeRegistration<IFeedbackRepository, SqlEntityFrameworkFeedbackRepository>(lifetime);
            QueueTypeRegistration<ILogEntryRepository, SqlEntityFrameworkLogEntryRepository>(lifetime);
            QueueTypeRegistration<ILookupRepository, SqlEntityFrameworkLookupRepository>(lifetime);
            QueueTypeRegistration<IPersonRepository, SqlEntityFrameworkPersonRepository>(lifetime);
            QueueTypeRegistration<IUserRepository, SqlEntityFrameworkUserRepository>(lifetime);
            QueueTypeRegistration<IUserClaimRepository, SqlEntityFrameworkUserClaimRepository>(lifetime);
        }
        
        protected virtual void QueueTypeRegistrationsForValidators()
        {
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.ConfigurationItem>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.ConfigurationItem>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.ConfigurationItemEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.ConfigurationItemEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Feedback>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Feedback>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.FeedbackEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.FeedbackEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.LogEntry>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.LogEntry>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.LogEntryEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.LogEntryEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Lookup>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Lookup>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.LookupEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.LookupEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Person>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Person>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.PersonEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.PersonEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.User>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.User>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.UserEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.UserEditorViewModel>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.UserClaim>, DefaultValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.UserClaim>>();
            QueueTypeRegistration<IValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.UserClaimEditorViewModel>, 	DefaultValidatorStrategy<Benday.EasyAuthDemo.WebUi.Models.UserClaimEditorViewModel>>();
        }
        
        protected virtual void QueueTypeRegistrationsForServiceLayers()
        {
            QueueTypeRegistration<IConfigurationItemService, ConfigurationItemService>();
            QueueTypeRegistration<IFeedbackService, FeedbackService>();
            QueueTypeRegistration<ILogEntryService, LogEntryService>();
            QueueTypeRegistration<ILookupService, LookupService>();
            QueueTypeRegistration<IPersonService, PersonService>();
            QueueTypeRegistration<IUserService, UserService>();
            QueueTypeRegistration<IUserClaimService, UserClaimService>();
        }
        
        protected virtual void RegisterTypesForAzureStorageBlobs(IServiceCollection services)
        {
            // azure images
            services.AddOptions<AzureBlobImageStorageOptions>().Configure(options =>
            {
                options.UseDevelopmentStorage = Configuration.GetValue<bool>("AzureStorage:UseDevelopmentStorage");
                options.AccountKey = Configuration.GetValue<string>("AzureStorage:AccountKey");
                options.AccountName = Configuration.GetValue<string>("AzureStorage:AccountName");
                options.ContainerName = Configuration.GetValue<string>("AzureStorage:ContainerName");
                options.ReadTokenExpirationInSeconds = Configuration.GetValue<int>("AzureStorage:ReadTokenExpirationInSeconds");
            });
            
            services.AddTransient<IAzureBlobImageSasTokenGenerator, AzureBlobImageStorageHelper>();
        }
        
        protected virtual void ConfigureLogging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            BeforeConfigureLogging(app, loggerFactory);
            
            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
            
            var connectionString = config.GetConnectionString("default");
            
            loggerFactory.AddProvider(
                new SqlDatabaseLoggerProvider(
                new SqlDatabaseLoggerOptions()
            {
                LogLevel = LogLevel.Information,
                    ConnectionString = connectionString
            }
            ));
            
            AfterConfigureLogging(app, loggerFactory);
        }
        
        partial void BeforeConfigureLogging(IApplicationBuilder app, ILoggerFactory loggerFactory);
        partial void AfterConfigureLogging(IApplicationBuilder app, ILoggerFactory loggerFactory);
        
        protected virtual void ConfigureAuthentication(IServiceCollection services)
        {
            var config = new SecurityConfiguration(Configuration);
            
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
            {
                options.LoginPath = new PathString(config.LoginPath);
                options.LogoutPath = new PathString(config.LogoutPath);
                options.AccessDeniedPath = new PathString(config.LoginPath);
            });
        }
        
        protected virtual void ConfigureAuthorization(IServiceCollection services)
        {
            QueueTypeRegistration<IAuthorizationHandler, LoggedInUsingEasyAuthHandler>(ServiceLifetime.Singleton);
            QueueTypeRegistration<IAuthorizationHandler, RoleAuthorizationHandler>(ServiceLifetime.Singleton);
            QueueTypeRegistration<IAuthorizationHandler, ClaimAuthorizationHandler>(ServiceLifetime.Singleton);
            QueueTypeRegistration<IRouteDataAccessor, HttpContextRouteDataAccessor>();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SecurityConstants.Policy_LoggedInUsingEasyAuth,
                    policy => policy.Requirements.Add(
                    new LoggedInUsingEasyAuthRequirement()));
                
                options.AddPolicy(SecurityConstants.Policy_IsAdministrator,
                    policy => policy.Requirements.Add(
                    new RoleAuthorizationRequirement(
                    SecurityConstants.RoleName_Admin)));
                
                options.DefaultPolicy = options.GetPolicy(SecurityConstants.Policy_LoggedInUsingEasyAuth);
            });
        }
        
        protected virtual void QueueTypeRegistrationsForPopulateClaimsMiddleware()
        {
            QueueTypeRegistration<PopulateClaimsMiddleware>();
            QueueTypeRegistration<ISecurityConfiguration, SecurityConfiguration>();
        }
    }
}