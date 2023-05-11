using InternalMessagingSystem.Version1._2.Data;
using InternalMessagingSystem.Version1._2.Interfaces;
using InternalMessagingSystem.Version1._2.UseCases;

namespace InternalMessagingSystem.Version1._2;

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
        services.AddControllers();
        // Add other services as needed
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
