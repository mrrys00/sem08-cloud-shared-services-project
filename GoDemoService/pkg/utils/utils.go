package utils

import (
	"log"
	"math/rand"
	"time"
)

func RandomSleep() {
	n := rand.Intn(1000)
	time.Sleep(time.Duration(n) * time.Millisecond)
	log.Printf("Sleep for %d milliseconds\n", n)
}
