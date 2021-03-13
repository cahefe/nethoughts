using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebAPIEnvironments.Filters;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Services;
using WebAPIEnvironments.Tests;

namespace WebAPIEnvironments
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        IWebHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            //  Dica: Acrescente a identificação do ambiente poara acesso via mṕetodo "ConfigureServices"
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //  Adicionar middleware: https://dzimchuk.net/understanding-aspnet-5-middleware/

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIEnvironments", Version = "v1" });
            });
            services.AddControllersWithViews(options =>
            {
                //  Carrega o filtro de testes exclusivamente nos ambientes de "Testes_Certificacao"
                if (Env.IsEnvironment("Testes_Certificacao"))
                    options.Filters.Add(typeof(ValidaCenarioCertificacaoResourceFilter));
            });

            //  *** Registro das dependências ***

            //  Dependências de contexto geral (usado em qualquer contexto/cenário)
            //  Exemplo 1: Gerador de Textos
            services.AddSingleton<TextosBogus>();
            services.AddSingleton<TextosFixos>();
            services.AddSingleton<IWideServiceFactory<ITextos>, TextosServiceFactory>();
            //  Exemplo 2: Gerador de Numeros
            services.AddSingleton<NumerosPositivos>();
            services.AddSingleton<NumerosNegativos>();
            services.AddSingleton<IWideServiceFactory<INumeros>, NumerosServiceFactory>();

            //  Gestão de Injeção de dependência (de acordo com os ambientes a acessar - especialmente para apresentar o "Testes_Certificacao")
            if (Env.IsEnvironment("Testes_Certificacao"))
            {
                //  Se ambiente de  "certificação"...

                //  Catálogo de todos os testes disponíveis para certificação
                services.AddScoped<Registrar_Investimento_Titulo_Inexistente>();

                //  ... viabilização das mapeamentos de acordo com o teste requisitado...
                services.AddScoped<DBContext_Tests_Negociacao>();
                services.AddScoped<ICertificationTestService, CertificationTestService>();
                services.AddScoped<ICalculosService>(serviceProvider => ObterImplementacaoParaTeste<ICalculosService>(serviceProvider));
            }
            else
            {
                //  Se aplicação padrão, utiliza as interfaces/implementações padrão para uso nos ambientes
                services.AddScoped<ICalculosService, CalculosService>();

            }
        }
        /// <summary>
        /// Define a implementação a ser utilizada para uma interface de acordo com o cenário solicitado
        /// </summary>
        /// <typeparam name="TInterface">Interface a ser servida (de acordo com o enumerador de teste solicitado)</typeparam>
        /// <param name="serviceProvider">Service provider</param>
        /// <returns>Implementação da interface baseada no cenário de teste solicitado</returns>
        /// <remarks>Esta funão acompanha o projeto de testes / Tratamento de DI</remarks>
        TInterface ObterImplementacaoParaTeste<TInterface>(IServiceProvider serviceProvider)
        {
            var objType = Type.GetType($"{typeof(TestsBaseType).Namespace}.{serviceProvider.GetService<ICertificationTestService>().Cenario}");
            if (objType == null)
                throw new InvalidCastException($"Failed on trying to obtain {nameof(TInterface)}");
            return ((ICertificationCenarios)serviceProvider.GetService(objType)).PrepareScenario<TInterface>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIEnvironments v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
