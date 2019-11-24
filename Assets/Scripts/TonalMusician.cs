using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Improv
{
    public abstract class TonalMusician : Musician
    {
        public const int InvalidNoteNumber = -999;
        
        public Sampler sampler;

        protected Chord currentChord;
        
        private List<Note> tmpNotes_ = new List<Note>(16);

        protected virtual void Awake()
        {
            sampler.Init(gameObject);
        }

        public override void SetChord(Chord chord)
        {
            currentChord = chord;
        }
        
        protected void PlayNote(int noteNumber, float timeBars)
        {
            double dspTime = clock.BarsToDspTime(timeBars);
            sampler.Play(noteNumber, dspTime);
        }

        protected int GetNote(Note[] notes, float minStrength)
        {
            tmpNotes_.Clear();
            foreach (var note in notes)
            {
                if (note.strength >= minStrength)
                {
                    tmpNotes_.Add(note);
                }
            }

            if (tmpNotes_.Count == 0)
            {
                return InvalidNoteNumber;
            }

            int idx = Random.Range(0, tmpNotes_.Count);
            return tmpNotes_[idx].noteNumber;
        }

        protected int GetChordNote(float minStrength)
        {
            if (currentChord == null)
            {
                return InvalidNoteNumber;
            }
            return GetNote(currentChord.chordNotes, minStrength);
        }

        protected int GetScaleNote(float minStrength)
        {
            if (currentChord == null)
            {
                return InvalidNoteNumber;
            }

            return GetNote(currentChord.scaleNotes, minStrength);
        }
    }
}