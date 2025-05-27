using System;

public class Showtime
{
    public Movie Movie { get; }
    public DateTime StartTime { get; }
    public CinemaHall Hall { get; }

    public Showtime(Movie movie, DateTime startTime, CinemaHall hall)
    {
        Movie = movie;
        StartTime = startTime;
        Hall = hall;
    }
}
