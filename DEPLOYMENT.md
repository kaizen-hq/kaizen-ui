# Deploying Kaizen UI to Netlify

This project has been converted to a standalone Blazor WebAssembly application that can be deployed to static hosting services like Netlify.

## Build for Production

To build the project for deployment:

```bash
cd Kaizen.Sample.Web.Client
dotnet publish -c Release -o ./publish
```

The deployable files will be in `./publish/wwwroot/`

## Deploy to Netlify

### Option 1: Netlify CLI

1. Install the Netlify CLI:
```bash
npm install -g netlify-cli
```

2. Build and deploy:
```bash
cd Kaizen.Sample.Web.Client
dotnet publish -c Release -o ./publish
netlify deploy --prod --dir=./publish/wwwroot
```

### Option 2: Netlify Web UI

1. Build the project:
```bash
cd Kaizen.Sample.Web.Client
dotnet publish -c Release -o ./publish
```

2. Go to https://app.netlify.com/drop
3. Drag and drop the `./publish/wwwroot` folder

### Option 3: Git Integration

1. Push your code to GitHub
2. In Netlify, create a new site from Git
3. Configure build settings:
   - **Base directory**: `Kaizen.Sample.Web.Client`
   - **Build command**: `dotnet publish -c Release -o ./publish`
   - **Publish directory**: `Kaizen.Sample.Web.Client/publish/wwwroot`

## Configuration

Create a `netlify.toml` file in the project root for automatic deployment:

```toml
[build]
  base = "Kaizen.Sample.Web.Client"
  command = "dotnet publish -c Release -o ./publish"
  publish = "Kaizen.Sample.Web.Client/publish/wwwroot"

[[redirects]]
  from = "/*"
  to = "/index.html"
  status = 200
```

This configuration ensures that client-side routing works correctly.

## Local Development

To run the WASM app locally:

```bash
cd Kaizen.Sample.Web.Client
dotnet run
```

Then open your browser to the URL shown in the console (typically http://localhost:5000).

## What Changed

The project was converted from a Blazor Web App with server components to a standalone Blazor WebAssembly app:

1. Created `index.html` in the client project
2. Removed `@rendermode` directives (everything runs on the client now)
3. Copied static assets to the client `wwwroot` folder
4. Updated the client `.csproj` for standalone WASM hosting
5. Sample code is embedded as resources (no server API needed)

The server project (`Kaizen.Sample.Web`) is no longer needed for deployment.
