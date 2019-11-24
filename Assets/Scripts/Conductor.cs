using System;
using UnityEngine;

namespace Improv
{
    public class Conductor : MonoBehaviour
    {
        public bool play;
        public Clock clock = new Clock();
        public Chord[] chords;
        public float chordLengthBars = 4;
        
        private Musician[] musicians;

        private void Awake()
        {
            musicians = GetComponentsInChildren<Musician>();
            foreach (var musician in musicians)
            {
                musician.SetClock(clock);
            }
        }

        private void Update()
        {
            if (play != clock.Playing)
            {
                if (play)
                {
                    clock.Play();
                }
                else
                {
                    clock.Stop();
                }
            }
            
            if (clock.Playing)
            {
                float timeBars = clock.TimeBars;
                
                // get the current chord
                float chordTimeBars = timeBars % chordLengthBars;
                int chordIdx = -1;
                for (int i = 0; i < chords.Length; ++i)
                {
                    if (chords[i].timeBars > chordTimeBars)
                    {
                        break;
                    }

                    chordIdx = i;
                }

                Chord currentChord = null;
                if (chordIdx >= 0)
                {
                    currentChord = chords[chordIdx];
                }
                
                // Update the musicians
                foreach (var musician in musicians)
                {
                    musician.SetChord(currentChord);
                    musician.UpdateNotes(timeBars);
                }
            }
        }
    }
}
