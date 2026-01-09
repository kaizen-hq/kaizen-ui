.PHONY: help watch publish deploy nuget-pack nuget-publish swa-create swa-deploy swa-deployment-token

# Optionally include .env file (silently ignored if missing)
-include .env

# Azure SWA configuration (override via environment or .env file)
AZURE_RESOURCE_GROUP ?= kaizen-ui-rg
AZURE_SWA_NAME ?= kaizen-ui
AZURE_LOCATION ?= centralus

help:
	@echo "Kaizen UI - Makefile Commands"
	@echo ""
	@echo "  make watch          - Run the sample project with hot reload"
	@echo "  make publish        - Build and publish for production (outputs to ./publish/wwwroot)"
	@echo "  make deploy         - Build and deploy to Netlify"
	@echo "  make nuget-pack     - Pack Kaizen.UI as a NuGet package"
	@echo "  make nuget-publish  - Pack and publish Kaizen.UI to NuGet (requires .env with NUGET_API_KEY)"
	@echo ""
	@echo "Azure Static Web Apps:"
	@echo "  make swa-create     - Create Azure Static Web App (requires az cli login)"
	@echo "  make swa-deploy     - Build and deploy to Azure Static Web Apps"
	@echo ""
	@echo "Environment variables for Azure (can be set in .env):"
	@echo "  AZURE_RESOURCE_GROUP  - Resource group name (default: kaizen-ui-rg)"
	@echo "  AZURE_SWA_NAME        - Static Web App name (default: kaizen-ui)"
	@echo "  AZURE_LOCATION        - Azure region (default: centralus)"
	@echo "  AZURE_SWA_TOKEN       - Deployment token (required for swa-deploy)"
	@echo ""

watch:
	@echo "Starting sample project with hot reload..."
	dotnet watch run --project Kaizen.Sample.Web.Client

publish:
	@echo "Publishing for production..."
	cd Kaizen.Sample.Web.Client && dotnet publish -c Release -o ./publish
	@echo ""
	@echo "✓ Build complete! Deploy folder: Kaizen.Sample.Web.Client/publish/wwwroot"
	@echo ""
	@echo "To deploy to Netlify:"
	@echo "  1. Drag/drop the wwwroot folder to https://app.netlify.com/drop"
	@echo "  2. Or run: make deploy"

nuget-pack:
	@echo "Packing Kaizen.UI for NuGet..."
	cd Kaizen.UI && dotnet pack -c Release
	@echo ""
	@echo "✓ Package created in Kaizen.UI/bin/Release/"
	@echo ""

nuget-publish: nuget-pack
	@echo "Publishing Kaizen.UI to NuGet..."
	@if [ ! -f .env ]; then \
		echo "Error: .env file not found. Create a .env file with NUGET_API_KEY=your_api_key"; \
		exit 1; \
	fi
	@export $$(cat .env | xargs) && \
	if [ -z "$$NUGET_API_KEY" ]; then \
		echo "Error: NUGET_API_KEY not set in .env file"; \
		exit 1; \
	fi && \
	dotnet nuget push Kaizen.UI/bin/Release/KaizenIO.UI.*.nupkg \
		--api-key $$NUGET_API_KEY \
		--source https://api.nuget.org/v3/index.json
	@echo ""
	@echo "✓ Package published to NuGet!"

swa-create:
	@echo "Creating Azure Static Web App..."
	az group create --name $(AZURE_RESOURCE_GROUP) --location $(AZURE_LOCATION)
	az staticwebapp create \
		--name $(AZURE_SWA_NAME) \
		--resource-group $(AZURE_RESOURCE_GROUP) \
		--location $(AZURE_LOCATION) \
		--sku Free
	@echo ""
	@echo "✓ Static Web App created!"
	@echo ""

swa-deployment-token:
	@echo "Deployment token for GitHub Actions (add as AZURE_STATIC_WEB_APPS_API_TOKEN secret):"
	@az staticwebapp secrets list --name $(AZURE_SWA_NAME) --resource-group $(AZURE_RESOURCE_GROUP) --query 'properties.apiKey' -o tsv


swa-deploy: publish
	@echo "Deploying to Azure Static Web Apps..."
	swa deploy Kaizen.Sample.Web.Client/publish/wwwroot \
		--deployment-token $(AZURE_SWA_TOKEN) \
		--env production
	@echo ""
	@echo "✓ Deployment complete!"
