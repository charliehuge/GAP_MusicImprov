using UnityEngine;

namespace Improv
{
    public class Solo : TonalMusician
    {
        public int recordBars = 4;
        public int repeats = 1;

        private int[] recordedMelody_;
        private float repeatStartBar_ = -999;
        
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
            
            // get the index within the recording
            int recordingIdx = this16th + (wholeBar % recordBars) * 16;

            // repeat the recording until the next time we should generate a melody
            float repeatEndBars = repeatStartBar_ + repeats * recordBars;
            if (timeBars < repeatEndBars)
            {
                int note = recordedMelody_[recordingIdx];
                if (note != InvalidNoteNumber)
                {
                    PlayNote(note, noteTime);
                }
            }
            // if we're recording, generate the melody
            else
            {
                if (recordingIdx == 0)
                {
                    recordedMelody_ = new int[recordBars * 16];
                }

                int note = InvalidNoteNumber;
                
                // always play a strong note on the downbeat
                if (this16th == 0)
                {
                    note = GetScaleNote(1.0f);
                }
                // usually play a note on the quarters
                else if (this16th % 4 == 0)
                {
                    if (Random.value < intensity)
                    {
                        note = GetScaleNote(0.5f);
                    }
                }
                // sometimes play a note on the eighths
                else if (this16th % 2 == 0)
                {
                    if (Random.value < intensity - 0.25f)
                    {
                        note = GetScaleNote(0.25f);
                    }
                }
                else if (Random.value < intensity - 0.5f)
                {
                    note = GetScaleNote(0.0f);
                }

                // record and play the note
                recordedMelody_[recordingIdx] = note;
                if (note != InvalidNoteNumber)
                {
                    PlayNote(note, noteTime);
                }

                // if we're done recording, start repeating
                int lastRecordingIdx = recordBars * 16 - 1;
                if (recordingIdx >= lastRecordingIdx)
                {
                    repeatStartBar_ = Mathf.Ceil(timeBars);
                }
            }
        }
    }
}