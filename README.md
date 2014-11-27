#MyRoom

##Estructura básica del proyecto

El proyecto está estructurado en 2 carpetas:

* Docs Incluye la documentación proporcionada por el cliente
* RestAPI Incluye el proyecto de Visual Studio 2013 para la API RESTful

##Cliente Web

El cliente web está desarrollado con AngularJS. cada apartado o sección está montada en un HTML diferente y cada HTML tiene asociado un Módulo de Angular.

Los módulos de AngularJS están agrupados en la carpeta /resources/js/
Los pre-requisitos (librerías externas al proyecto) son cargadas a partir los diferentes CDN.

##Test

Para realizar las pruebas de la API sin necesidad del cliente se puede utilizar cualquier cliente REST. Recomendados:

* [Postman - REST Client](https://chrome.google.com/webstore/detail/postman-rest-client/fdmmgilgnpjigdojojpjoooidkmcomcm)
* [Fiddler](http://www.telerik.com/fiddler)