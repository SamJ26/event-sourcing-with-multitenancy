# Event Sourcing demo app

## Setup

1. Start postgres container using command bellow
    ```shell
    docker run -it -p 127.0.0.1:5432:5432 --name postgres -e POSTGRES_PASSWORD=<<DB-PASSWORD>> postgres
    ```

2. Configure user secrets

3. Run the application
    ```shell
    dotnet run
    ```