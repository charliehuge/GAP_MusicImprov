using System;
using UnityEngine;

namespace Improv
{
    [Serializable]
    public class Clock
    {
        public float tempo = 120.0f;
        public float lookaheadSeconds = 0.1f;

        private double startTime_;

        public float TimeBars
        {
            get
            {
                if (!Playing)
                {
                    return 0;
                }

                double timeSeconds = lookaheadSeconds + AudioSettings.dspTime - startTime_;
                double timeQuarters = (tempo / 60) * timeSeconds;
                // stick to 4/4 time for this example
                return (float) (timeQuarters / 4);
            }
        }

        public double BarsToDspTime(float bars)
        {
            float quarters = bars * 4;
            float seconds = quarters / (tempo / 60);
            return startTime_ + seconds;
        }

        public bool Playing { get; private set; }

        public void Play()
        {
            if (Playing)
            {
                return;
            }

            Playing = true;
            startTime_ = AudioSettings.dspTime;
        }

        public void Stop()
        {
            Playing = false;
        }
    }
}