# Expense Tracker

A simple app to record and manage your personal expenses. You can add, edit, and delete expenses, and view summaries by category and date.

Project URL: [https://roadmap.sh/projects/expense-tracker](https://roadmap.sh/projects/expense-tracker)

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or higher

## Features

- Add, edit, and delete expenses
- Categorize expenses (e.g., food, transport)
- View total and category-based summaries
- Filter expenses by date
- Simple and clean interface
- Persistent data storage

## Getting Started

**Clone the repository**

```bash
git clone https://github.com/hectorrosario22/ExpenseTracker.git
cd ExpenseTracker
```

**Build the project**

```bash
dotnet build
```

### Usage

```bash
dotnet run -- <command> [arguments]
```

**Example**

```bash
dotnet run -- add --description "Lunch" --amount 20
```

**Sample Output**

```bash
Expense added successfully (ID: 1)
```
