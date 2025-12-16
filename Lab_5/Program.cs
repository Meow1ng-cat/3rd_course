// See https://aka.ms/new-console-template for more information
using Lab_5;

var track1 = new BasicTrack("Song 1", 100);
var track2 = new BasicTrack("Song 2", 120);

var playlist = new ManagedPlaylist(5); // 5 сек склейки
playlist.Add(track1);
playlist.Add(track2);

Console.WriteLine($"Count: {playlist.Count}"); // 2
Console.WriteLine($"Total duration: {playlist.DurationSec}"); // 100 + 120 + 5 = 225

playlist.Seek(90); // внутри первого трека
Console.WriteLine($"Position after 90s: {playlist.PositionSec}"); // 90

playlist.Seek(20); // переход через границу + склейка
Console.WriteLine($"Position after transition: {playlist.PositionSec}"); // 115 (100 + 5 склейки + 10)