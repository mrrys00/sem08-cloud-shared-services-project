from sys import exit, argv
from asyncio import ensure_future
from requests import post
from asyncio import run as asyncio_run
from uuid import uuid4

from Helper import print_colored, simulate_delay, random_bool, random_double

CYAN, GREEN, RED = '\033[96m', '\033[92m', '\033[91m'
TRANSACTION = '/transaction'
BALANCE = '/balance'


async def start_client():
    print_colored('Starting Client!', CYAN)
    while True:
        for request_url in urls:
            await simulate_delay()
            endpoint, json = get_request(request_url)
            ensure_future(send_post_request(endpoint, json))
            print_colored(f'Sending request to {endpoint}', GREEN)


def get_request(request_url):
    if random_bool():
        endpoint = request_url + TRANSACTION
        json = {
            'fromAccount': str(uuid4()),
            'toAccount': str(uuid4()),
            'value': random_double()
        }
    else:
        endpoint = request_url + BALANCE
        json = {'account': str(uuid4())}

    return endpoint, json


async def send_post_request(post_url, data):
    await simulate_delay()
    try:
        post(post_url, json=data)
    except Exception as ex:
        msg = ex.message if hasattr(ex, 'message') else ex
        print_colored(f'Send error: {msg}', RED)


if __name__ == "__main__":
    if len(argv) < 2:
        print_colored('Invalid arguments! Expected '
                      'at least one url: <url 1> <url 2>', RED)
        exit()

    urls = argv[1:]

    for url in urls:
        if not url.startswith('http://') and not url.startswith('https://'):
            print_colored('Invalid arguments! Url must be valid '
                          'and absolute eg: <scheme>://<authority>', RED)
            exit()

    asyncio_run(start_client())
