using System;

namespace Improv
{
    [Serializable]
    public class Chord
    {
        public float timeBars;
        public Note[] chordNotes;
        public Note[] scaleNotes;
    }
}