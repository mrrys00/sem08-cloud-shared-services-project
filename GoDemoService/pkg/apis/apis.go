package apis

import (
	"context"
	"fmt"
	"github.com/gin-gonic/gin"
	"go.opentelemetry.io/otel/propagation"
	//"go.opentelemetry.io/otel"
	"github.com/mrrys00/sem08-cloud-shared-services-project/lib/tracing"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"net/http"
)

func HandleSecurityAlert(c *gin.Context) {
	ctx := context.Background()
	tracer := tracing.Init(ctx, config.ServiceName)

	//outboundHostPort, err := os.LookupEnv("OUTBOUND_HOST_PORT")
	//if err {
	//	outboundHostPort = "localhost:8082"
	//}

	//tracer := otel.Tracer("example-tracer")

	_, span := tracer.Start(c.Request.Context(), config.SpanSecurityAlert)
	defer span.End()

	propagator := propagation.TraceContext{}

	propagator.Inject(ctx, propagation.HeaderCarrier(c.Request.Header))
	//if _, err = writer.Write([]byte(fmt.Sprintf("%s -> %s", thisServiceName, response))); err != nil {
	//	log.Fatalf("Error occurred on write: %s", err)
	//}

	name := c.DefaultQuery("name", "World")
	span.AddEvent("Saying hello to " + name)

	c.JSON(http.StatusOK, gin.H{"message": fmt.Sprintf("Hello, %s!", name)})
}
