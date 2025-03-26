
# Drawing Captcha Umbraco Integration

This is the NuGet package for .NET 8 .NET Core applications, primarily written to extend the Umbraco Forms Integration. You can find the package on [NuGet](https://www.nuget.org/packages/DrawingCaptcha/).

## Installation

Once the NuGet package is installed, the package source will be copied to `~/App_Plugins/Drawing_Captcha/`.

## Setup

### Script Integration

After installation, implement the script that triggers the drawing captcha attributes. It is recommended to place this script in the base page view to ensure it is rendered after the form:

```html
<script src="~/App_Plugins/Drawing_Captcha/captcha.js" onload="initializeCaptcha()"></script>
```

Example integration in a Razor view:

```razor
@using System.Globalization
@inherits UmbracoViewPage<IBasePage>
@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration
@inject Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor

<!DOCTYPE html>
<html>
<head>
</head>
<body>
    @await Html.PartialAsync("Partials/Header")
    @RenderBody()
    @await Html.PartialAsync("Partials/Footer")
    <script src="~/App_Plugins/Drawing_Captcha/captcha.js" onload="initializeCaptcha()"></script>
</body>
</html>
```

### Configuration

Ensure your API key and server are configured in `appsettings.json`:

```json
"Forms": {
    "FieldTypes": {
        "DrawingCaptcha": {
            "APIKey": "yourapikey",
            "Server": "https://yourdrawingcaptchainstance.com/"
        }
    }
}
```

You can obtain an API key by creating a company account if you haven't done so already. Make sure to allow the origin that will be using the drawing captcha.

![Origin Setup](https://github.com/user-attachments/assets/8eaf6df7-090d-4c5a-8253-44528dab4dde)

### API Key Management

Navigate to drawing-captcha > api-keys to create a new API key.

![API Key Navigation](https://github.com/user-attachments/assets/04afe91c-75d9-4ed5-8252-ce62969c6c81)

Create an API key:

![API Key Creation](https://github.com/user-attachments/assets/de5e86df-68c5-415f-8686-49f263cdaa62)

### Umbraco Backoffice Integration

After setting up the configuration, navigate to the Umbraco Forms backoffice. You should see the extended field type added by the package, indicating that the drawing captcha is active.

![Field Type Added](https://github.com/user-attachments/assets/d5b09c52-cca9-4901-a349-477a940de258)

Here you can see that the drawing captcha is active:

![Captcha Active](https://github.com/user-attachments/assets/e3cb0304-41b7-4b54-8d80-b9ce00ecdf21)

## Additional Resources

- [GitHub Repository](https://github.com/Drawing-Captcha/Nuget)
- [Documentation](https://docs.drawing-captcha.com)

## License

This project is licensed under the MIT License.

## Contact

For issues or contributions, please visit the [GitHub repository](https://github.com/Drawing-Captcha/Nuget).

