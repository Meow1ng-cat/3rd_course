namespace Lab_5;

public interface ITrack
{
    string Title { get; }
    int DurationSec { get; }
    int PositionSec { get; set; }
    void Seek(int seconds);
}