using Microsoft.AspNetCore.Http;
using MovieBuff;
using MovieBuff.Core.Services.LoggingService;
using MovieBuff.Core.Services.MediaService;
using MovieBuff.Core.Services.TicketService;
using MovieBuff.Infrastructure.Services.MediaService;
using MovieBuff.Services.RatingService;
using MovieBuff.Services.ReportingService;
using MovieBuff.Services.TicketService;
using MovieBuff.Web;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MediaServiceCollection
    {
        public static IServiceCollection AddMediaServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggingManager, LoggingManager>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IReportingService, ReportingService>();
            services.AddScoped<ITicketService, TicketService>();
            return services;
        }
    }
}
