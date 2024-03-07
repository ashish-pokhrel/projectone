using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace oneapp.Utilities
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.ParameterDescriptions.Any())
            {
                if (context.ApiDescription.ParameterDescriptions.Any(x => x.ModelMetadata.ModelType != null))
                {


                    var modelsWithFile = context.ApiDescription.ParameterDescriptions
                                  .Where(x => x.ModelMetadata.ModelType.GetProperties()
                                      .Any(p => p.PropertyType == typeof(List<IFormFile>)));

                    if (modelsWithFile.Any())
                    {
                        var modelsWithFileParameters = modelsWithFile
                                    .Select(x => x.ModelMetadata.ModelType)
                                    .Distinct()
                                    .ToList();
                        var schemaProperties = new Dictionary<string, OpenApiSchema>();

                        foreach (var modelType in modelsWithFileParameters)
                        {
                            var fileProperties = modelType.GetProperties().Where(p => p.PropertyType == typeof(List<IFormFile>));

                            foreach (var fileProperty in fileProperties)
                            {
                                schemaProperties.Add(fileProperty.Name, new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Type = "string", Format = "binary" } });
                            }
                        }

                        if (operation.RequestBody == null)
                        {
                            operation.RequestBody = new OpenApiRequestBody();
                        }

                        operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType>
                        {
                            { "multipart/form-data", new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Properties = schemaProperties
                                    }
                                }
                            }
                        };
                    }
                }
            }
        }
    }
}

