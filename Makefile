.PHONY: help publish

help:
	@echo "Kaizen UI - Makefile Commands"
	@echo ""
	@echo "  make publish   - Build and publish for production (outputs to ./publish/wwwroot)"
	@echo ""

publish:
	@echo "Publishing for production..."
	cd Kaizen.Sample.Web.Client && dotnet publish -c Release -o ./publish
	@echo ""
	@echo "✓ Build complete! Deploy folder: Kaizen.Sample.Web.Client/publish/wwwroot"
	@echo ""
	@echo "To deploy to Netlify:"
	@echo "  1. Drag/drop the wwwroot folder to https://app.netlify.com/drop"
	@echo "  2. Or run: make deploy"
