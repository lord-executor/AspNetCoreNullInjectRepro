# Instructions
Run the project with `dotnet run .` (from within the project directory) and then connect to https://localhost:5001/ with your browser.

The result should be an error page with the content
```
ActivationException: Error activating IActionContextAccessor using conditional implicit self-binding of IActionContextAccessor
Provider returned null.
Activation path:
6) Injection of dependency IActionContextAccessor into parameter actionContextAccessor of constructor of type PageRequestDelegateFactory
5) Injection of dependency IRequestDelegateFactory into parameter requestDelegateFactories of constructor of type ActionEndpointFactory
4) Injection of dependency ActionEndpointFactory into parameter endpointFactory of constructor of type DefaultPageLoader
3) Injection of dependency PageLoader into parameter loader of constructor of type DynamicPageEndpointMatcherPolicy
2) Injection of dependency MatcherPolicy into parameter policies of constructor of type DfaMatcherBuilder
1) Request for DfaMatcherBuilder

...
```

In `Program.cs` line 35, there is a workaround prepared that "solves" the problem of the missing provider for `IActionContextAccessor`. Ideally however, this should not be necessary.
