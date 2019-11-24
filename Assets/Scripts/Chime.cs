using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Improv
{
    public class Chime : TonalMusician
    {
        public enum Mode
        {
            Up,
            Down,
            Random
        }

        public Mode mode;
        public int octaveShift;
        
        private int sequenceIdx_;

        public override void UpdateNotes(float timeBars)
        {
            // set the step size based on intensity
            int stepSize;
            if (intensity < 0.4f)
            {
                stepSize = 4;
            }
            else if (intensity < 0.7f)
            {
                stepSize = 8;
            }
            else
            {
                stepSize = 16;
            }
            
            
            // find out the positions based on the step size
            int lastStep = Mathf.FloorToInt(lastTimeBars * stepSize) % stepSize;
            int thisStep = Mathf.FloorToInt(timeBars * stepSize) % stepSize;
            lastTimeBars = timeBars;

            // skip if we haven't advanced
            if (lastStep == thisStep)
            {
                return;
            }
            
            // find the note time (in bars)
            int wholeBar = (int) timeBars;
            float barFraction = thisStep / (float) stepSize;
            float noteTime = wholeBar + barFraction;
            
            // get the index within the sequence
            int numChordNotes = currentChord.chordNotes.Length;
            int chordNoteIdx;
            switch (mode)
            {
                case Mode.Up:
                    sequenceIdx_ = (sequenceIdx_ + 1) % numChordNotes;
                    chordNoteIdx = sequenceIdx_;
                    break;
                case Mode.Down:
                    sequenceIdx_ = (sequenceIdx_ + 1) % numChordNotes;
                    chordNoteIdx = numChordNotes - (sequenceIdx_ + 1);
                    break;
                case Mode.Random:
                    chordNoteIdx = Random.Range(0, numChordNotes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            int note = currentChord.chordNotes[chordNoteIdx].noteNumber + octaveShift * 12;
            PlayNote(note, noteTime);
        }
    }
}