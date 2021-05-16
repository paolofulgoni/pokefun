![Poké Ball](https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Pok%C3%A9_Ball.svg/241px-Pok%C3%A9_Ball.svg.png)

_A Pokemon Web API, just for fun_

# PokeFun

[![build](https://github.com/paolofulgoni/pokefun/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/paolofulgoni/pokefun/actions/workflows/build.yml?query=branch%3Amain)
[![docker](https://github.com/paolofulgoni/pokefun/actions/workflows/docker.yml/badge.svg)](https://github.com/paolofulgoni/pokefun/actions/workflows/docker.yml)
[![codecov](https://codecov.io/gh/paolofulgoni/pokefun/branch/main/graph/badge.svg?token=AWD9PXV1EX)](https://codecov.io/gh/paolofulgoni/pokefun)

This REST API provides two endpoints to get information about Pokemons:

`GET ​/pokemon​/<pokemon name>`
_Get some basic information about a Pokemon_

```
> http localhost:5000/pokemon/mewtwo
```
```json
{
    "description": "It was created by a scientist after years of horrific gene-splicing and DNA-engineering experiments.",
    "habitat": "rare",
    "isLegendary": true,
    "name": "mewtwo"
}
```

`GET ​/pokemon​/translated​/<pokemon name>`
_Get some basic information about a Pokemon, with "fun translation" of its description: Yoda translation if the Pokemon's habitat is cave or it's legendary; Shakespeare translation for all other Pokemons_

```
> http localhost:5000/pokemon/translated/mewtwo
```
```json
{
    "description": "Created by a scientist after years of horrific gene-splicing and dna-engineering experiments, it was.",
    "habitat": "rare",
    "isLegendary": true,
    "name": "mewtwo"
}
```

## 🎠 Run

### ... using Docker

The easiest way to run the service is through Docker.

If you don't have Docker installed on your system, take a look [here](https://docs.docker.com/get-docker/) first.

This command will run a container named `pokefun`, listening on port 5000 (choose another port number if that one is already in use):

```
> docker run -d -p 5000:80 --name pokefun paolofulgoni/pokefun
```

Given that the Docker image is published to Docker Hub (repository [paolofulgoni/pokefun](https://hub.docker.com/repository/docker/paolofulgoni/pokefun)), you don't have to build the image from source.

You can now call the endpoints with your tool of choice, e.g. [HTTPie](https://httpie.io/):

```ah
> http localhost:5000/pokemon/ditto
```

Do you want to see the beautiful Swagger UI and test the API from there? Then run the service in `Development` mode and open your browser to http://localhost:5000/swagger

```
> docker run -d -p 5000:80 --name pokefun -e ASPNETCORE_ENVIRONMENT=Development paolofulgoni/pokefun
```

To stop the service, run this command:

```
> docker stop pokefun
```

When you're done, remove the container:

```
> docker rm pokefun
```

### ... using .NET 5 SDK

You can easily run the service from your computer, but you'll have to compile it first. Therefore, you need to:

* Install the [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
* Clone the repository locally

Then use the dotnet CLI to run the service. Make sure you're on the root folder of the project, then type:

```
> dotnet run --project ./src/PokeFun
```

This will use the `Development` Hosting environment, therefore you can open a browser to http://localhost:5000/swagger and have fun with the Swagger UI.

Press `CTRL+C` when you're done.

## 🔧 Build and test

Make sure [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) is installed on your dev environment. Then just open the project with your IDE of choice.

If you want to build the project using the .NET CLI, run the following command from the project's root folder:

```
> dotnet build
```

The project contains some unit and integration tests. You can run them with the following command:

```
> dotnet test
```

*IMPORTANT*: integration tests actually call the external services. If you want, you can easily exclude them filtering out the `Integration` category:

```
> dotnet test --filter TestCategory!=Integration
```

Integration tests are excluded by the CI pipeline to avoid false test failures due to the external services. For example, FunTranslation have rate limits which can change the result of some calls.

## ☑ Todo

Here below few things I'd like to do before... going to production (just kidding, it'll never happen 😁):

* Add a cache layer, since the information returned by the API is static and third-party services require it (PokeApi states this in its [Fair Use Policy](https://pokeapi.co/docs/v2#fairuse); FunTranslations have [rate limits](https://funtranslations.com/api/))
* Add HTTPS support if required (in most cases it's offloaded to a load balancer, so I disabled it)
* Add retry and circuit breaker policies to external HTTP calls 
* Add an Healthcheck endpoint

## 🙏 Credits

* [PokeApi](https://pokeapi.co/) - A RESTful API for Pokémon - [GitHub repo](https://github.com/PokeAPI/pokeapi/)
* [FunTranslations](https://funtranslations.com/) - Translations for fun
* [Poké Ball image](https://commons.wikimedia.org/wiki/File:Pok%C3%A9_Ball.svg) - [CC BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/deed.en) by [Geni](https://commons.wikimedia.org/wiki/User:Geni)
* Pokémon and Pokémon character names are trademarks of Nintendo
