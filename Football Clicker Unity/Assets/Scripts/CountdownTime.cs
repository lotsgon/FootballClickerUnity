public class CountdownTime
{
    public int Hours {get; private set ;}
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    public CountdownTime(float seconds)
    {
        Hours = (int)seconds / 3600;
        Minutes = (int)(seconds % 3600) / 60;
        Seconds = (int)(seconds % 3600) % 60;
    }
}
