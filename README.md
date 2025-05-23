# Expense Tracker

A command-line application built with .NET 9 to help you efficiently manage your personal expenses. You can add, edit, delete, and categorize expenses, view summaries, export your records to CSV, and even set a monthly budget with alerts when it's exceeded.

Project URL: [https://roadmap.sh/projects/expense-tracker](https://roadmap.sh/projects/expense-tracker)

## 📦 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or higher

## 🚀 Features

- **Expense Management**: Add, update, and remove your expenses.
- **Categorization**: Organize expenses into categories like food, transport, utilities, etc.
- **Summaries**: View expense summaries by total, category, or month.
- **Filtering**: Filter expenses by date range and/or category.
- **Monthly Budget Tracking**: Set a monthly budget and receive alerts when it’s exceeded.
- **CSV Export**: Export expenses to CSV files, with optional filters.
- **User-Friendly CLI**: Clear prompts and simple command structure.
- **Persistent Storage**: Your data is saved between sessions.

## ⚙️ Installation

**Clone the repository**

```bash
git clone https://github.com/hectorrosario22/ExpenseTracker.git
cd ExpenseTracker
```

**Build the project**

```bash
dotnet build
```

## 🧪 Usage

Run the application with:
```bash
dotnet run -- <command> [arguments]
```

### 📖 Commands Overview

| Command      | Description                              |
| ------------ | ---------------------------------------- |
| `add`        | Add a new expense                        |
| `update`     | Update an existing expense               |
| `delete`     | Delete an expense                        |
| `list`       | List filtered (or all) expenses          |
| `summary`    | Show total and category-based summaries  |
| `csv`        | Export filtered (or all) expenses to CSV |
| `budget`     | Set or update the monthly budget         |
| `help`       | Show command help                        |

### 📖 Command Examples

**Add an Expense**

```bash
dotnet run -- add --description "Lunch" --amount 20 --category "food" --category "essential"
```

⚠️ If this expense causes your monthly total to exceed the set budget, a warning is displayed.

**Update an Expense**

```bash
dotnet run -- update --id 1 --description "Lunch" --amount 20 --category "food" --category "essential"
```

⚠️ If this expense causes your monthly total to exceed the set budget, a warning is displayed.

**Delete an Expense**

```bash
dotnet run -- delete --id 1
```

**List filtered or all Expenses**

```bash
dotnet run -- list --month 1 --category "food" --category "essential"
```

**Month** and **Category** parameters are optional.

**View Expense Summary**

```bash
dotnet run -- summary --month 1 --category "food" --category "essential"
```

**Month** and **Category** parameters are optional.

**Export to CSV**

```bash
dotnet run -- csv --month 1 --category "food" --category "essential"
```

**Month** and **Category** parameters are optional.

**Set Monthly Expenses Budget**

```bash
dotnet run -- budget --monthly-expenses 1000
```
