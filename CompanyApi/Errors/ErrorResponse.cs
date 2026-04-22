using System.Text.Json.Serialization;

namespace CompanyApi.Errors
{
    public record ErrorResponse
    {
        /// <summary>
        /// A short error code
        /// </summary>
        [JsonPropertyName("error")]
        public required string Error { get; set; }
        /// <summary>
        /// Error description
        /// </summary>
        [JsonPropertyName("error_description")]
        public required string ErrorDescription { get; set; }
    }
}
