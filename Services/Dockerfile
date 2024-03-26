# Use Ubuntu as base image
FROM ubuntu:latest

# Set environment variables
ENV DEBIAN_FRONTEND=noninteractive \
    DOTNET_CLI_TELEMETRY_OPTOUT=true

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

# Install .NET SDK
RUN wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-6.0 \
    && rm packages-microsoft-prod.deb

# Install any needed packages specified in py_deps.txt
RUN pip3 install --no-cache-dir -r requirements.txt.txt


# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !
# ! ! ! ! ! ! FOR NET BANK  ! ! ! ! ! ! !
# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !

# Copy NetBank
COPY services/NetBankFiles/ /services/net_bank/

# Run NetBank
CMD ["dotnet", "/services/net_bank/NetBank.dll", "port", "url1", "url2"]
# specify port and urls ! ! !


# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !
# ! ! ! ! ! FOR PY BANK CLIENT  ! ! ! ! !
# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !

# Copy PyBank
COPY services/PyBankFiles/ /services/py_bank/

# Run PyBankClient
CMD ["python3", "PyBankClient.py", "port"]
# specify port ! ! !


# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !
# ! ! ! ! FOR PY BANK SERVICE ! ! ! ! ! !
# ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! ! !

# Copy PyBank
COPY services/PyBankFiles/ /services/py_bank/

# Run PyBankClient
CMD ["python3", "PyBankService.py", "url1", "url2"]
# specify urls ! ! !
