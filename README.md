# Roulette Bets API Backend

# Resumen
Este proyecto se trata de un juego de Ruleta, donde se exponen diferentes endpoints para poder ser consumidos

### Endpoints Roulette
* POST -> Crea una nueva Ruleta
* PATCH -> Cambia el estado de una Ruleta (OPEN-CLOSE, CLOSE-OPEN)
* GET -> Obtiene todas las ruletas existentes
* GET{id} -> Retorna una ruleta dado un id
### Endpoints Bet
* POST -> Abre una nueva apuesta en una ruleta abierta
* PUT -> Cierra todas las apuestas, cierra la ruleta, borra las apuestas hechas y retorna el estado final del juego en especifico.
### Endpoint User
* POST -> Crea un nuevo usuario para el sistema
### Endpoint Token Bearer
* POST -> Crea y devuelve un token para poder Loguearse, dado un usuario específico
### :bulb: Diagrama de Componentes


### 🎯 Atributos de Calidad
|Atributo|Fuente|Estimulo|Ambiente|Respuesta|Medida de respuesta|
|--|--|--|--|--|--|
|Disponibilidad|Externo al sistema|Despliegue en aws se averió|Operaciones normales |Realizar nuevamente el despliegue|3 horas|  
|Mantenibilidad|Interno al sistema|Código con malas práticas|Producción|evaluar código fuente y realizar correcciones|1 hora |  
|Seguridad|Usuario no loggeado|Intenta loggearse al sistema|Bajo operaciones normales  |No permitir al usuario loggearse |menos de 30 minutos|
|Escalabilidad |externo al sistema|servidor muy lento|operaciones normales  |escalabilidad horizontal| menos de 2 minutos|
### Author
* Carlos Javier Orduz Trujillo 
