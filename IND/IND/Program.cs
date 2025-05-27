using System;

class Program
{
    static void Main(string[] args)
    {
        var system = new BookingSystem();              // Створення системи
        system.SeedData();                             // Завантаження тестових даних
        system.LoadTicketsFromFile("tickets.txt"); // ⬅ Завантаження раніше заброньованих місць

        Console.WriteLine("Вiтаємо у системi бронювання кiно!");

        while (true)
        {
            system.ShowAllShowtimes();
            Console.WriteLine("Виберiть номер сеансу (або 0 для виходу): ");

            if (!int.TryParse(Console.ReadLine(), out int showtimeChoice) || showtimeChoice == 0)
                break;

            var selectedShowtime = system.GetShowtimeByIndex(showtimeChoice - 1);
            var maxRow = selectedShowtime.Hall.Rows;
            var maxSeat = selectedShowtime.Hall.SeatsPerRow;

            Console.WriteLine($"Максимальний ряд: {maxRow}");
            Console.WriteLine($"Максимальна кiлькiсть мiсць у ряду: {maxSeat}");

            selectedShowtime.Hall.PrintOccupiedSeats();  // Показати зайняті місця

            Console.Write("Введiть номер ряду: ");
            if (!int.TryParse(Console.ReadLine(), out int row) || row < 1 || row > maxRow)
            {
                Console.WriteLine("Некоректний номер ряду.");
                continue;
            }

            Console.Write("Введiть номер мiсця: ");
            if (!int.TryParse(Console.ReadLine(), out int seat) || seat < 1 || seat > maxSeat)
            {
                Console.WriteLine("Некоректний номер мiсця.");
                continue;
            }

            system.BookTicket(showtimeChoice - 1, row - 1, seat - 1);
            Console.WriteLine();
        }

        Console.WriteLine("Дякуємо! Гарного перегляду!");
    }
}
