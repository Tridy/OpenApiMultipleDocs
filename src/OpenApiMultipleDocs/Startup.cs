using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MultipleDocs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
        }

        // called by runtime
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc(options =>
            {
                options.Conventions.Add(new ApiExplorerGroupPerControllerConvention());
            });

            services.AddSwaggerGen(GenerateDefinitionForEachController);

            services.AddEndpointsApiExplorer();
        }

        void GenerateDefinitionForEachController(SwaggerGenOptions c)
        {
            IEnumerable<Type> controllers = typeof(Startup).Assembly.GetTypes().Where(t => t.BaseType == typeof(ControllerBase));

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);

                c.SwaggerDoc(controllerName, new OpenApiInfo { Title = controllerName, Version = "v1" });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionController = apiDesc.ActionDescriptor.RouteValues["controller"];
                    return docName == controllerName;
                });
            }

            c.DocInclusionPredicate(IncludeDocumentPredicate);
        }

        static bool IncludeDocumentPredicate(string docName, ApiDescription apiDesc)
        {
            if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            if (methodInfo.DeclaringType == null) return false;

            string typeName = methodInfo.DeclaringType.Name;
            string groupName = typeName.Replace("Controller", string.Empty);

            return groupName == docName;
        }

        // called by runtime
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            SpecifyEndpointForEachController(app);
        }

        void SpecifyEndpointForEachController(IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                foreach (var controller in typeof(Program).Assembly.GetTypes().Where(t => t.BaseType == typeof(ControllerBase)))
                {
                    var controllerName = controller.Name.Replace("Controller", "");
                    c.SwaggerEndpoint($"/swagger/{controllerName}/swagger.json", controllerName);
                }
            });
        }
    }
}