using AutoMapper;

//using DevStore.Catalog.Application.AutoMapper;
//using DevStore.Catalog.Data;
//using DevStore.Catalog.Domain.Events;
using DevStore.Core.Messages;
//using DevStore.Finance.Business.Events;
//using DevStore.Finance.Data;
//using DevStore.Sales.Application;
//using DevStore.Sales.Application.Commands;
//using DevStore.Sales.Data;
using DevStore.WebApp.MVC.Data;
using DevStore.WebApp.MVC.Setup;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;

namespace DevStore.WebApp.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var nomeFila = "fila_rebus";

            services.AddRebus(configure => configure
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), nomeFila))
                //.Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila))
                .Subscriptions(s => s.StoreInMemory())
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(nomeFila);
                        //.MapAssemblyOf<InicializeOrderCommand>(nomeFila)
                        //.MapAssemblyOf<StartOrderCommand>(nomeFila)
                        //.MapAssemblyOf<FinishOrderCommand>(nomeFila)
                        //.MapAssemblyOf<CancelOrderAndDisrollFromCourseCommand>(nomeFila)
                        //.MapAssemblyOf<CancelOrderCommand>(nomeFila);
                })
                .Sagas(s => s.StoreInMemory())
                .Options(o =>
                {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(1);
                    o.SetBusName("Demo Rebus");
                })
            );

            // Register handlers 
            //services.AutoRegisterHandlersFromAssemblyOf<PaymentEventHandler>();
            //services.AutoRegisterHandlersFromAssemblyOf<CourseEventHandler>();
            //services.AutoRegisterHandlersFromAssemblyOf<OrderSaga>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<CatalogContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<SalesContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<FinanceContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            //services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

            services.AddMediatR(typeof(Startup));

            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ShowRoom}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
