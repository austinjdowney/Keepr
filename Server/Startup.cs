using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Server.Repositories;
using Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // TODO[epic=Auth] copy/paste
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = $"https://{Configuration["AUTH0_DOMAIN"]}/";
        options.Audience = Configuration["AUTH0_AUDIENCE"];
      });

      services.AddCors(options =>
      {
        options.AddPolicy("CorsDevPolicy", builder =>
              {
                builder
                        .WithOrigins(new string[]{
                          "http://localhost:8080",
                                "http://localhost:8081"
                              })
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
              });
      });
      // end copy/paste

      services.AddControllers();

      // REPOS
      services.AddScoped<AccountRepository>();
      services.AddTransient<VaultsRepository>();
      services.AddTransient<KeepsRepository>();
      services.AddTransient<VaultKeepsRepository>();
      // BUSINESS LOGIC
      services.AddScoped<AccountService>();
      services.AddTransient<VaultsService>();
      services.AddTransient<KeepsService>();
      services.AddTransient<VaultKeepsService>();


      // TODO[epic=DB] database Connection
      services.AddScoped<IDbConnection>(x => CreateDbConnection());



      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Keepr", Version = "v1" });
      });
    }

    // TODO[epic=DB] database Connection
    private IDbConnection CreateDbConnection()
    {
      string connectionString = Configuration["CONNECTION_STRING"];
      return new MySqlConnection(connectionString);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Keepr v1"));
        // TODO[epic=Auth] Add Cors Policy when in development mode
        app.UseCors("CorsDevPolicy");
      }

      app.UseHttpsRedirection();

      // creating a folder wwwroot and index.html inside of it.. will pop up the index.html as if in client
      //Similar to npm run build

      //Go to vue configure.js and adjust the output to ../final.server/wwwroot
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseRouting();

      // TODO[epic=Auth] Add Authenentication so bearer gets validated
      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
