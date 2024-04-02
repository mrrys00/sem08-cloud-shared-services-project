FROM python:3.12-slim

ARG app=/app
WORKDIR ${app}

COPY . ${app}

RUN pip install fastapi uvicorn
# CMD ["sleep", "2137"]
CMD ["python3", "service/PyBankService.py", "8080"]
