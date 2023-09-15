# Hacker News API - ASP.NET Core Application

This ASP.NET Core application retrieves and displays details of the top "best stories" from the Hacker News API. It efficiently services large numbers of requests without risking overloading the Hacker News API by implementing rate limiting.

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 or later)
- [Git](https://git-scm.com/downloads) (optional)

## Getting Started

Follow these steps to get the application up and running:

1. Clone the repository (if you haven't already):

   ```bash
   git clone https://github.com/your-username/hacker-news-api-app.git
   cd hacker-news-api-app
2. Run the application:

   ```bash
   dotnet run
   ```
   The application will start and be accessible at http://localhost:5130/index.html (HTTP) by default.

## Usage

Once the application is running, you can access the following endpoints:

- `http://localhost:5130/swagger` (Swagger UI for API documentation)
- `http://localhost:5130/api/stories/best/{number}` (Retrieve the top "best stories" where `{number}` is the number of stories to fetch)

Example API request:

```http
GET http://localhost:5130/api/stories/best/5
```

## Rate Limiting
The application implements rate limiting to ensure efficient service without overloading the Hacker News API. By default, it allows 100 requests per minute to all endpoints.

## Contributing
Feel free to contribute to this project by opening issues or pull requests.



