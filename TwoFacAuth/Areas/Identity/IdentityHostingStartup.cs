using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoFacAuth.Areas.Identity.Data;
using TwoFacAuth.Data;

[assembly: HostingStartup(typeof(TwoFacAuth.Areas.Identity.IdentityHostingStartup))]
namespace TwoFacAuth.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TwoFacAuthContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TwoFacAuthContextConnection")));

                services.AddDefaultIdentity<TwoFacAuthUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TwoFacAuthContext>();
            });
        }
    }
}