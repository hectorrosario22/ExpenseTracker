﻿using System.Globalization;
using Cocona;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Parameters;
using ExpenseTracker.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder(args);
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped<IPrintService, PrintService>();
builder.Services.AddScoped<IStore, JsonStore>();

var app = builder.Build();

var usaCulture = new CultureInfo("en-US");
CultureInfo.CurrentCulture = usaCulture;
CultureInfo.CurrentUICulture = usaCulture;

app.AddCommand("budget", async (
    BudgetCommandParameter parameters,
    IBudgetService budgetService,
    IPrintService printService) =>
{
    Budget budget = new()
    {
        MonthlyExpenses = parameters.MonthlyExpenses,
    };
    var result = await budgetService.UpdateBudget(budget);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success("Budget updated successfully");
});

app.AddCommand("add", async (
    AddCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    Expense expense = new()
    {
        Description = parameters.Description,
        Amount = parameters.Amount,
        Categories = [.. parameters.Categories],
    };
    var result = await expenseService.AddExpense(expense);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success($"Expense added successfully (ID: {result.Value})");
    if (!string.IsNullOrWhiteSpace(result.SuccessMessage))
    {
        printService.Warning(result.SuccessMessage);
    }
});

app.AddCommand("update", async (
    UpdateCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    Expense expense = new()
    {
        Id = parameters.Id,
        Description = parameters.Description,
        Amount = parameters.Amount,
        Categories = [.. parameters.Categories],
    };
    var result = await expenseService.UpdateExpense(expense);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success($"Expense updated successfully (ID: {parameters.Id})");
    if (!string.IsNullOrWhiteSpace(result.SuccessMessage))
    {
        printService.Warning(result.SuccessMessage);
    }
});

app.AddCommand("delete", async (
    DeleteCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    var result = await expenseService.DeleteExpense(parameters.Id);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success($"Expense deleted successfully (ID: {parameters.Id})");
});

app.AddCommand("list", async (
    QueryCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    var result = await expenseService.GetExpenses(parameters.Month, parameters.Categories);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Table(result.Value);
});

app.AddCommand("summary", async (
    QueryCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    var result = await expenseService.GetTotalExpenses(parameters.Month, parameters.Categories);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Default($"Total expenses: {result.Value:C2}");
});

app.AddCommand("csv", async (
    QueryCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService,
    IExportService exportService) =>
{
    var result = await expenseService.GetExpenses(parameters.Month, parameters.Categories);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    var filePath = await exportService.ToCsv(result.Value);
    printService.Success($"Expenses exported successfully to '{filePath}'");
});

app.Run();
