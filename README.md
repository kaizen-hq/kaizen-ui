# Kaizen.UI

Kaizen.UI is a Blazor component library providing reusable UI components and
patterns for building web applications.

You can view the demo and documentation [here](https://ui.gokaizen.io)

## Installation

1. `dotnet add package KaizenIO.UI`
2. Reference the stylesheet
    ```html
    <link rel="stylesheet" href="@Assets["_content/KaizenIO.UI/kaizen-ui.css"]" />
    ```
3. Add the following to your `_Imports.razor` file:
    ```html
    @using Kaizen.UI.Components
    @using Kaizen.UI
    @using Blazicons
    ```
4. To get the ToastService to work, add this to your `Program.cs`.
    ```cs
    builder.Services.AddScoped<ToastService>();
    ```
    Then add the `Toaster` to your `MainLayout.razor` file.  See example below.

## Components

### Layout 

Kaizen UI ships with some css classes that let you arrange the layout in 
mobile and desktop friendly way.  Here is a simple example of a layout
with a collapsible left navigation.

This also shows how to setup the `Toaster` so the `ToastService` will work.

```html
@inherits LayoutComponentBase

<Toaster />

<div class="layout">
    <header>
        <a href="/" class="logo"></a>
    </header>
    <aside>
        <LeftNavWrapper />
    </aside>
    <main>
        @Body
    </main>
</div>
```
### Default Styles

The `kaizen-ui.css` contains default styles that will affect how inputs appear,
as well as provide several classes to help provide a consisten look and feel
throughout your application.

These are fairly minimal.  The Kaizen UI philosophy is to leave the creativity
up to the consumer and to be easy to override.  The styles presented here are
meant to be good starting off points, and aren't very opinionated. 

## Requirements
- Supported version of .NET (9.0 or higher)

## Other Projects

This project uses `Blazicons.Lucide` for icons.  More on that
[here](https://github.com/kyleherzog/Blazicons.Lucide?tab=readme-ov-file).

## Contributing

Feel free to submit issues and enhancement requests.

## License

MIT
