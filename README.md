1. Create a new ASP.NET Core Web API project using Visual Studio or another IDE of your choice.
      - **Created a .sln file which contains 5 projects(Application, Domain, Infrastructure, WebAPI and Tests) with .NET Core framework using VS 2022 Professional. The project follows(as much as possible) The Clean Architecture Pattern which is the best practice when using .net Core Framework**
2. Set up the necessary database schema using Entity Framework Core. You should have tables for customers, articles, payments, transactions, and a join table for the many-to-many relationship between transactions and articles.
      - **Created entities, with relationships(OnModelCreating). The DBContext is using Microsoft.EntityFrameworkCore .OnConfiguring  method sets the database in memory usage. Find the entities under Domain project, the DB Context is under Infrastructure.**
3. Implement CRUD operations for customers, articles, and payments. Use HTTP GET for retrieving data, POST for creating new data, PUT for updating existing data, and DELETE for removing data.
      - **CRUD operations can be easily tested having Swagger UI.  Make sure WEBAPI project is selected and hit F5. There are 4 controllers for each entity containing endpoints for each operation. The project implements MediatR design pattern to handle requests.
           Also, DI takes the responsability for creating new objects  when needed.
        For each endpoint there is a specific handler that handles the request based on params and manages the logic behind. Each Handler inmplements IRequestHandler<TRequest,TResponse>. See more  details in Application project. Also, AutoMapper extension is used for a better readability and friendly debug.**
4. Implement an endpoint for creating transactions that can include one or more articles and one or more payments. When a new transaction is created, the inventory of each article should be updated and each payment should be processed. Use a many-to-many relationship between transactions and articles to handle cases where a transaction includes multiple articles.
     - **When a transaction is created the quantity of article is decreased, and new payments are added**
5. Implement input validation for all endpoints to ensure that data is correctly formatted and meets any necessary constraints.
      - **Used FluentValidation to validate the requests**
6. Implement error handling for all endpoints to handle cases where an error occurs during processing.
       - **The returned StatusCode  suggests whether the call was a success or not based on the code.**
7. Write unit tests for all endpoints using the NUnit testing framework. The tests should cover all use cases and should include edge cases and error handling scenarios.
       -  **The Tests projects uses Moq and NUnit for testing purposes.**
8. Create a Dockerfile for the API that specifies the necessary dependencies and configuration.
      - **Find the Docker file in tree structure.**
9. Build a Docker image of the APl using the Dockerfile.
      -**https://docs.docker.com/get-started/02_our_app/ helped me do create the docker image.**
10. Run the API in a Docker container.
        ![fancyContainer](https://github.com/vmatuz/SuperMetro/assets/168770879/0ad0ecef-67a7-4b9d-8ae2-b91c902790cb)
