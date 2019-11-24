using System;
using System.Collections.Generic;
using UnityEngine;

namespace Improv
{
    public class Drummer : Musician
    {
        public Sampler kick;
        public Sampler snare;
        public Sampler hat;

        private Note[] kickNotes = new Note[16];
        private Note[] snareNotes = new Note[16];
        private Note[] hatNotes = new Note[16];

        private void Awake()
        {
            kick.Init(gameObject);
            snare.Init(gameObject);
            hat.Init(gameObject);
            // generate a one-bar 4/4 beat and a fill to use
            // you could also create an editor for this
            float[] kickStrengths =
            {
                1, 0, 0, 0.1f,
                0.4f, 0, 0.4f, 0,
                0.9f, 0, 0, 0.2f,
                0.5f, 0, 0.3f, 0
            };
            for (int i = 0; i < kickNotes.Length; ++i)
            {
                var note = new Note();
                note.noteNumber = 60;
                note.strength = kickStrengths[i];
                kickNotes[i] = note;
            }
            float[] snareStrengths =
            {
                0, 0.1f, 0, 0.2f,
                1, 0, 0.5f, 0,
                0, 0.2f, 0, 0.2f,
                1, 0, 0.25f, 0.1f
            };
            for (int i = 0; i < snareNotes.Length; ++i)
            {
                var note = new Note();
                note.noteNumber = 60;
                note.strength = snareStrengths[i];
                snareNotes[i] = note;
            }         
            float[] hatThresholds =
            {
                1, 0.1f, 0.9f, 0.3f,
                1, 0.5f, 0.7f, 0.4f,
                1, 0.2f, 0.8f, 0.3f,
                1, 0.4f, 0.7f, 0.1f
            };
            for (int i = 0; i < hatNotes.Length; ++i)
            {
                var note = new Note();
                note.noteNumber = 60;
                note.strength = hatThresholds[i];
                hatNotes[i] = note;
            }     
        }
        
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

            // find the hit time (in audio engine time)
            int wholeBar = (int) timeBars;
            float barFraction = this16th / 16.0f;
            double hitTime = clock.BarsToDspTime(wholeBar + barFraction);

            // find the notes that are in this index and play them if they're at the right level
            float minStrength = 1.0f - intensity;
            Note tmpNote = kickNotes[this16th];
            if (tmpNote.strength >= minStrength)
            {
                kick.Play(tmpNote.noteNumber, hitTime);
            }

            tmpNote = snareNotes[this16th];
            if (tmpNote.strength >= minStrength)
            {
                snare.Play(tmpNote.noteNumber, hitTime);
            }

            tmpNote = hatNotes[this16th];
            if (tmpNote.strength >= minStrength)
            {
                hat.Play(tmpNote.noteNumber, hitTime);
            }
        }
    }
}