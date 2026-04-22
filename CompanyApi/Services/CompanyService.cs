using CompanyApi.Dtos;
using CompanyApi.Exceptions;
using CompanyApi.Options;
using System.Xml.Serialization;

namespace CompanyApi.Services
{
    public class CompanyService(IHttpClientFactory httpClientFactory, CompanyApiOptions options, ILogger<CompanyService> logger) : ICompanyService
    {
        public async Task<CompanyDto> GetCompanyAsync(int id, CancellationToken cancellationToken)
        {
            using var scope = logger.BeginScope("Company Request id: {id}", id);
            logger.LogInformation("Fetching company id: {id}", id);

            var url = $"{options.XmlBaseUrl}/{id}.xml";

            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("request");

            HttpResponseMessage response;

            try
            {
                response = await httpClient.GetAsync(url, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Network error fetching company id: {id}", id);
                throw new CompanyNotFoundException(id);
            }

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Fetching company id {id} failed with HttpStatus code: {StatusCode}", id, response.StatusCode);
                throw new CompanyNotFoundException(id);
            }

            var xml = await response.Content.ReadAsStringAsync();
            var fileDescriptor = ParseCompany(xml);

            return new CompanyDto
            {
                Id = fileDescriptor.Id,
                Name = fileDescriptor.Name,
                Description = fileDescriptor.Description
            };
        }

        private CompanyXmlModel ParseCompany(string xml)
        {
            var serializer = new XmlSerializer(typeof(CompanyXmlModel));
            using var reader = new StringReader(xml);
            var fileDescriptor = (CompanyXmlModel)serializer.Deserialize(reader);

            return fileDescriptor;
        }
    }
}