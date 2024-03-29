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

# Copy PyBank
COPY ./ /services/py_bank/

# Install any needed packages specified in py_deps.txt
RUN pip3 install -r /services/py_bank/requirements.txt

# Expose port 8080
EXPOSE 8080

# Run PyBankClient on port 8080
CMD python3 -m flask --app /services/py_bank/app.py run -p 8080
