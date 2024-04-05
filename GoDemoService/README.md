# Simple Go Demo Service

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
2. Go to http://localhost:16686/. There will be no traces for _godemoservice_ so it won't be available in the dropdown.
3. Select _godemoservice_ with the Service dropdown.
4. Go to Locust Web UI at [localhost:8099](http://localhost:8099/).
5. Input
    * Number of users - peak number of concurrent Locust users.
    * Ram up - rate to spawn users at (users per second).
    * Mocking target (Go Service at [http://godemoservice:8083](http://godemoservice:8083))
6. Press `Start`. Locust will now mock service and agregate statistics
7. Should see the logs in the Jaeger UI

## Documentation

* DEFAULT_URL= [http://godemoservice:8083](http://godemoservice:8083)

### ENDPOINTS:

1. `/hello`
    * request:
      * params:
        * name (optional): `<string>` (eg. Ala)
    * response:
      * json:
        * message (required): `<string>` (eg. Hello, Ala!)
        * if message not specified: `<string>` (Hello, World!)
        ```json
          { "message": "Hello, Ala!" }
        ```
    * curl:
      ```ps1
      curl --location 'http://godemoservice:8083/alert?message=Ala'
      ```
2. `/alert`
    * request:
      * params:
        * message (optional): `<string>` (eg. Ala)
    * response:
      * json:
        * message (required): `<string>` (eg. Ala)
        * if message not specified: `<string>` ("Not specified message")
        ```json
          { "message": "Ala" }
        ```
    * curl:
      ```ps1
      curl --location 'http://godemoservice:8083/hello?name=Ala'
      ```

## TO DO list

1. Add Grafana integration
