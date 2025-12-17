.PHONY: help watch publish deploy nuget-pack nuget-publish

help:
	@echo "Kaizen UI - Makefile Commands"
	@echo ""
	@echo "  make watch          - Run the sample project with hot reload"
	@echo "  make publish        - Build and publish for production (outputs to ./publish/wwwroot)"
	@echo "  make deploy         - Build and deploy to Netlify"
	@echo "  make nuget-pack     - Pack Kaizen.UI as a NuGet package"
	@echo "  make nuget-publish  - Pack and publish Kaizen.UI to NuGet (requires .env with NUGET_API_KEY)"
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

deploy: publish
	@echo "Deploying to Netlify..."
	netlify deploy --prod --dir=Kaizen.Sample.Web.Client/publish/wwwroot
	@echo ""
	@echo "✓ Deployment complete!"

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
