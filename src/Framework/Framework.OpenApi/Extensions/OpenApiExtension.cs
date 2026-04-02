using Framework.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace Framework.OpenApi
{
    public static class FrameworkOpenApiExtensions
    {
        public static void AddFrameworkOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                    document.Components.SecuritySchemes[HttpConstant.HEADER] = new OpenApiSecurityScheme
                    {
                        Scheme = "oauth2",
                        Name = HttpConstant.HEADER,
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    };

                    var appName = App.AppName;
                    var index = appName.IndexOf(DelimitersConstant.DOT);
                    var route = index >= 0 ? appName[(index + 1)..] : appName;
                    var routeName = NamingUtil.CamelCaseToKebabCase(route);

                    if (!string.IsNullOrWhiteSpace(routeName))
                    {
                        var openApiPaths = new OpenApiPaths();
                        foreach (var path in document.Paths)
                        {
                            openApiPaths.Add(DelimitersConstant.SLASH + routeName + path.Key, path.Value);
                        }

                        document.Paths = openApiPaths;
                    }

                    document.Servers = null;
                    return Task.CompletedTask;
                });

                options.AddOperationTransformer((operation, context, cancellationToken) =>
                {
                    operation.Security ??= [];
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        //[new OpenApiSecuritySchemeReference(AuthConstant.HEADER)] = []
                    });

                    var swaggerOperationAttribute = context.Description.ActionDescriptor.EndpointMetadata
                        .FirstOrDefault(metadata => metadata.GetType().Name == "SwaggerOperationAttribute");

                    if (swaggerOperationAttribute != null)
                    {
                        var summary = swaggerOperationAttribute.GetType().GetProperty("Summary")?.GetValue(swaggerOperationAttribute) as string;
                        if (!string.IsNullOrWhiteSpace(summary))
                        {
                            operation.Summary = summary;
                        }

                        var description = swaggerOperationAttribute.GetType().GetProperty("Description")?.GetValue(swaggerOperationAttribute) as string;
                        if (!string.IsNullOrWhiteSpace(description))
                        {
                            operation.Description = description;
                        }
                    }

                    return Task.CompletedTask;
                });
            });
        }

        private static string GetDefaultRouteName()
        {
            var appName = App.AppName;
            var index = appName.IndexOf(DelimitersConstant.DOT);
            var route = index >= 0 ? appName[(index + 1)..] : appName;
            return NamingUtil.CamelCaseToKebabCase(route);
        }
    }
}