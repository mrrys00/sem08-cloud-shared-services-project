package main

import (
	"github.com/gin-gonic/gin"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/apis"
	"github.com/mrrys00/sem08-cloud-shared-services-project/pkg/config"
	"log"
)

func main() {
	// Create a new Gin router
	r := gin.Default()

	// Routes
	r.GET(config.RouteSecurityAlert, apis.HandleSecurityAlert)

	// Run server
	if err := r.Run(config.DefaultPort); err != nil {
		log.Fatalf("failed to run server: %v", err)
	} else {
		log.Printf("Server %s started properly", config.ServiceName)
	}
}
