# Redirect Middleware Demo

## Code Challenge

Create a component that can be added to any ASP.NET Core application to handle redirects.
 
### Acceptance Criteria

- The component will get the redirects from a RESTful API, but for now, create a mock service that returns the following JSON.
- The component must cache the results in memory but allow multiple threads to read the cache.
- The cache must be updated every few minutes without requiring any HTTP request to wait for the refresh.
- The cache duration must be configurable.
- Failed API calls are logged as errors.
- Successful API calls and cache refreshes are logged as information.
- If `useRelative` is true, it redirects the target to a relative destination instead of an exact destination. For example, in the data below, `/product-directory/bits/masonary/diamond-tip` would redirect to `/products/bits/masonary/diamond-tip`.

### Sample Service Results

```
[
    {
        "redirectUrl": "/campaignA",
        "targetUrl": "/campaigns/targetcampaign",
        "redirectType": 302,
        "useRelative": false
    },
    {
        "redirectUrl": "/campaignB",
        "targetUrl": "/campaigns/targetcampaign/channelB",
        "redirectType": 302,
        "useRelative": false
    },
    {
        "redirectUrl": "/product-directory",
        "targetUrl": "/products",
        "redirectType": 301,
        "useRelative": true
    }
]
```

## Usage

To use the redirect middleware, follow these steps:

1. Add a project reference to the `RedirectionMiddleware` project on your ASP.NET Core application. Note that for a real-world scenario, this would be published as a NuGet package.
1. In your `program.cs` file, add the following line of code to add the middleware and necessary assemblies:

        builder.Services.AddRedirectMiddleware("YOUR_API_URL_HERE", TimeSpan.FromMinutes(30));

1. In the same file, add the following line of code near the top of your request pipeline, likely prior to `app.UseRouting();`:

        app.UseRedirectMiddleware();
