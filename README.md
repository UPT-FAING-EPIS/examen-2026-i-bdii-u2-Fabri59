# Ticketing Hub

Plataforma base para venta de entradas a eventos con backend en .NET 8, frontend en React, persistencia orientada a MongoDB y automatización con Terraform + GitHub Actions.

## Estructura

- `src/Api/Ticketing.Api`: API REST principal.
- `src/Core/Ticketing.Domain`: entidades y reglas de dominio.
- `src/Core/Ticketing.Application`: contratos y servicios de aplicación.
- `src/Infrastructure/Ticketing.Infrastructure.Mongo`: persistencia y configuración de infraestructura.
- `src/Libraries/Ticketing.EntityMapper`: librería para mapeo entidad-documento y migraciones.
- `tools/Ticketing.Seeder`: automatización para migración/siembra de datos iniciales.
- `tools/Ticketing.SchemaExporter`: generación de documentación y diagrama de la base de datos.
- `web/ticketing-web`: frontend React.
- `infra/terraform`: IaC para despliegue en nube.
- `.github/workflows`: automatizaciones de CI, infraestructura y documentación.

## Desarrollo local

1. Ejecuta el backend con `dotnet run --project src/Api/Ticketing.Api`.
2. Ejecuta el frontend con `npm install` y `npm run dev` dentro de `web/ticketing-web`.
3. Genera la documentación de base de datos con `dotnet run --project tools/Ticketing.SchemaExporter -- docs/database`.

## Infraestructura

La carpeta `infra/terraform` deja preparado el despliegue en Azure con Container Apps, Container Registry y Storage Account.
La base de datos vive en la VM `38.250.116.71` con MongoDB expuesto en `27017`, así que el backend apunta a `mongodb://38.250.116.71:27017/ticketing`.
Cuando tengas credenciales de Azure, puedo conectar el backend, el frontend y el resto del despliegue en ese entorno.

## Secretos de despliegue

GitHub Actions espera estos secretos configurados en el repositorio, sin pegar sus valores en el código:

- `AZURE_WEBAPP_PUBLISH_PROFILE`: credencial JSON de Azure usada por `azure/login`.
- `TERRAFORM_API_TOKEN`: token para la integración con el proveedor externo que uses en Terraform.

Variables opcionales del repositorio para el flujo unificado:

- `AZURE_WEBAPP_NAME`: nombre real del Web App existente o a crear.
- `AZURE_RESOURCE_GROUP`: resource group objetivo.
- `AZURE_LOCATION`: ubicación de Azure, por ejemplo `eastus`.
- `AZURE_APP_SERVICE_PLAN`: nombre del App Service Plan.

Estado actual del proyecto: el backend, el frontend y el workflow unificado ya están cableados para CI y despliegue.

## Despliegue

- El workflow [`ci-deploy`](.github/workflows/deploy.yml) valida backend y frontend en `push` y `pull_request`.
- En `push` a `main` o con `workflow_dispatch`, el mismo workflow también inicia sesión en Azure, crea los recursos faltantes y despliega el frontend por Azure CLI.
- Si no defines `AZURE_WEBAPP_NAME`, el nombre del Web App se calcula desde el repositorio como `<repo>-web`.
- El workflow [`infra`](.github/workflows/infra.yml) valida Terraform antes de aplicar cambios de infraestructura.

## Repositorio

URL del repositorio: pendiente de completar.
