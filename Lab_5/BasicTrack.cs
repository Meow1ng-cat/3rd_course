using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal class BasicTrack : ITrack
    {
        private string _title;
        private int _durationSec;
        private int _positionSec;

        public BasicTrack(string title, int durationSec)
        {
            _title = title;
            _durationSec = durationSec;
            _positionSec = 0;
        }

        public string Title => _title;
        public int DurationSec => _durationSec;
        public int PositionSec
        {
            get => _positionSec;
            set => _positionSec = Math.Max(0, Math.Min(_durationSec, value));
        }

        public void Seek(int seconds)
        {
            PositionSec = Math.Min(PositionSec + seconds, DurationSec);
        }
    }
}
