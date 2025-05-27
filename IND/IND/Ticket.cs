using System;
using System.IO;

public class Ticket
{
    public Showtime Showtime { get; }
    public int Row { get; }
    public int Seat { get; }
    public double Price { get; }

    public Ticket(Showtime showtime, int row, int seat, double price)
    {
        Showtime = showtime;
        Row = row;
        Seat = seat;
        Price = price;
    }

    public void Print()
    {
        Console.WriteLine($"Movie: {Showtime.Movie.Title}, Time: {Showtime.StartTime}, Hall: {Showtime.Hall.HallNumber}, Row: {Row + 1}, Seat: {Seat + 1}, Price: {Price} UAH");
    }

    public void SaveToFile(string filePath)
    {
        string line = $"{Showtime.Movie.Title} | {Showtime.StartTime} | Hall {Showtime.Hall.HallNumber} | Row {Row + 1} | Seat {Seat + 1} | {Price}";
        File.AppendAllText(filePath, line + Environment.NewLine);
    }
}
