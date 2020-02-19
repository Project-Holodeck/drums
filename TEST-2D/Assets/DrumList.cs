namespace Scheduler {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using MidiParser;

    public struct DrumEvent : IComparable<DrumEvent> {
        public double time;
        public int drum;

        public DrumEvent(double time, int drum) {
            this.time = time;
            this.drum = drum;
        }
        
        public int CompareTo(DrumEvent otherEvent)
        {
            return this.time.CompareTo(otherEvent.time);
        }

        public int Compare(DrumEvent event1, DrumEvent event2)
        {
            return event1.time.CompareTo(event2.time);
        }
    }
}