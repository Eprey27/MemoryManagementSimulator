
# Memory Simulator Frontend

Este proyecto es la interfaz de usuario para el simulador de gestión de memoria. Está construido con Angular y Angular Material.

## Requisitos Previos

- Node.js (versión 14 o superior)
- Angular CLI

## Instalación

```bash
npm install
```

## Ejecución en Desarrollo

```bash
npm start
```

La aplicación estará disponible en [http://localhost:4200](http://localhost:4200).

## Compilación para Producción

```bash
npm run build
```

## Ejecutar Pruebas Unitarias

```bash
npm test
```

## Ejecutar Pruebas E2E

```bash
npm run cypress:open
```

## Configuración del Proxy

Durante el desarrollo, las solicitudes a la API se redirigen al backend mediante el archivo `proxy.conf.json`.

## Estructura del Proyecto

- `src/`: Contiene el código fuente de la aplicación.
- `src/app/`: Contiene los módulos, componentes y servicios de la aplicación.
- `src/environments/`: Contiene los archivos de configuración de entornos.
- `src/assets/`: Contiene los recursos estáticos.

## Versionado y Despliegue

Se puede gestionar la versión y la fecha de despliegue de la aplicación utilizando un archivo de configuración o tags en el repositorio. Asegúrate de mantener la versión actualizada para cada release.

## Contribución

Si deseas contribuir, por favor crea un fork del repositorio y envía un pull request.

## Notas Adicionales

- Es recomendable utilizar un módulo `swap-space` para manejar correctamente la simulación de gestión de memoria.
- Asegúrate de que las solicitudes al API para la validación de direcciones se gestionen adecuadamente en el frontend mediante los servicios.
- Considera implementar un registro detallado de las versiones y fechas de despliegue de los dispositivos IoT conectados.

# Simulador de Gestión de Memoria - Frontend

Este proyecto es la interfaz de usuario para el simulador de gestión de memoria. Está construido con **Angular** y **Angular Material**.

## Requisitos Previos

Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:

- **Node.js** (versión 14 o superior): [Descargar Node.js](https://nodejs.org/)
- **Angular CLI** (versión 15 o superior): Instálalo globalmente ejecutando:

  ```bash
  npm install -g @angular/cli
  ```

## Instalación y Configuración del Proyecto

Sigue estos pasos para configurar y ejecutar el proyecto en tu máquina local.

### 1. Clonar el Repositorio

Si el proyecto está en un repositorio de Git, clónalo usando el siguiente comando. Si ya tienes los archivos, puedes saltar este paso.

```bash
git clone https://github.com/tu-usuario/memory-simulator-frontend.git
```

### 2. Navegar al Directorio del Proyecto

Entra en el directorio del proyecto:

```bash
cd memory-simulator-frontend
```

### 3. Instalar las Dependencias

Instala las dependencias necesarias utilizando **npm**:

```bash
npm install
```

### 4. Configurar el Proxy (Opcional)

Si deseas redirigir las solicitudes a una API backend durante el desarrollo, asegúrate de que el archivo `proxy.conf.json` está configurado correctamente. Por defecto, las solicitudes a `/api` se redirigen a `http://localhost:5000`.

```json
{
  "/api": {
    "target": "http://localhost:5000",
    "secure": false,
    "changeOrigin": true
  }
}
```

### 5. Ejecutar la Aplicación en Desarrollo

Inicia el servidor de desarrollo con el siguiente comando:

```bash
npm start
```

Este comando es equivalente a `ng serve --proxy-config proxy.conf.json`.

La aplicación estará disponible en [http://localhost:4200](http://localhost:4200).

### 6. Compilación para Producción

Para compilar el proyecto para producción, ejecuta:

```bash
npm run build
```

Esto generará los archivos compilados en la carpeta `dist/`.

### 7. Ejecutar Pruebas Unitarias

Para ejecutar las pruebas unitarias con **Karma** y **Jasmine**, utiliza:

```bash
npm test
```

Esto también generará un reporte de cobertura de código en la carpeta `coverage/`.

### 8. Ejecutar Pruebas E2E (End-to-End)

Para ejecutar las pruebas E2E con **Cypress**, sigue estos pasos:

#### 8.1. Iniciar el Servidor de Desarrollo

En una terminal, inicia el servidor de desarrollo si aún no lo has hecho:

```bash
npm start
```

#### 8.2. Abrir Cypress

En otra terminal, ejecuta:

```bash
npm run cypress:open
```

Esto abrirá la interfaz de Cypress donde podrás ejecutar las pruebas E2E.

### 9. Estructura del Proyecto

- `src/`: Contiene el código fuente de la aplicación.
  - `app/`: Contiene los módulos, componentes y servicios de la aplicación.
    - `core/`: Módulo que contiene servicios singleton.
    - `shared/`: Módulo que contiene componentes y modelos compartidos.
    - `features/`: Contiene los módulos de características específicas.
  - `assets/`: Contiene los recursos estáticos.
  - `environments/`: Contiene los archivos de configuración de entornos.
- `proxy.conf.json`: Configuración del proxy para redirigir solicitudes durante el desarrollo.

### 10. Configuración de Variables de Entorno

Si necesitas configurar variables de entorno adicionales, puedes modificar los archivos en `src/environments/`:

- `environment.ts`: Configuración para el entorno de desarrollo.
- `environment.prod.ts`: Configuración para el entorno de producción.

### 11. Actualizar Dependencias (Opcional)

Si deseas actualizar las dependencias del proyecto a sus últimas versiones compatibles, puedes utilizar:

```bash
npm update
```

### 12. Comandos Útiles de Angular CLI

- **Generar un componente**:

  ```bash
  ng generate component nombre-del-componente
  ```

- **Generar un servicio**:

  ```bash
  ng generate service nombre-del-servicio
  ```

- **Construir el proyecto con una configuración específica**:

  ```bash
  ng build --configuration production
  ```

## Recursos Adicionales

- **Documentación de Angular**: [https://angular.io/docs](https://angular.io/docs)
- **Documentación de Angular Material**: [https://material.angular.io/](https://material.angular.io/)
- **Documentación de Cypress**: [https://docs.cypress.io/](https://docs.cypress.io/)

## Contribución

Si deseas contribuir al proyecto, por favor sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una rama para tu feature o fix:

   ```bash
   git checkout -b nombre-de-tu-rama
   ```

3. Realiza tus cambios y realiza commits descriptivos.
4. Empuja tus cambios a tu repositorio fork:

   ```bash
   git push origin nombre-de-tu-rama
   ```

5. Crea un Pull Request en el repositorio original.

## Contacto

Para cualquier duda o sugerencia, puedes contactar al mantenedor del proyecto en [correo@example.com](mailto:correo@example.com).
