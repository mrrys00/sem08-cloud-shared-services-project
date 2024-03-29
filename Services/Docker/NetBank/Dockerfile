# Use Ubuntu as base image
FROM ubuntu:latest

# Add APT pinning configuration
RUN echo 'Package: dotnet-* aspnetcore-* netstandard-*' >> /etc/apt/preferences.d/dotnet \
    && echo 'Pin: origin "packages.microsoft.com"' >> /etc/apt/preferences.d/dotnet \
    && echo 'Pin-Priority: 1001' >> /etc/apt/preferences.d/dotnet

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
    gnupg2 \
    && rm -rf /var/lib/apt/lists/*

# Install .NET SDK
RUN wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-7.0 \
    && rm packages-microsoft-prod.deb

# Install Kafka
RUN apt-get update && apt-get install -y openjdk-11-jre-headless
RUN wget -q https://downloads.apache.org/kafka/3.7.0/kafka-3.7.0-src.tgz -O /tmp/kafka.tgz \
    && tar -xzf /tmp/kafka.tgz -C /opt \
    && rm /tmp/kafka.tgz

# Copy NetBank
COPY ./ /services/net_bank/

# Expose ports
EXPOSE 8080 9092

# Run Kafka and NetBank
CMD /opt/kafka_2.13-3.0.0/bin/kafka-server-start.sh /opt/kafka_2.13-3.0.0/config/server.properties & \
    dotnet /services/net_bank/NetBank.dll 8080 9092
