FROM python:3.12-slim

WORKDIR /app

COPY ./service/ .

RUN pip install fastapi uvicorn

CMD ["uvicorn", "PyBankService:app", "--host", "0.0.0.0", "--port", "8080"]
