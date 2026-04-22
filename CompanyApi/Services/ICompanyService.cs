using CompanyApi.Dtos;

namespace CompanyApi.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompanyAsync(int id, CancellationToken cancellationToken);
    }
}
