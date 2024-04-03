package apis

import (
	"context"
	"fmt"
	"github.com/gin-gonic/gin"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/utils"
	"go.opentelemetry.io/otel/propagation"
	"go.opentelemetry.io/otel/trace"
	"net/http"
)

func HelloMiddleware(ctx context.Context, tracer trace.Tracer) gin.HandlerFunc {
	handleHello := func(c *gin.Context) {
		_, span := tracer.Start(c.Request.Context(), config.SpanHello)
		defer span.End()

		propagator := propagation.TraceContext{}

		propagator.Inject(ctx, propagation.HeaderCarrier(c.Request.Header))

		utils.RandomSleep()

		name := c.DefaultQuery("name", "World")
		span.AddEvent("Saying hello to " + name)

		c.JSON(http.StatusOK, gin.H{"message": fmt.Sprintf("Hello, %s!", name)})
	}

	return gin.HandlerFunc(handleHello)
}

func AlertMiddleware(ctx context.Context, tracer trace.Tracer) gin.HandlerFunc {
	handleAlert := func(c *gin.Context) {
		_, span := tracer.Start(c.Request.Context(), config.SpanAlert)
		defer span.End()

		propagator := propagation.TraceContext{}
		propagator.Inject(ctx, propagation.HeaderCarrier(c.Request.Header))

		utils.RandomSleep()

		alertMessage := c.DefaultQuery("message", "Not specified message")
		span.AddEvent("Alert " + alertMessage)

		// TO DO - ping Mateusz's service
		//resp, err := ping.Ping(ctx, tracer, config.OutboundHostPort, config.OutboundHostEndp)
		//if err != nil {
		//	log.Fatalf("%s\n", err)
		//} else {
		//	log.Printf("%s\n", resp)
		//}

		c.JSON(http.StatusOK, gin.H{"message": fmt.Sprintf("%s", alertMessage)})
	}

	return gin.HandlerFunc(handleAlert)
}
