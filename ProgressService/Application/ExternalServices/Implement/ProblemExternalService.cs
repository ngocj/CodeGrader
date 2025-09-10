using Application.Dtos.ExternalDtos;
using Application.ExternalServices.Interface;
using Common.ResultPattern;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.ServiceExternal.Implementation
{
    public class ProblemExternalService : IProblemExternalService
    {
        protected IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProblemExternalService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result> ValidateProblem(string problemId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var baseUrl = _configuration["ExternalService:BaseUrl"];
                var url = _configuration["ExternalService:ProblemService:GetById"];
                var requestUrl = $"{baseUrl}/{url}/{problemId}";
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseResult = await response.Content.ReadFromJsonAsync<Result<ProblemDto>>();

                    if (responseResult == null)
                    {
                        return Result<bool>.Failure("Invalid response from problem service", null);
                    }

                    if (responseResult.IsSuccess)
                    {
                        return Result.Success(string.Empty);
                    }
                    else
                    {
                        return Result<bool>.Failure(responseResult.Message, null);
                    }
                }
                else
                {
                    return Result.Failure($"Cannot connect to problem service: {response.StatusCode}", null);

                }
            }
            catch
            {
                return Result.Failure("Error validating user", null);
            }
        }

        public async Task<Result<ProblemDto>> GetProblemById(int problemId)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ExternalService:BaseUrl"];
            var requestUrl = $"{baseUrl}/problem/{problemId}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                var problemResult = await response.Content.ReadFromJsonAsync<Result<ProblemDto>>(
                        new JsonSerializerOptions()
                        {
                            NumberHandling = JsonNumberHandling.WriteAsString,
                            PropertyNameCaseInsensitive = true
                        }
                    );

                return problemResult;
            }
            catch (Exception ex)
            {
                return Result<ProblemDto>.Failure(ex.Message, null);
            }
        }
    }
}