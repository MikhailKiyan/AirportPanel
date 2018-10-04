using AirportPanel.WebApplication.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportPanel.DAL
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			this.Configuration = configuration;
			this.Environment = environment;
		}

		public IConfiguration Configuration { get; }

		public IHostingEnvironment Environment { get; }

		public void AddDbContexts(IServiceCollection services)
		{
			services.AddDbContext<AirportPanelSecurityDbContext>(options =>
				options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AirportPanelSecurity;Trusted_Connection=True;MultipleActiveResultSets=true"));

			services.AddDbContext<AirportPanelDataDbContext>(options =>
				options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AirportPanelData;Trusted_Connection=True;MultipleActiveResultSets=true"));
			/*
			services.AddDbContext<AirportPanelSecurityDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection")));

			services.AddDbContext<AirportPanelDataDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("AirportPanelConnection")));
			*/
		}
	}
}
