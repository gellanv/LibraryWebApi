using Library.Services;
using LibraryWebApi.Validation;

namespace LibraryWebApi.Extentions
{
    public static class ServiceProviderExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<BookService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<RatingService>();

            services.AddScoped<BookValidation>();
            services.AddScoped<ReviewValidation>();
            services.AddScoped<CustomValidation>();
        }
    }
}
