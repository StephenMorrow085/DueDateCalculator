using NUnit.Framework;
using System;

namespace DueDateCalculator.Tests
{
    [TestFixture]
    public class DueDateCalculatorTests
    {
        // Test for submitting a time within the same day during work hours
        [Test]
        public void CalculateDueDate_SameDayWithinWorkHours_ReturnsCorrectTime()
        {
            var submitTime = new DateTime(2025, 4, 14, 10, 0, 0); // Monday 10:00 AM
            int turnaroundHours = 2;

            var expected = new DateTime(2025, 4, 14, 12, 0, 0); // Monday 12:00 PM
            var result = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test for submitting a time that overflows work hours into the next day
        [Test]
        public void CalculateDueDate_OverflowWorkHours_LandsNextDayProperly()
        {
            var submitTime = new DateTime(2025, 4, 14, 14, 0, 0); // Monday 2:00 PM
            int turnaroundHours = 6;

            var expected = new DateTime(2025, 4, 15, 12, 0, 0); // Tuesday 12:00 PM
            var result = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test for submitting a time that spans the weekend
        [Test]
        public void CalculateDueDate_SpansWeekend_SkipsToMonday()
        {
            var submitTime = new DateTime(2025, 4, 11, 15, 0, 0); // Friday 3:00 PM
            int turnaroundHours = 10;

            var expected = new DateTime(2025, 4, 14, 17, 0, 0); // Expected due date: Monday 9:05 AM
            var result = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);

            Assert.That(result, Is.EqualTo(expected));
        }

        // Test for submitting a time before work hours
        [Test]
        public void CalculateDueDate_SubmitTimeBeforeWorkHours_ReturnsMinValue()
        {
            var submitTime = new DateTime(2025, 4, 14, 8, 59, 59); // Before 9 AM
            int turnaroundHours = 1;

            var result = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);

            // Log a message indicating the submit time is before work hours
            if (result == DateTime.MinValue)
            {
                TestContext.Progress.WriteLine($"[INFO] Submit time {submitTime} is before work hours (9 AM).");
            }

            Assert.That(result, Is.EqualTo(DateTime.MinValue));
        }

        // Test for submitting a time after work hours
        [Test]
        public void CalculateDueDate_SubmitTimeAfterWorkHours_ReturnsMinValue()
        {
            var submitTime = new DateTime(2025, 4, 14, 17, 1, 0); // After 5 PM
            int turnaroundHours = 1;

            var result = DueDateCalculatorApp.DueDateCalculator.CalculateDueDate(submitTime, turnaroundHours);

            // Log a message indicating the submit time is after work hours
            if (result == DateTime.MinValue)
            {
                TestContext.Progress.WriteLine($"[INFO] Submit time {submitTime} is after work hours (5 PM).");
            }

            Assert.That(result, Is.EqualTo(DateTime.MinValue));
        }
    }
}
