using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class BookingSystem
{
    private List<Movie> Movies = new();
    private List<Showtime> Showtimes = new();

    public void SeedData()
    {
        var movie1 = new Movie("Interstellar", TimeSpan.FromMinutes(169), "Sci-Fi", 8.6);
        var movie2 = new Movie("The Dark Knight", TimeSpan.FromMinutes(152), "Action", 9.0);
        Movies.AddRange(new[] { movie1, movie2 });

        var hall1 = new CinemaHall(1, 5, 5);
        var hall2 = new CinemaHall(2, 4, 4);

        Showtimes.Add(new Showtime(movie1, DateTime.Today.AddHours(18), hall1));
        Showtimes.Add(new Showtime(movie2, DateTime.Today.AddHours(20), hall2));
    }

    public Showtime GetShowtimeByIndex(int index)
    {
        return Showtimes[index];
    }

    public void ShowAllShowtimes()
    {
        for (int i = 0; i < Showtimes.Count; i++)
        {
            var s = Showtimes[i];
            Console.WriteLine($"{i + 1}. {s.Movie.Title} - {s.StartTime} - Hall {s.Hall.HallNumber}");
        }
    }

    public void LoadTicketsFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            // Формат: Title | Time | Hall 1 (або Зала 1) | Row 1 | Seat 1 | Price
            var parts = line.Split('|');
            if (parts.Length < 6)
                continue;

            string movieTitle = parts[0].Trim();
            string timeString = parts[1].Trim();
            string hallStr = parts[2].Trim();
            string rowStr = parts[3].Trim();
            string seatStr = parts[4].Trim();

            if (!DateTime.TryParse(timeString, out DateTime time))
                continue;

            string hallNumberStr = hallStr.Replace("Hall", "").Replace("Зала", "").Trim();
            if (!int.TryParse(hallNumberStr, out int hallNumber))
                continue;

            var rowMatch = Regex.Match(rowStr, @"\d+");
            if (!rowMatch.Success || !int.TryParse(rowMatch.Value, out int row))
                continue;
            row -= 1;

            var seatMatch = Regex.Match(seatStr, @"\d+");
            if (!seatMatch.Success || !int.TryParse(seatMatch.Value, out int seat))
                continue;
            seat -= 1;

            // Тепер знаходиш відповідний сеанс і бронюєш місце:
            foreach (var showtime in Showtimes)
            {
                if (showtime.Movie.Title == movieTitle &&
                    showtime.StartTime == time &&
                    showtime.Hall.HallNumber == hallNumber)
                {
                    showtime.Hall.BookSeat(row, seat);
                    break;
                }
            }
        }
    }


    public void BookTicket(int showtimeIndex, int row, int seat)
    {
        var showtime = Showtimes[showtimeIndex];
        if (showtime.Hall.IsSeatAvailable(row, seat))
        {
            showtime.Hall.BookSeat(row, seat);
            var ticket = new Ticket(showtime, row, seat, 150.0);
            Console.WriteLine("Successfully booked:");
            ticket.Print();

            ticket.SaveToFile("tickets.txt");
        }
        else
        {
            Console.WriteLine("This seat is already taken.");
        }
    }
}
