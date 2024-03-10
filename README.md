# OpenTelemetry - project for Environment of Services Implementation course

## 1. Introduction

This project's purpose is to construct and perform a simple case study, that would allow to get acquainted with OpenTelemetry technology, which is an open source observability framework that provides IT teams with standardized protocols and tools for collecting and routing telemetry data.

**Authors**:

- Mateusz Cyganek
- Szymon Ryś
- Piotr Socała
- Rafał Tekielski


## 2. Theoretical background/technology stack

### OpenTelemetry

OpenTelemetry stands as a cutting-edge Observability framework and toolkit meticulously crafted to orchestrate telemetry data like **traces**, **metrics**, and **logs**. Significantly, OpenTelemetry operates independently of vendors and tools, allowing integration with a diverse range of Observability backends, including both open-source solutions like Jaeger and Prometheus, as well as commercial offerings.

Unlike conventional observability backends such as Jaeger or Prometheus, OpenTelemetry takes a different approach. It is primarily focused on the lifecycle of telemetry data—generation, collection, management, and export. A core objective of OpenTelemetry is to simplify the process of instrumenting applications or systems, regardless of their language, infrastructure, or runtime environment. Importantly, OpenTelemetry leaves the storage and visualization of telemetry data to other specialized tools, promoting flexibility and adaptability in observability management.

#### Traces

Traces give us the big picture of what happens when a request is made to an application. Whether your application is a monolith with a single database or a sophisticated mesh of services, traces are essential to understanding the full “path” a request takes in your application.

They are represented by **spans**, which are the units of work or operation. In OpenTelemetry, they include the following information:

- Name
- Parent span ID (empty for root spans)
- Start and End Timestamps
- Span Context
- Attributes
- Span Events
- Span Links
- Span Status

#### Metrics

A metric is a measurement of a service captured at runtime. The moment of capturing a measurements is known as a metric event, which consists not only of the measurement itself, but also the time at which it was captured and associated metadata.

#### Logs

A log is a timestamped text record, either structured (recommended) or unstructured, with metadata. Of all telemetry signals, logs have the biggest legacy. Most programming languages have built-in logging capabilities or well-known, widely used logging libraries. Although logs are an independent data source, they may also be attached to spans. In OpenTelemetry, any data that is not part of a distributed trace or a metric is a log. For example, events are a specific type of log. Logs often contain detailed debugging/diagnostic info, such as inputs to an operation, the result of the operation, and any supporting metadata for that operation.
