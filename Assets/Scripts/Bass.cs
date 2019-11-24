using UnityEngine;

namespace Improv
{
    public class Bass : TonalMusician
    {
        public override void UpdateNotes(float timeBars)
        {
            // find out the positions in 16th notes
            int last16th = Mathf.FloorToInt(lastTimeBars * 16) % 16;
            int this16th = Mathf.FloorToInt(timeBars * 16) % 16;
            lastTimeBars = timeBars;

            // skip if we haven't advanced
            if (last16th == this16th)
            {
                return;
            }

            // find the note time (in bars)
            int wholeBar = (int) timeBars;
            float barFraction = this16th / 16.0f;
            float noteTime = wholeBar + barFraction;

            // play roots on the downbeat
            if (this16th == 0)
            {
                var note = GetChordNote(1.0f);
                if (note != InvalidNoteNumber)
                {
                    PlayNote(note, noteTime);
                }

                return;
            }
            
            // usually play strong tones on the other quarters
            if (this16th % 4 == 0)
            {
                if (Random.value < intensity)
                {
                    var note = GetChordNote(0.5f);
                    if (note != InvalidNoteNumber)
                    {
                        PlayNote(note, noteTime);
                    }
                }

                return;
            }
            
            // sometimes play on the 8ths
            if (this16th % 2 == 0)
            {
                if (Random.value < intensity - 0.25f)
                {
                    var note = GetChordNote(0.25f);
                    if (note != InvalidNoteNumber)
                    {
                        PlayNote(note, noteTime);
                    }
                }
                return;
            }

            // sometimes play on the 16ths
            if (Random.value < intensity - 0.5f)
            {
                var note = GetChordNote(0);
                if (note != InvalidNoteNumber)
                {
                    PlayNote(note, noteTime);
                }
            }
        }
    }
}