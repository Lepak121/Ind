using System;

public class CinemaHall
{
    public int HallNumber { get; }
    public int Rows { get; }
    public int SeatsPerRow { get; }
    private bool[,] seats;

    public CinemaHall(int hallNumber, int rows, int seatsPerRow)
    {
        HallNumber = hallNumber;
        Rows = rows;
        SeatsPerRow = seatsPerRow;
        seats = new bool[rows, seatsPerRow];
    }

    public bool IsSeatAvailable(int row, int seat)
    {
        return !seats[row, seat];
    }

    public void BookSeat(int row, int seat)
    {
        seats[row, seat] = true;
    }

    public void PrintOccupiedSeats()
    {
        Console.WriteLine("Occupied seats:");
        for (int r = 0; r < Rows; r++)
        {
            for (int s = 0; s < SeatsPerRow; s++)
            {
                if (seats[r, s])
                    Console.Write($"[Row {r + 1}, Seat {s + 1}] ");
            }
        }
        Console.WriteLine();
    }
}
