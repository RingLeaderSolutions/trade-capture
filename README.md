# Trade Capture
This is a sample architecture for a distributed workflow built around a service bus (RabbitMQ) and based on microservices. It simulates a simple workflow for a trade which comprises two steps:
1. A new trade is sent to the system and captured by the Validation Service Web API.
2. The is validated in the Validation Service.
3. Depending its id, the trade will be then sent to either the Pomax Pump or GE Swap Pump.
4. The trade is finally completed.

There are three services in charge of executing each step in the workflow:
* ValidationService: self-hosted WebAPI. When a new request is received, it creates a new trade in the system with the provided Id. Also runs the actual ValidationService which consumes messages from the queue.
* PomaxPumpService and GE Swap Service: listens to messages on the service bus and forwards them to either Pomax Pump or GE Swap Pump.

## Project structure
The solution contains the following projects:
* ValidationService: hosting the WebAPI receiving trades and connects the Validation Service into RabbitMQ.
* StateSaga: orchestrates the workflow by receving messages from each service, fowarding them to the one according to the workflow configuration and keeping state of each trade.
* Persistence: data access layer in EntityFramework for keeping workflow state.
* Messaging: interface definitions for events and commands for the message queue.
* Common: shared types and libraries.

## Repository structure
This sample architecture is also configured to run all services in Docker instances. This configuration is done on the 'container' branch.

## Runtime setup
The Trade Capture has got three dependencies: RabbitMQ, SQL Server, and SEQ logging (optional). These can be either be installed locally, or from Docker containers (run locally or on the cloud). Make sure these are set up before running the solution.
If you rather running them from Docker containers there are scripts in the 'tools' folder to do so:
* To run them on Docker in your machine, use the script 'run-docker-containers.cmd' which will execute docker-compose with both RabbitMQ, SQL Server on Linux and SEQ logging. This script will also configure automatically the virtual host 'EDF.TradeCapture' in RabbitMQ.
* To run them in Azure as Container Instances PaaS, run the script 'create-trade-capture-containers.bat' which will deploy RabbitMQ and SEQ logging instances. Make sure that you have created a Resource Group called 'edf'. Unfortunately, this deployment doesn't configure RabbitMQ, so should run the script 'rabbit-bash.bat' to open a session on your RabbitMQ Docker instance and execute the contents of the file 'create-vhost.bat' on it.