# Dev Notes

## Docker build & run steps

1. Navigate to the solution folder
2. Run this command to get an image: `docker build -t trendyol-case-cenkay .`
3. Then run the container, **make sure that you definetely run this in interactive mode** (otherwise the container will get stuck in an infinite loop, will eat up your CPU in the background) `docker run --name trendyolcase-cenkay -it trendyol-case-cenkay`
4. Now you may interact with the console.
5. Type `exit` into the console to exit the application. 

## Why no cache and/or persistent data?
First, I had thought it would make the project look fancy, but then it has struck to my mind that all the sort of optimizations that I have to implement afterwards.
Therefore I have decided not to implement any cache and database support into the case.

## Is this the best you've got?
Most certainly not. However I just kept refactoring and implementing and honestly it was beneficial for my growth, alas there's a deadline for everything, right?

## In UnitTests there is no usage of Moq?
I was deep into development and came to a point that I had to refactor all my domain methods; exposing them to be more testable. However whatever done is done.

## Also
There might be some inconsistencies in the codebase due to these being develop in different times.
I was on annual leave for the last two weeks. I was on vacation for 5 days, still in my free time I had done insignificant amount of development. 

**Feel free to ask me anything:** vergilicenkay@gmail.com
