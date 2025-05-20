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

app.Run();
