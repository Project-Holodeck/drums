namespace Scheduler {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using MidiParser;

    public struct DrumEvent {
        int time;
        int drum;
        public DrumEvent(int time, int drum) {
            this.time = time;
            this.drum = drum;
        }
    }
}