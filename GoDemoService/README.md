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
4. Send cURLs (eg. using Postman or terminal): 
    ```
   curl --location 'http://localhost:8083/alert?message=Ala'
   ```
    ```
   curl --location 'http://localhost:8083/hello?name=Ala'
   ```
5. Should see the response:
    ```
    {
    "message": "Ala"
    }
   ```
   ```
    {
    "message": "Hello, Ala!"
    }
   ```
6. Should see the logs in the Jaeger UI

## Documentation


DEFAULT_URL= `localhost:8083`

**ENDPOINTS:**

1. _/hello_
    * request:
      * params:
        * name (optional): string (eg. Ala)
    * response:
      * json:
        * message (required): string (eg. Hello, Ala!)
        * if message not specified: string (Hello, World!)
2. _/alert_
    * request:
      * params:
        * message (optional): string (eg. Ala)
    * response:
      * json:
        * message (required): string (eg. Ala)
        * if message not specified: string ("Not specified message")

## TO DO list

1. Add Grafana integration
