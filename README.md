# TwitterClone

TwitterClone es una aplicación web que clona las funcionalidades básicas de Twitter. Está desarrollada utilizando .NET y MySQL, y se distribuye utilizando Docker para facilitar su despliegue en diferentes entornos.

## Requisitos

- [Docker](https://www.docker.com/get-started) (Version 20.10 o superior)
- [Git](https://git-scm.com/downloads)

## Instalación

### 1. Descargar e instalar Docker
Sigue las instrucciones oficiales para instalar Docker en tu sistema operativo:

- [Docker para Windows](https://docs.docker.com/docker-for-windows/install/)
- [Docker para Mac](https://docs.docker.com/docker-for-mac/install/)
- [Docker para Linux](https://docs.docker.com/engine/install/)

Verificar que Docker esté ejecutándose correctamente antes de continuar.

### 2. Clonar el repositorio
Abre una terminal y ejecuta el siguiente comando para clonar el repositorio:
git clone https://github.com/MathiClaude/twitter-clone.git

### 3. Navegar al directorio del proyecto
cd TwitterClone

### 4. Construir y levantar los contenedores Docker
Ejecuta el siguiente comando para construir las imágenes de Docker y levantar los contenedores:

docker-compose up --build

### 5. Acceder a la aplicación
Una vez que los contenedores estén corriendo, puedes acceder a la aplicación en tu navegador web en la siguiente URL:
http://localhost:8080/swagger/index.html

Mediante el swagger se podran probar todas las peticiones de las funcionalidades especificadas.

## Estructura del Proyecto
El proyecto está estructurado en cuatro carpetas principales:

- [TwitterClone.API]: Contiene la API del proyecto.
- [TwitterClone.Application]: Contiene la lógica de la aplicación.
- [TwitterClone.Domain] : Contiene las entidades y lógica de dominio.
- [TwitterClone.Infrastructure]: Contiene la lógica de acceso a datos y otras infraestructuras.

## Conexión a la Base de Datos
La base de datos MySQL se levanta como un contenedor Docker separado. La conexión a la base de datos se define en el archivo appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=mysql-db;Database=twitter_clone;User=root;Password=root;SslMode=none;"
}

Donde se pude apreciar tanto el usuario como el password para acceder a la misma

## Recomendacion 

### Uso de la Interfaz de Rider para Consultar la Base de Datos
He realizado el proyecto apoyandome en las ventajas que brinda la interfaz de JetBrains Rider, el cual
proporciona una interfaz gráfica para consultar la base de datos. 
También puedes utilizar otras herramientas como MySQL Workbench, DBeaver o la línea de comandos mysql, simplemente se deben usar 
las credenciales ya mencionadas arriba