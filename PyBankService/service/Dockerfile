FROM python:3.12-slim

WORKDIR /app/PyBankService

COPY PyBankService/service/ .

RUN pip install -r requirements.txt
RUN opentelemetry-bootstrap -a install

CMD ["opentelemetry-instrument", "--traces_exporter", "console", "--metrics_exporter", "console", "--logs_exporter", "console", "--service_name", "pybankserv", "flask", "run", "-p", "8080", "--host", "0.0.0.0"]
