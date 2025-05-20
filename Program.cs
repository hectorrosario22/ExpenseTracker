using Cocona;
using ExpenseTracker.Parameters;

var builder = CoconaApp.CreateBuilder(args);
var app = builder.Build();

app.AddCommand("add", (AddCommandParameter parameters) =>
{
    // TODO: Implement the logic to save the expense
    Console.WriteLine("Expense added successfully (ID: 1)");
});

app.Run();
