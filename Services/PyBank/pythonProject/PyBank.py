from uvicorn import run as uvicorn_run
from sys import exit, argv
from random import randint, randbytes
from asyncio import sleep, ensure_future
from fastapi import FastAPI
from requests import post
from asyncio import create_task, run as asyncio_run, CancelledError
from uuid import uuid4

CYAN = '\033[96m'
GREEN = '\033[92m'
YELLOW = '\033[93m'
RED = '\033[91m'
ENDC = '\033[0m'
MAGENTA = '\033[95m'

TRANSACTION = '/transaction'
BALANCE = '/balance'

MIN_SLEEP = 1
MAX_SLEEP = 5

app = FastAPI()


def random_bool():
    return bool(randbytes(1))


def print_colored(text, color):
    print(f'{color}{text}{ENDC}')


def random_double():
    randint(-10_000_000, 10_00_000) / 100


async def simulate_delay():
    delay = randint(MIN_SLEEP, MAX_SLEEP)
    await sleep(delay)


@app.post(TRANSACTION)
async def process_transaction(_):
    print_colored('Received new transaction!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)
    return {"executed": random_bool()}


@app.get(BALANCE)
async def get_balance():
    print_colored('Received new balance request!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)
    return {"balance": random_double()}


async def send_post_request(post_url, data):
    await simulate_delay()
    post(post_url, json=data)


def print_invalid_arguments():
    print_colored('''Invalid arguments!
Expected port and at least one url: <port> <url 1> <url 2>
Url must be valid and absolute eg: <scheme>://<authority>''', RED)


async def run_client():
    print_colored('Starting client!', CYAN)
    while True:
        for request_url in urls:
            await simulate_delay()
            endpoint, json = get_request(request_url)
            ensure_future(send_post_request(endpoint, json))
            print_colored(f'Sending request to {endpoint}', GREEN)


async def start_client():
    client_task = create_task(run_client())
    input('Press any key to stop the service . . .')
    print_colored('Stopping task . . .', YELLOW)

    try:
        client_task.cancel()
    except CancelledError:
        print_colored('Client stopped!', CYAN)


def get_request(request_url):
    if random_bool():
        endpoint = request_url + TRANSACTION
        json = {
            'fromAccount': uuid4(),
            'toAccount': uuid4(),
            'value': random_double()
        }
    else:
        endpoint = request_url + BALANCE
        json = {'account': uuid4()}

    return endpoint, json


if __name__ == "__main__":
    if len(argv) < 3 or not argv[1].isdigit():
        print_invalid_arguments()
        exit()

    port = int(argv[1])
    urls = argv[2:]

    for url in urls:
        if not url.startswith('http://') and not url.startswith('https://'):
            print_invalid_arguments()
            exit()

    asyncio_run(start_client())
    print_colored(f'Starting FastAPI!', CYAN)
    uvicorn_run(app, host="localhost", port=port)
