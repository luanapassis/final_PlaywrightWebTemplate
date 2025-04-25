# PlaywrightWebTemplate

## 🧪 Overview

**PlaywrightWebTemplate** is a robust and modular test automation framework built using:

- ✅ [Playwright](https://playwright.dev/dotnet)
- ✅ [NUnit](https://nunit.org/)
- ✅ [ExtentReports](https://extentreports.com/)
- ✅ C# (.NET)
- ✅ SQL Server Integration
- ✅ Configuration-driven architecture

It supports **parallel execution**, **video recording**, **step-by-step screenshots**, **test data from SQL**, and **multi-environment configuration**.

---

## 📂 Project Structure

```
PlaywrightWebTemplate/
│
├── Bases/              # Test and Page base classes
├── DataBaseSteps/      # Classes for SQL-based test data retrieval
├── Helpers/            # Utilities (browser setup, reporting, config, etc.)
├── Pages/              # Page Object Models
├── Queries/            # SQL files for test data
├── Reports/            # Generated HTML test reports (ExtentReports)
├── Steps/              # Reusable test steps (e.g. login flow)
├── Tests/              # Test classes organized by feature or domain
│
├── appsettingsQA.json       # Environment-specific config
├── environment.json         # Current environment selector
└── PlaywrightWebTemplate.csproj
```

---

## ⚙️ Configuration

Configurations are stored in external JSON files:

- **environment.json**: defines the current environment (`QA`, `UAT`, `DEV`)
- **appsettings{ENV}.json**: stores environment-specific settings

Example: `appsettingsQA.json`

```json
{
  "URL": "https://www.saucedemo.com/",
  "BROWSER": "chromium",
  "HEADLESS": "false",
  "RECORD_VIDEO": "true",
  "SCREENSHOT_EVERY_STEP": "true",
  "DEFAULT_TIMEOUT": 100000,
  ...
}
```

---

## 🧰 Features

- 🌐 **Cross-browser** support (Chromium, Firefox, WebKit)
- 🎥 **Video recording** and 📸 **screenshots** at every step
- 📊 **HTML reporting** with [ExtentReports](https://extentreports.com/)
- 🧬 **Reusable Step classes** for modularity
- 🔧 **Configurable timeouts, headless mode, locale, and base URL**
- 🧮 **SQL Server integration** to fetch test data via `.sql` files

---

## ▶️ How to Run

### 1. Install Dependencies

```bash
dotnet restore
```

### 2. Run All Tests

```bash
dotnet test
```

### 3. Run a Specific Test File

```bash
dotnet test --filter "FullyQualifiedName~PlaywrightWebTemplate.Tests.LoginTests"
```

---

## 📁 Reports

- HTML reports are generated automatically under the `/Reports` directory.
- Screenshots and videos are attached to each step (if enabled in config).

---

## 🧪 Test Execution Strategy

- Each test inherits from `TestBase`, which sets up:
  - A new browser and context per test
  - ExtentReports integration
  - Full cleanup of context and browser at the end
- Pages inherit from `PageBase`, which wraps:
  - Playwright actions with logging
  - Tab/window switching
  - Step-level screenshot capture

---

## ✅ Best Practices

- Define all test data outside the code (SQL, JSON, config).
- Reuse Steps to avoid duplication across tests.
- Use environment-based configuration to support multiple stages (QA/UAT/DEV).
- Use `[Parallelizable]` to run tests faster.

---

## 🧩 Extensions & Next Steps

This framework is built for scalability. It is easy to extend with:

- CI/CD integration (GitHub Actions, Azure DevOps, Jenkins)
- Data-driven testing (`TestCaseSource`)
- API testing layer
- Retry policies
- Custom CLI to generate Pages/Steps/Tests

---

## ✍️ Authors

Created and maintained by the QA team at [Update Here].
