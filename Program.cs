using System.Globalization;
using Cocona;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Parameters;
using ExpenseTracker.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder(args);
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IPrintService, PrintService>();
builder.Services.AddScoped<IStore, JsonStore>();

var app = builder.Build();

var usaCulture = new CultureInfo("en-US");
CultureInfo.CurrentCulture = usaCulture;
CultureInfo.CurrentUICulture = usaCulture;

app.AddCommand("add", async (
    AddCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    Expense expense = new()
    {
        Description = parameters.Description,
        Amount = parameters.Amount
    };
    var result = await expenseService.AddExpense(expense);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success($"Expense added successfully (ID: {result.Value})");
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
        Amount = parameters.Amount
    };
    var result = await expenseService.UpdateExpense(expense);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Success($"Expense updated successfully (ID: {parameters.Id})");
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
    IExpenseService expenseService,
    IPrintService printService) =>
{
    var result = await expenseService.GetExpenses();
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }

    printService.Table(result.Value);
});

app.AddCommand("summary", async (
    SummaryCommandParameter parameters,
    IExpenseService expenseService,
    IPrintService printService) =>
{
    var result = await expenseService.GetTotalExpenses(parameters.Month);
    if (!result.IsSuccess)
    {
        var errorMessage = string.Join(", ", result.Errors);
        printService.Failed(errorMessage);
        return;
    }
    
    printService.Default($"Total expenses: {result.Value:C2}");
});

app.Run();
