using AirportPanel.WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportPanel.DAL
{
	public class Sturtup
	{
		public static void AddDbContexts(IServiceCollection services)
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
