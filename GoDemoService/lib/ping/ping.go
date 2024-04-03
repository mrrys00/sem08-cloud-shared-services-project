package ping

import (
	"context"
	"fmt"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"log"
	"net/http"
	"time"

	"go.opentelemetry.io/otel/propagation"
	"go.opentelemetry.io/otel/trace"

	libhttp "github.com/mrrys00/sem08-cloud-shared-services-project/lib/http"
)

// Ping sends a Ping request to the given hostPort, ensuring a new span is created
// for the downstream call, and associating the span to the parent span, if available
// in the provided context.
func Ping(
	ctx context.Context,
	tracer trace.Tracer,
	hostPort string,
	hostEndp string) (string, error) {

	ctx, span := tracer.Start(ctx, config.SpanOutboundHostEndp)
	defer span.End()

	url := fmt.Sprintf("http://%s/%s", hostPort, hostEndp)
	req, err := http.NewRequestWithContext(ctx, http.MethodGet, url, nil)
	if err != nil {
		return "", fmt.Errorf("failed GET request to %s: %w", url, err)
	}

	propagator := propagation.TraceContext{}

	propagator.Inject(ctx, propagation.HeaderCarrier(req.Header))

	respBody, err := libhttp.Do(req)
	if err != nil {
		return "", fmt.Errorf("failed http request: %w", err)
	}

	return respBody, nil
}

func PingGoRoutine(
	ctx context.Context,
	tracer trace.Tracer,
	timeSleepSec int) {
	for {
		resp, err := Ping(ctx, tracer, config.OutboundHostPort, config.OutboundHostEndp)
		if err != nil {
			log.Fatalf("%s\n", err)
		} else {
			log.Printf("%s\n", resp)
		}
		time.Sleep(time.Duration(timeSleepSec) * time.Second)
		log.Printf("Sleep for %d milliseconds\n", timeSleepSec)
	}
}
