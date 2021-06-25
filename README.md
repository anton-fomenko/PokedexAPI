# Pokedex API
The app is currently published here: https://pokedex-api-1.herokuapp.com      
Swagger documentation: https://pokedex-api-1.herokuapp.com/swagger/index.html

## How to run locally?
1) Clone the repository.
1) Install Docker: https://docs.docker.com/get-docker/
2) In the console go to PokedexAPI/Pokedex.API folder
3) Build an image with tag pokedex: "docker build -t pokedex ."
4) Start the container: "docker run -p 5000:80 pokedex"
5) Visit the following address in the browser: http://localhost:5000/swagger/index.html

## How to run tests?
1) Download and install Visual Studio: https://visualstudio.microsoft.com/downloads/
2) Open the Pokedex.API.sln in the repository.
3) Click Test -> Test Explorer on the menu bar.
4) In Test Explorer select tests you want to run and start them. 

## Integration tests
Integration tests are slower than the unit tests and situated in the separate project Pokedex.API.IntegrationTests.   
Since Fun Translations API allows only 5 requests per hour, some tests may fail if we reach this limit.   

## What I would do differently for a production API?
* Add load tests and stress tests.
* Add monitoring of different metrics (number of requests, average response time, etc.).
* Setup Elasticsearch and Kibana to log and display the failed requests.
* Swagger documentation is publicly available, however, if in the future we limit the access to our app in production, it is better to hide the documentation and endpoints from the general public and protect everything with authentication.
* Currently, the app is published on the Heroku cloud platform, but for production, I would consider using something more sophisticated, like AWS or Azure.
* I would acquire a paid Fun Translations API subscription for production.
* It is a good practice to remove the information about the server in the response headers, so for prod it is better to remove the "server" header