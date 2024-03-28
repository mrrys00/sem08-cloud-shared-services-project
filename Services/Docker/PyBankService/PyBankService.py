from starlette.middleware import Middleware
from starlette.middleware.cors import CORSMiddleware
from uvicorn import run as uvicorn_run
from sys import exit, argv
from fastapi import FastAPI

from Helper import print_colored, simulate_delay, random_bool, random_double

CYAN, RED, MAGENTA = '\033[96m', '\033[91m', '\033[95m'
TRANSACTION = '/transaction'
BALANCE = '/balance'

app = FastAPI(middleware=[
    Middleware(CORSMiddleware, allow_origins=["*"])
])


@app.get(TRANSACTION)
async def get_transaction():
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


if __name__ == "__main__":
    if len(argv) != 2 or not argv[1].isdigit():
        print_colored('''Invalid arguments!
Expected port: <port>''', RED)
        exit()

    port = int(argv[1])
    print_colored(f'Starting FastAPI on port {port}!', CYAN)
    uvicorn_run(app, host="localhost", port=port)
