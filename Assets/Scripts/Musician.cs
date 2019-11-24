using UnityEngine;

namespace Improv
{
    public abstract class Musician: MonoBehaviour
    {
        [Range(0.0f, 1.0f)] public float intensity;
        
        protected Clock clock;
        protected float lastTimeBars = -0.25f;

        public void SetClock(Clock clock)
        {
            this.clock = clock;
        }

        public abstract void UpdateNotes(float timeBars);
        public virtual void SetChord(Chord chord) {}

        // helpers to convert to bars
        protected static float Quarter(int index)
        {
            return index / 4.0f;
        }

        protected static float Eighth(int index)
        {
            return index / 8.0f;
        }

        protected static float Sixteenth(int index)
        {
            return index / 16.0f;
        }
    }
}