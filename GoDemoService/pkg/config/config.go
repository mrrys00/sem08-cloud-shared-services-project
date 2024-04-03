package config

import "os"

const (
	constServiceName      = "SERVICE_NAME"
	serviceNameDefault    = "godemoservice"
	constPort             = "SERVICE_PORT"
	serviceDefaultPort    = `8081`
	constOutboundHostPort = "OUTBOUND_HOST_PORT"
	outboundHostPort      = "localhost:8080"
	constOutboundHostEndp = "OUTBOUND_GET_ENDP"
	outboundHostEndp      = "balance"

	RouteHello           = "hello"
	SpanHello            = RouteHello
	RouteAlert           = "alert"
	SpanAlert            = RouteAlert
	SpanOutboundHostEndp = outboundHostEndp
)

var (
	ServiceName      = setServiceName()
	DefaultPort      = setServicePort()
	OutboundHostPort = setOutboundHostPort()
	OutboundHostEndp = setOutboundHostEndp()
)

func setServiceName() string {
	val, present := os.LookupEnv(constServiceName)
	if !present {
		val = serviceNameDefault
	}

	return val
}

func setServicePort() string {
	val, present := os.LookupEnv(constPort)
	if !present {
		val = serviceDefaultPort
	}
	val = ":" + val
	return val
}

func setOutboundHostPort() string {
	val, present := os.LookupEnv(constOutboundHostPort)
	if !present {
		val = outboundHostPort
	}

	return val
}

func setOutboundHostEndp() string {
	val, present := os.LookupEnv(constOutboundHostEndp)
	if !present {
		val = outboundHostEndp
	}
	val = "/" + val
	return val
}
