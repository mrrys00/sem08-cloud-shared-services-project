package apis

import (
	"context"
	"fmt"
	"github.com/gin-gonic/gin"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/utils"
	"go.opentelemetry.io/otel/propagation"
	//"go.opentelemetry.io/otel"
	"github.com/mrrys00/sem08-cloud-shared-services-project/lib/tracing"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"net/http"
)

func HandleHello(c *gin.Context) {
	ctx := context.Background()
	tracer := tracing.Init(ctx, config.ServiceName)

	//outboundHostPort, err := os.LookupEnv("OUTBOUND_HOST_PORT")
	//if err {
	//	outboundHostPort = "localhost:8082"
	//}

	//tracer := otel.Tracer("example-tracer")

	_, span := tracer.Start(c.Request.Context(), config.SpanHello)
	defer span.End()

	propagator := propagation.TraceContext{}

	propagator.Inject(ctx, propagation.HeaderCarrier(c.Request.Header))
	//if _, err = writer.Write([]byte(fmt.Sprintf("%s -> %s", thisServiceName, response))); err != nil {
	//	log.Fatalf("Error occurred on write: %s", err)
	//}

	utils.RandomSleep()

	name := c.DefaultQuery("name", "World")
	span.AddEvent("Saying hello to " + name)

	c.JSON(http.StatusOK, gin.H{"message": fmt.Sprintf("Hello, %s!", name)})
}

func HandleAlert(c *gin.Context) {
	ctx := context.Background()
	tracer := tracing.Init(ctx, config.ServiceName)

	_, span := tracer.Start(c.Request.Context(), config.SpanAlert)
	defer span.End()

	propagator := propagation.TraceContext{}
	propagator.Inject(ctx, propagation.HeaderCarrier(c.Request.Header))

	utils.RandomSleep()

	alertMessage := c.DefaultQuery("message", "Not specified message")
	span.AddEvent("Alert " + alertMessage)

	c.JSON(http.StatusOK, gin.H{"message": fmt.Sprintf("%s", alertMessage)})
}
