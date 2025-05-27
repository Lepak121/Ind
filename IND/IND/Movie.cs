public class Movie
{
    public string Title { get; }
    public TimeSpan Duration { get; }
    public string Genre { get; }
    public double Rating { get; }

    public Movie(string title, TimeSpan duration, string genre, double rating)
    {
        Title = title;
        Duration = duration;
        Genre = genre;
        Rating = rating;
    }
}
