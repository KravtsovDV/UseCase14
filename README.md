# UseCase14
Test task for Generative AI survey. UseCase #14.

## Description
Implement internationalization and localization logic to support multiple regions in the application.

Application demonstrates different types of number formatting, currencies, text localization and units of measurements (length/mass/volume).

If some text localizations are not available (e.g., in FR locale), application just leaves key string used for it (i.e., default behavior of ASP.NET internationalization with _IStringLocalizer_).

If locale used in URL is not supported, application will use default 'en-US' locale.

## Installation

1. Clone the repository.
``` git clone https://github.com/KravtsovDV/UseCase14.git```


2. Open console in the root folder and run the commands
   ```
   dotnet build
   dotnet run
   ```

## Endpoints usage
Locale is determined as the first part of URL, right after top-level size address, e.g.:
https://localhost:44313/en-US/Test/Privacy will give 'en-US' culture. You can also use shorter version https://localhost:44313/en/Test/Privacy as 'en' will be found in 'en-US' supported culture.

To see the example of fully translated page go to main page with EN or UK culture:
https://localhost:44313 (defaults to 'en-US')
https://localhost:44313/uk (uses 'uk-UA' culture)
_Note that you may have some other port - look it in console output after ```dotnet run``` from [Installation](#Installation)_

There you will see datetime in several formats, numbers in culture-specific formatting (normal and exponential variants) and currencies (you can also switch to 'de-DE' locale to see difference in number formatting better).
Below them you will see localization of some length, volume and mass values (note, that they all have one and the same value and automatically converted according to culture - change locale in URL to see the difference).
You can also use links on the page - e.g., to Privacy Page.

To see the page with some translations lacking use 'de'/'de-DE' or 'fr'/'fr-FR' URLs (note that, e.g., 'fr-CA' locale is not supported - i.e., you should use either shorter versions like 'fr' or fully supported cultures - 'en-US', 'uk-US', 'fr-FR' and 'de-DE').

To see how page defaults to 'en-US' locale when URL locale is not supported, go to, e.g., https://localhost:44313/es-ES
