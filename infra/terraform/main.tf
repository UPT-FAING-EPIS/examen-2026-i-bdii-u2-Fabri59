resource "azurerm_resource_group" "this" {
  name     = var.resource_group_name
  location = var.location
}

resource "azurerm_container_registry" "this" {
  name                = replace("${var.project_name}acr", "-", "")
  resource_group_name  = azurerm_resource_group.this.name
  location            = azurerm_resource_group.this.location
  sku                 = "Basic"
  admin_enabled       = true
}

resource "azurerm_log_analytics_workspace" "this" {
  name                = "law-${var.project_name}"
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_container_app_environment" "this" {
  name                       = "cae-${var.project_name}"
  location                   = azurerm_resource_group.this.location
  resource_group_name        = azurerm_resource_group.this.name
  log_analytics_workspace_id = azurerm_log_analytics_workspace.this.id
}

resource "azurerm_container_app" "api" {
  name                         = "api-${var.project_name}"
  container_app_environment_id  = azurerm_container_app_environment.this.id
  resource_group_name           = azurerm_resource_group.this.name
  revision_mode                 = "Single"
  registry {
    server   = azurerm_container_registry.this.login_server
    username = azurerm_container_registry.this.admin_username
    password = azurerm_container_registry.this.admin_password
  }

  template {
    container {
      name   = "api"
      image  = "${azurerm_container_registry.this.login_server}/tickethub-api:latest"
      cpu    = 0.5
      memory = "1Gi"
      env {
        name  = "Mongo__ConnectionString"
        value = var.mongo_connection_string
      }
      env {
        name  = "ASPNETCORE_URLS"
        value = "http://+:8080"
      }
    }
  }

  ingress {
    external_enabled = true
    target_port      = 8080
    transport        = "auto"
  }
}

resource "azurerm_storage_account" "web" {
  name                     = substr(replace("${var.project_name}web", "-", ""), 0, 24)
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  account_kind             = "StorageV2"
  static_website {
    index_document     = "index.html"
    error_404_document = "index.html"
  }
}

output "api_url" {
  value = azurerm_container_app.api.ingress[0].fqdn
}

output "web_storage_account" {
  value = azurerm_storage_account.web.name
}
