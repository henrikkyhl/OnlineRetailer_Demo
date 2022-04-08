The example is based on my partial solution to the Microservice mini project, where I have added an Ocelot API Gateway to the solution. I have tested that the example can run using Docker Compose.

The docker-compose file has been modified so that the services listens on the following HTTPS ports for requests outside the docker host:

ApiGateway: 7430
ProductApi: 7431
OrderApi: 7432

Routes are defined in the ocelot-routing.json and ocelot-aggregation.json files inside the ApiGateway project.

There is a Postman collection in the Postman subfolder that you can use to test the solution.

Beware that the solution uses CloudAMQP. You should modify the  "cloudAMQPConnectionString" in the Startup classes of ProductApi and OrderApi to match your own RabbitMQ server, if you want to run the example.
