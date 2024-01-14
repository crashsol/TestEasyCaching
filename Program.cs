
using AspectCore.Extensions.Hosting;
using EasyCaching.CSRedis;
using TestEasyCaching.Services;
using EasyCaching.Interceptor.AspectCore;
using EasyCaching.Core;
using Microsoft.Extensions.DependencyInjection;
using EasyCaching.Core.Configurations;

namespace TestEasyCaching
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //Ìí¼Óauto factory
            builder.Host.UseServiceContext();



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddEasyCaching(option =>
            {

                option.UseInMemory();


                option.UseCSRedis(config =>
                {
                    config.DBConfig = new CSRedisDBOptions
                    {
                        ConnectionStrings = new System.Collections.Generic.List<string>
                        {
                            "127.0.0.1:6379,defaultDatabase=0,poolsize=10"
                        },
                        //kthe sentinels settings
                        Sentinels = new System.Collections.Generic.List<string>
                        {
                            //"192.169.1.10:26379", "192.169.1.11:26379", "192.169.1.12:26379"
                        },
                        // the read write setting for sentinel mode
                        ReadOnly = false,
                       
                    };
                    config.SerializerName = "json";
                });

                //option.UseCSRedis(builder.Configuration, "DefaultCSRedis", "easycaching:csredis");

                //option.UseRedis(config =>
                //{
                //    config.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                //    config.DBConfig.SyncTimeout = 10000;
                //    config.DBConfig.AsyncTimeout = 10000;
                //    config.SerializerName = "json";

                //}, "redis1");

                option.WithJson();
                //option.WithMessagePack();
              
            });


            builder.Services.AddTransient<IDemoService, DemoService>();
            // AspectCore
            builder.Services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = EasyCachingConstValue.DefaultCSRedisName);

            //builder.Services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName);

            //builder.Services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "redis1");

            //builder.Services.ConfigureCastleInterceptor(options => options.CacheProviderName = "m1");





            var app = builder.Build();




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
