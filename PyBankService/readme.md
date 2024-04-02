# Simple Py Demo Service

## How to run?

```shell
docker compose up
```

or more brutal solution:

```shell
docker compose up --force-recreate --remove-orphans --detach
```

stop:

```shell
docker compose down
```

get rid of odl image:
```shell
docker rmi $(docker images 'godemoservice-godemoserv' -a -q)
```

## How to test?

1. After running the docker compose 
2. Go to Locust Web UI at [localhost:8089](http://localhost:8089/). 
3. Input
    * Users - peak number of concurrent Locust users.
    * Spawn-rate - rate to spawn users at (users per second).
    * Mocking target (Py Service at [http://localhost:8080](http://localhost:8080))
4. Press `Start`. Locust will now mock service and agregate statistics

## Documentation

* DEFAULT_URL= [http://localhost:8080](http://localhost:8080)

* [Locust configuration](https://docs.locust.io/en/stable/configuration.html)

### Endpoints

1. `/transaction`
    * request:
      * params: optional and ignored
    * response:
        ```json
        { "execution" : true } // if transaction was succesful
        ```
    * curl:
        ```ps1
        curl --location 'http://pybankserv:8080/transaction'
        ```
2. `/balance`
    * request:
      * params: optional and ignored
    * response:
        ```json
        { "balance" : 123.45 } // balance value
        ```
    * curl:
        ```ps1
        curl --location 'http://pybankserv:8080/balance'
        ```
