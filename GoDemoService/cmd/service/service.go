package main

import (
	"context"
	"github.com/gin-gonic/gin"
	"github.com/mrrys00/sem08-cloud-shared-services-project/lib/tracing"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/apis"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"log"
)

func main() {
	// Create a new Gin router and Jaeger tracer
	r := gin.Default()
	ctx := context.Background()
	tracer := tracing.Init(ctx, config.ServiceName)

	// Routes
	r.GET(config.RouteHello, apis.HelloMiddleware(ctx, tracer))
	r.GET(config.RouteAlert, apis.AlertMiddleware(ctx, tracer))

	// Run server
	if err := r.Run(config.DefaultPort); err != nil {
		log.Fatalf("failed to run server: %v", err)
	} else {
		log.Printf("Server %s started properly", config.ServiceName)
	}

	// ping goroutine for Mateusz's service
	//go ping.PingGoRoutine(ctx, tracer, 5)
}
