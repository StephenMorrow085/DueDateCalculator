using System;

namespace DueDateCalculatorApp
{
    public class DueDateCalculator
    {
        public static DateTime CalculateDueDate(DateTime submitTime, int turnaroundHours)
        {
            
            // Validate input: Must be within working hours (9 AM - 5 PM) and on a weekday.
            if (submitTime.Hour < 9 || submitTime.Hour >= 17)
            {
                Console.WriteLine("ERROR: Submit time must be between 9 AM and 5 PM.");
                return DateTime.MinValue;
            }
            if (submitTime.DayOfWeek == DayOfWeek.Saturday || submitTime.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("ERROR: Submit time cannot be on a weekend.");
                return DateTime.MinValue;
            }

            DateTime dueDate = submitTime;
            int remainingHours = turnaroundHours;

            while (remainingHours > 0)
            {
                int hoursLeftInDay = 17 - dueDate.Hour; // Hours remaining in workday

                if (remainingHours <= hoursLeftInDay)
                {
                    dueDate = dueDate.AddHours(remainingHours);
                    remainingHours = 0; // All hours used
                }
                else
                {
                    remainingHours -= hoursLeftInDay; // Use up available hours
                    dueDate = dueDate.Date.AddDays(1).AddHours(9); // Move to next day at 9 AM

                    // Skip weekends
                    while (dueDate.DayOfWeek == DayOfWeek.Saturday || dueDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dueDate = dueDate.AddDays(1);
                    }
                }
            }

            return dueDate;
        }
    }
}
