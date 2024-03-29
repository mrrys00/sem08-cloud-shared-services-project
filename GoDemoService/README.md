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
4. Send cURL (eg. using Postman or terminal): 
    ```
   curl --location 'http://localhost:8081/securityalert?name=Ala'
   ```
5. Should see the response:
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
2. _/alert_
    * request:
        * params:
            * message (optional): string (eg. Ala)
    * response:
        * json:
            * message (required): string (eg. Ala)

## TO DO list

1. Integration with Mateusz's service with routine
2. More logging
3. [DONE] Move services configs to the docker compose envs
4. [DONE] Upgrade documentation
5. [DONE] Add some random delay for response
6. Move to _Services_ dir before merge
7. Add Grafana integration
