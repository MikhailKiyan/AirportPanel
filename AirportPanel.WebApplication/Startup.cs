

namespace AirportPanel.WebApplication
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.OData.Edm;
	using Microsoft.AspNet.OData.Extensions;
	using Microsoft.AspNet.OData.Builder;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.HttpsPolicy;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using Newtonsoft.Json.Serialization;

	using AirportPanel.WebApplication.Data;
	using AirportPanel.Model.EntityModels;
	using AirportPanel.Contract;
	using AirportPanel.Utility.DB;

	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment environment) {
			this.Configuration = configuration;
			this.Environment = environment;
		}

		public IConfiguration Configuration { get; }

		public IHostingEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// services.AddSingleton<AirportPanelSecurityDbContext>(_ => AirportPanelSecurityDbContext.Create());
			// services.AddSingleton<IRepository<Flight>, Repository<Flight>>();
			// services.AddSingleton<IRepository<FlightStatus>, Repository<FlightStatus>>();
			// services.AddSingleton<IUnitOfWork, UnitOfWork>();



			services.Configure<CookiePolicyOptions>(options => {
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			new DAL.Startup(this.Configuration, this.Environment)
				.AddDbContexts(services);
		
			services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AirportPanelSecurityDbContext>();

			services.AddOData();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				// Added for Kendo
				// Maintain property names during serialization. See:
				// https://github.com/aspnet/Announcements/issues/19
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			// Added for Kendo
			services.AddKendo();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
				/*
				routes.MapODataServiceRoute(
					routeName: "odata",
					routePrefix: "odata",
					model: GetEdmModel());*/
			});

			app.UseKendo(env);

			app.UseOData(routeName: "odata", routePrerix: "odata", model: GetEdmModel());
		}

		private static IEdmModel GetEdmModel() {
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Flight>("Flights");
			builder.EntitySet<FlightStatus>("FlightStatuses");
			return builder.GetEdmModel();
		}
	}
}
