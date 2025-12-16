using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class ManagedPlaylist : ITrack, IPlaylist
    {
        private List<ITrack> _tracks = new();
        private int _crossfadeSec;
        private int _positionSec;

        public ManagedPlaylist(int crossfadeSec)
        {
            _crossfadeSec = crossfadeSec;
            _positionSec = 0;
        }

        public string Title => "Playlist";
        public int DurationSec => CalculateTotalDuration();
        public int PositionSec
        {
            get => _positionSec;
            set => _positionSec = Math.Max(0, Math.Min(DurationSec, value));
        }

        public int Count => _tracks.Count;

        public void Add(ITrack track)
        {
            _tracks.Add(track);
        }

        public void Seek(int seconds)
        {
            _positionSec = Math.Min(_positionSec + seconds, DurationSec);
        }

        private int CalculateTotalDuration()
        {
            if (_tracks.Count == 0) return 0;

            int total = 0;
            for (int i = 0; i < _tracks.Count; i++)
            {
                total += _tracks[i].DurationSec;
                if (i < _tracks.Count - 1) // склейка между треками кроме последнего
                    total += _crossfadeSec;
            }
            return total;
        }
    }
}
