namespace Lab_5;

public interface IPlaylist
{
    int Count { get; }
    void Add(ITrack track);
}