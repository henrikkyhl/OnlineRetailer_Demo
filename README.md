# OnlineRetailer_Demo

The example is based on my partial solution to the Microservice mini project, where I have added an Ocelot API Gateway to the solution. I have tested that the example can run using Docker Compose. The example cannot yet run using Kubernetes.


The docker-compose file has been modified so that the services listens on the following ports for requests outside the docker host:


OnlineRetailerApiGateway: 7000
ProductApi: 7001
OrderApi: 7002

Re-routes are defined in the ocelot.json file inside the OnlineRetailerApiGateway project.


There is a Postman collection in the Postman subfolder that you can use to test the solution.


Beware that the solution uses CloudAMQP. You should modify the  "cloudAMQPConnectionString" in the Startup classes of ProductApi and OrderApi to match your own RabbitMQ server, if you want to run the example.

