# PyBankServices and PyBankClients

## How to run PyBankService?

### Service Mk1

```shell
python3 service/PyBankService.py 8080
```
Will start the bank service on port `8080`
at [http//:localhost:8080](http//:localhost:8080)

### Service Mk2

```shell
python3 -m flask --app /services/py_bank/app.py run -p 8080
```
Will start the `Flask` bank service on port `8080`
at [http//:localhost:8080](http//:localhost:8080)

## How to run PyBankClient?

### Client Mk1
```shell
python3 /services/py_bank/PyBankClient.py <url1> <url2>
```
Will start client that will send requests with random delay to urls

### Client Mk2 - Locust mocking client

```shell
python3 -m locust -P 8089
```
Will start locust mocking client as described in file `locustfile.py`
as a web interface avaliable
at [http//:localhost:8089](http//:localhost:8089)


## Documentation

[Locust configuration options](https://docs.locust.io/en/stable/configuration.html)

**Endpoints:**

1. `/transaction`
    * request:
      * params: optional and ignored
    * response:
      * json: { execution : `<bool>` }
      > // if transaction was succesful
2. `/balance`
    * request:
      * params: optional and ignored
    * response:
      * json: { balance : `<double>` }
      > // balance value

