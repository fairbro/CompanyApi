using System.ComponentModel.DataAnnotations;

namespace CompanyApi.Dtos
{
    public record CompanyDto
    {
        /// <summary>
        /// Unique Company identifier
        /// </summary>
        public required int Id { get; set; }
        /// <summary>
        /// Company name
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Company description
        /// </summary>
        public required string Description { get; set; }
    }
}
