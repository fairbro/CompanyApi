A lightweight .NET 10 Minimal API that exposes company information sourced from XML files stored in a GitHub repository. The API parses XML, maps it to DTOs, and returns clean JSON responses that conform to the provided OpenAPI specification.

🚀 Features
✔ Get company details
Fetches a company by ID from the GitHub XML source and returns:

json
{
  "id": 1,
  "name": "MWNZ",
  "description": "..is awesome"
}
✔ Proper 404 handling
If the company does not exist, the API returns:

json
{
  "error": "Not Found",
  "error_description": "Company with id 3 does not exist"
}
✔ Health endpoint
Simple health check at:

Code
GET /health

## Configuration

The XML source URL is configured via `appsettings.json`:

{
  "CompanyApi": {
    "XmlBaseUrl": "https://raw.githubusercontent.com/MiddlewareNewZealand/evaluation-instructions/main/xml-api"
  }
}

🛠️ Running the API
Prerequisites
.NET 10 SDK

Run the API
Code
dotnet run --project CompanyApi
The API will start on:

Code
https://localhost:5001
http://localhost:5000
🧪 Running Tests
From the solution root:

Code
dotnet test
All integration tests should pass.

📄 API Endpoints
GET /companies/{id}
Returns a company by ID.

✔ 200 OK
json
{
  "id": 1,
  "name": "MWNZ",
  "description": "..is awesome"
}
❌ 404 Not Found
json
{
  "error": "Not Found",
  "error_description": "Company with id 3 does not exist"
}
GET /health
Simple health check.