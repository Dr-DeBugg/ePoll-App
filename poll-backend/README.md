# ePoll backend

This project provides the backend for ePoll application, made with C# using .NET 5.0 framework.

## Requirements

Requires the following installations:
* [.NET 5.0 SDK](https://dotnet.microsoft.com/download)
* [Docker](https://docs.docker.com/desktop/)

During Docker Desktop (Windows) installation, if you run into `WSL 2 Installation is incomplete` error, one possible solution is WSL update by running the following as administrator:
```
wsl --update
```

## MongoDB with Docker
First, start the Docker Desktop App.
Then we need a MongoDB instance for backend server by running this docker command:

```
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```

If port 27017 is not available on your computer, please replace it with another port in the command above, then update your port in PollBackend/appsettings.json: "MongoSettings" -> "Connection".

## Run backend server

To start the backend server, first go into the project directory from this directory:

```
cd PollBackend
```

Then start the backend server:

```
dotnet run
```

Open [http://localhost:5001](http://localhost:5001) to view it in the browser.

If port 5001 is unavailable, change it before starting the backend in PollBackend/appsettings.json: "Urls".