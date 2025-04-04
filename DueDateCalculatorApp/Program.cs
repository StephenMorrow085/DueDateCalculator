using System;
using DueDateCalculatorApp; // Ensure to include the namespace of the DueDateCalculator class


class Program
{
    static void Main()
    {
        // Example submission times
        // var submitTime = new DateTime(2025, 4, 14, 10, 0, 0); // Returns properly.
        // var submitTime = new DateTime(2025, 4, 11, 10, 0, 0); // Returns properly.
        var submitTime = new DateTime(2025, 4, 11, 08, 0, 0); // Returns with an error.

        int turnaroundHours = 16;

        var dueDate = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);
        if (dueDate == DateTime.MinValue)
        {
            Console.WriteLine("Invalid submit time!");
        }
        else
        {
            Console.WriteLine($"Due date: {dueDate}");
        }
    }
}
