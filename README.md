# Roulette Bets API Backend

# Resumen
Este proyecto se trata de un juego de Ruleta, donde se exponen diferentes endpoints para poder ser consumidos. La aplicación tiene una arquitectura orientada a microservicios, se utiliza servicio de cache Azure Redis para mejorar la escalabilidad y rendimiento de la aplicación, se encuentra actualmente desplegada en AWS Lambda.

### Endpoints Roulette
* POST -> Crea una nueva Ruleta /v1/roulettes
  * Ejemplo de uso
    ```
    {
        "name": "Ruleta 6",
        "state": "OPEN"
    }
    ```
* PATCH -> Cambia el estado de una Ruleta (CLOSE-OPEN) /v1/roulettes
  * Ejemplo de uso
    https://oim5y3xrfn7mez4yo4x3k5atxu0bbuol.lambda-url.us-east-1.on.aws/v1/roulettes/{id de ruleta}
* GET -> Obtiene todas las ruletas existentes /v1/roulettes
* GET{id} -> Retorna una ruleta dado un id /v1/roulettes
  * Ejemplo de Uso
    https://oim5y3xrfn7mez4yo4x3k5atxu0bbuol.lambda-url.us-east-1.on.aws/v1/roulettes/{id de ruleta}
    https://oim5y3xrfn7mez4yo4x3k5atxu0bbuol.lambda-url.us-east-1.on.aws/v1/roulettes/648d1291c75700597a89ac03
### Endpoints Bet
* POST -> Abre una nueva apuesta en una ruleta abierta /v1/bets
  * Ejemplo de Uso
    ```
    {
      "number": 31, (entre 0 y 36)
      "color": "red", (red o black)
      "quantity": 206, (Mayor que 0 y menor que 10000)
      "rouletteId": "648d1291c75700597a89ac03" (Id de Ruleta que esté abierta)
    }
    ```
* PUT -> Cierra todas las apuestas, cierra la ruleta, borra las apuestas hechas y retorna el estado final del juego en especifico.
  * Ejemplo de Uso
    https://oim5y3xrfn7mez4yo4x3k5atxu0bbuol.lambda-url.us-east-1.on.aws/v1/bets?rouletteId={Id de la ruleta}
### Endpoint User
* POST -> Crea un nuevo usuario para el sistema /v1/users/signUp
  * Ejemplo de uso
    ```
     {
          "displayName": "Julian Bonilla",
          "userName": "julian.bonilla",
          "email": "julian.bonilla@gmail.com",
          "password": "julian1234"
      }
    ```
  
### Endpoint Token Bearer
* POST -> Crea y devuelve un token para poder Loguearse, dado un usuario específico
  * Ejemplo de Uso
    ```
    {
      "email":"cjot60@gmail.com,
      "password":"sanord$20"
    }
    ```
### :bulb: Diagrama de Componentes
![imagen](https://github.com/CarlosOrduz777/Roulette_Bets_API/blob/main/Images/RouletteBetsComponentsDiagram.drawio(1).png)

### 🎯 Atributos de Calidad
|Atributo|Fuente|Estimulo|Ambiente|Respuesta|Medida de respuesta|
|--|--|--|--|--|--|
|Disponibilidad|Externo al sistema|Despliegue en aws se averió|Operaciones normales |Realizar nuevamente el despliegue|3 horas|  
|Mantenibilidad|Interno al sistema|Código con malas práticas|Producción|evaluar código fuente y realizar correcciones|1 hora |  
|Seguridad|Usuario no loggeado|Intenta loggearse al sistema|Bajo operaciones normales  |No permitir al usuario loggearse |menos de 30 minutos|
|Escalabilidad |externo al sistema|servidor muy lento|operaciones normales  |escalabilidad horizontal| menos de 2 minutos|
### Aspectos a Mejorar para una siguiente versión
* Las contraseñas deben ir encriptadas a la base de datos
* Agregar pruebas unitarias
* Ejecutar la aplicación en tiempo real
### Aprendizaje
* Fue un reto gigante ya que nunca había programado en .Net Core 6.0, tuve algunos inconvenientes en las versiones de algunas librerías. Lo cual conllevaría una mayor investigación.
### Author
* Carlos Javier Orduz Trujillo 
