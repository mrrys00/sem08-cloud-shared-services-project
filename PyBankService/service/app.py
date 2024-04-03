from datetime import datetime
from logging import basicConfig, getLogger, INFO
from flask import Flask
from opentelemetry import trace, metrics
from Helper import print_colored, simulate_delay, random_bool, random_double

CYAN, RED, MAGENTA = '\033[96m', '\033[91m', '\033[95m'
TRANSACTION = '/transaction'
BALANCE = '/balance'


app = Flask(__name__)

basicConfig(level=INFO)
logger = getLogger(__name__)

tracer = trace.get_tracer('pybankserv.tracer')
meter = metrics.get_meter('pybankserv.meter')
# meter2 = metrics.get_meter('pybankserv.meter')

pybankserv_counter = meter.create_counter(
    'pybankserv.pybankserv_counter',
    description='Number of failed transactions',
)
# balances_with_debt_counter = meter2.create_counter(
#     'pybankserv.debt_balance',
#     description='Number of balance requests from accounts with debt',
# )


@app.route(TRANSACTION)
async def get_transaction():
    print_colored('Received new transaction!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)

    executed = random_bool()
    result = {'executed': executed}

    log_request(result)
    trace_request(TRANSACTION, not executed)

    return result


@app.route(BALANCE)
async def get_balance():
    print_colored('Received new balance request!', MAGENTA)
    await simulate_delay()
    print_colored('Sending request response!', MAGENTA)

    balance = random_double()
    result = {'balance': balance}

    log_request(result)
    trace_request(BALANCE, balance < 0)

    return result


def log_request(result):
    logger.log(msg={
        'time': datetime.now(),
        'result': result
    }, level=INFO)


def trace_request(req_type, increm):
    with tracer.start_as_current_span('pybankserv') as tr:
        tr.set_attribute('pybankserv.type', req_type)
        tr.set_attribute('pybankserv.date', str(datetime.now()))

        if req_type is TRANSACTION and increm:
            pybankserv_counter.add(
                1, {'pybankserv.pybankserv_counter', str(False)})
        if req_type is BALANCE and increm:
            pybankserv_counter.add(
                1, {'pybankserv.pybankserv_counter', str(True)})
