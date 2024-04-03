from random import randint, randbytes
from asyncio import sleep

ENDC = '\033[0m'
MIN_SLEEP = 1
MAX_SLEEP = 5


def random_bool():
    return bool(randbytes(1))


def print_colored(text, color):
    print(f'{color}{text}{ENDC}')


def random_double():
    return randint(-10_000_000, 10_00_000) / 100


async def simulate_delay():
    delay = randint(MIN_SLEEP, MAX_SLEEP)
    await sleep(delay)