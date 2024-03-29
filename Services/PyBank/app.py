from flask import Flask
from datetime import datetime
import logging

from Helper import print_colored, simulate_delay, random_bool, random_double

CYAN, RED, MAGENTA = '\033[96m', '\033[91m', '\033[95m'
TRANSACTION = '/transaction'
BALANCE = '/balance'

app = Flask(__name__)
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


@app.route(TRANSACTION)
async def get_transaction():
    print_colored('Received new transaction!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)
    result = {"executed": random_bool()}
    log(result)
    return result


@app.route(BALANCE)
async def get_balance():
    print_colored('Received new balance request!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)
    result = {"balance": random_double()}
    log(result)
    return result


def log(result):
    logger.log(msg={
        "time": datetime.now(),
        "result": result
    }, level=logging.INFO)
