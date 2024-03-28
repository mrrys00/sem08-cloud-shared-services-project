# Use Ubuntu as base image
FROM ubuntu:latest

# Set environment variables
ENV DEBIAN_FRONTEND=noninteractive

# Update package list and install dependencies
RUN apt-get update && apt-get install -y \
    curl \
    wget \
    apt-transport-https \
    lsb-release \
    ca-certificates \
    python3 \
    python3-pip \
    && rm -rf /var/lib/apt/lists/*

# Install any needed packages specified in py_deps.txt
RUN pip3 install --no-cache-dir -r requirements.txt.txt

# Copy PyBank
COPY PyBankClient/ /services/py_bank/

# Run PyBankClient
CMD ["python3", "PyBankClient.py", "port"]
# specify port ! ! !
