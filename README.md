# 🧪 Playwright Web Test Automation Framework (.NET + NUnit)

## 📁 Project Structure Overview

This framework is built using:

- **Microsoft.Playwright** for browser automation.
- **NUnit** for organizing and executing tests.
- **ExtentReports** for visual reporting.
- A modular **Page Object Model (POM)** structure for maintainability and reusability.

### Key Folders and Files:

| Path                         | Description                                                         |
|------------------------------|----------------------------------------------------------------------|
| `Bases/TestBase.cs`          | Abstract class that initializes the browser, context, and page.     |
| `Bases/PageBase.cs`          | Core base for all pages, includes reusable interactions.             |
| `Helpers/JsonHelpers.cs`     | Reads configuration from `appsettingsQA.json`.                       |
| `Helpers/ExtentReportHelpers.cs` | Handles integration with ExtentReports.                           |
| `Helpers/BrowserHelpers.cs`  | Decides which browser to launch based on configuration.             |
| `Helpers/PlaywrightSessionHelpers.cs` | Creates contexts and pages with configuration support.       |
| `Pages`         | Page object, inherits from `PageBase`.                      |
| `Tests`        | NUnit test file, inherits from `TestBase`.                          |

## 🧱 C# Development Standards

This project adheres to **common .NET/C# best practices**:

| Principle                          | Implementation Example                             |
|-----------------------------------|----------------------------------------------------|
| ✔️ Async/Await everywhere         | All Playwright methods are async (`Task<>`)        |
| ✔️ Dependency inversion (DI ready)| Pages and helpers are injected with `IPage`, `ExtentTest` |
| ✔️ SOLID Principles               | Each class has a clear single responsibility       |
| ✔️ Readable Naming Conventions    | CamelCase for methods, PascalCase for types        |
| ✔️ Logging & Reporting            | Built-in screenshot capture and Extent logging     |

## 🔧 Configuration

### 📄 appsettingsQA.json

```json
{
  "BROWSER": "chromium",
  "URL": "https://your-app-url.com",
  "DEFAULT_TIMEOUT": "10000"
}
```

### Supported Browsers

- `chromium`
- `firefox`
- `webkit`

## 🚀 How to Run Tests

Install dependencies:

```bash
dotnet restore
playwright install
```

Run tests:

```bash
dotnet test
```

Optionally specify browser:

```bash
dotnet test -- Playwright.BrowserName=chromium

dotnet test -- NUnit.NumberOfTestWorkers=1
```

## 🧩 Writing Page Objects

Each page class inherits from `PageBase.cs`, which includes common methods like:

```csharp
await ClickAsync(locator);
await SendKeysAsync(locator, "value");
await GetTextAsync(locator);
await ElementExistsAsync(locator);
etc..
```

Example:

```csharp
public class LoginPage : PageBase
{
    private readonly ILocator usernameInput;
    private readonly ILocator passwordInput;
    private readonly ILocator loginButton;

    public LoginPage(IPage page, ExtentTest test) : base(page, test)
    {
        usernameInput = page.Locator("#username");
        passwordInput = page.Locator("#password");
        loginButton   = page.Locator("#login");
    }

    public async Task LoginAsync(string user, string pass)
    {
        await SendKeysAsync(usernameInput, user);
        await SendKeysAsync(passwordInput, pass);
        await ClickAsync(loginButton);
    }
}
```

## 🧪 Creating Tests

Tests extend `TestBase.cs`, which handles setup, teardown, and navigation.

```csharp
[Test]
public async Task LoginShouldWork()
{
    var loginPage = new LoginPage(Page, _test);
    await loginPage.LoginAsync("admin", "adminpass");

    Assert.True(await Page.IsVisibleAsync("text=Dashboard"));
}
```

## 📊 Reports

- HTML reports are generated using ExtentReports.
- Each action logs a screenshot and a readable message.
- Report is flushed after all tests complete.
