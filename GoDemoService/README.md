# Simple Go Demo Service

## How to run?

```shell
docker compose up
```

## How to test?

1. After running the docker compose 
2. Go to http://localhost:16686/.
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

DEFAULT_URL= `localhost:8081`

**ENDPOINTS:**

1. _/securityalert_
    * request:
      * params:
        * name (optional): string (eg. Ala)
    * response:
      * json:
        * message (required): string (eg. Hello, Ala!) 

## TO DO list

1. Integration with Mateusz's service
2. More logging
3. Move services configs to the docker compose envs
4. Upgrade documentation
5. Add some random delay for response
6. Move to _Services_ dir before merge
