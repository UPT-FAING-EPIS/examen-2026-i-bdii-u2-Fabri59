variable "project_name" {
  type        = string
  description = "Project name prefix."
  default     = "tickethub"
}

variable "location" {
  type        = string
  description = "Azure region for deployment."
  default     = "East US"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name."
  default     = "rg-tickethub-dev"
}

variable "mongo_connection_string" {
  type        = string
  description = "MongoDB Atlas or Cosmos Mongo API connection string."
  sensitive   = true
  default     = ""
}
