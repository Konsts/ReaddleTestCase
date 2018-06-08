package main

import (
	"encoding/json"
	"fmt"
	"log"
	"os"
	"time"
)

type Fib struct {
	FibonachiValue int `json:"F(n)"`
	FibonachiN     int `json:"n"`
}

func main() {
	input := make(chan int, 1)
	go getInput(input)
	var userInput int
	var fibNum int
	var errorAnswer int
	var correctAnswer int
	for {
		fmt.Println("input Fibonacci")
		select {
		case userInput = <-input:
			if fibonachi(fibNum) == userInput {
				correctAnswer++
				errorAnswer = 0
				fibNum++
				fmt.Println("Right!")
			} else {
				errorAnswer++
				correctAnswer = 0
				fmt.Println("Error! correct answer:")
				output := Fib{fibonachi(fibNum), fibNum}
				data, err := json.Marshal(output)
				if err != nil {
					log.Fatal(err)
				}
				fmt.Printf("%s\n", data)
				fibNum++
			}
		case <-time.After(10000 * time.Millisecond):
			fmt.Println("timed out")
			correctAnswer = 0
			errorAnswer++
		}
		if errorAnswer > 3 {
			fmt.Println("You have more then 3 error")
			os.Exit(0)
		}
		if correctAnswer >= 10 {
			fmt.Println("Excellent! you have 10 correct answers!")
			os.Exit(0)
		}
	}
}

func getInput(input chan int) {
	for {
		var result int
		_, err := fmt.Scan(&result)
		if err != nil {
			log.Fatal(err)
		}

		input <- result
	}
}

func fibonachi(n int) int {

	if n == 0 {
		return 0
	}
	if n == 1 {
		return 1
	}
	return fibonachi(n-1) + fibonachi(n-2)
}
