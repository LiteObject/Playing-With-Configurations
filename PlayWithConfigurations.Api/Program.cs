namespace PlayWithConfigurations.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _ = builder.Services.AddControllers();

            _ = builder.Services.AddEndpointsApiExplorer();

            string myKey = builder.Configuration.GetValue<string>("MyKey");
            _ = builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Play-With-Configurations API",
                    Version = "v1",
                    Description = $"Description: MyKey - {myKey}"
                });
            });

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = $"Financial Integrations API - {app.Environment.EnvironmentName}";
            });

            _ = app.MapFallback(() => Results.Redirect("/swagger"));

            _ = app.UseAuthorization();


            _ = app.MapControllers();

            app.Run();
        }
    }
}