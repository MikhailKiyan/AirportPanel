using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirportPanel.WebApplication.Data
{
    public class AirportPanelSecurityDbContext : IdentityDbContext
    {
        public AirportPanelSecurityDbContext(DbContextOptions<AirportPanelSecurityDbContext> options)
            : base(options)
        {
        }
    }
}
