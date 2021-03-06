using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TwitterServer.GraphQL;
using TwitterServer.Auth;
using TwitterServer.Data;
using TwitterServer.Services.Interfaces;
using TwitterServer.Services.Services;

namespace TwitterServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region dbcontext    

            var connectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(connectionStr));
            #endregion
        
            services.AddScoped<ITwitterUserRepository,TwitterUserRepository>();  
            services.AddScoped<ITwitteRepository, TwitteRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    ValidIssuer = "issuer",
                    RequireSignedTokens = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
            services.AddGraphQL(
            SchemaBuilder
                .New()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddAuthorizeDirectiveType()
                .Create()
        );
            services.AddQueryRequestInterceptor(AuthenticationInterceptor());
            
        }
        private static OnCreateRequestAsync AuthenticationInterceptor()
        {
            
            return (context, builder, token) =>
            {
                if (context.GetUser().Identity.IsAuthenticated)
                {
                    builder.SetProperty("currentUser",
                        new CurrentUser(Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                            context.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToList()));
                }

                return Task.CompletedTask;
            };
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication(); 

            app.UsePlayground();
            app.UseGraphQL();


        }
    }
}
