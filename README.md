![logo header](/assets/header.jpeg "logo header")

NASABot integrated with NASA API and LUIS (Language Recognition Service). It provides access to the latest NASA API (like Space Weather Database Of Notifications and other NASA services) using plain English and Natural User Flow.

# Architecture Overview
![logo header](/assets/architecture-overview-2.jpg  "logo header")

# Configuration Check List

1. Obtain NASA API key here https://api.nasa.gov/ 
2. Setup LUIS service and obtain API key
3. Update configuration file

```JS
{
  "NasaApi": {
    "ApiKey": "[key]"
  },
  "Luis": {
    "LuisAppId": "[key]",
    "LuisAPIKey": "[key]",
    "LuisAPIHostName": "[key]"
   },
  }
}
```


# Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request