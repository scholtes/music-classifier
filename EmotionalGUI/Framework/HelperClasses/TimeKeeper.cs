using Framework.Interfaces;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Framework
{
    /// <summary>
    /// Used to update the currently playing song timer label
    /// </summary>
    public class TimeKeeper
    {
        #region Properties
        private DispatcherTimer timer;
        private IMainForm mainForm;
        private DateTime timerStart;
        private TimeSpan accumulatedTime;
        public TimeSpan max;
        #endregion

        #region Constructors
        private TimeKeeper() { }


        /// <summary>
        /// A class used for easy functionality of the current song timer label
        /// </summary>
        /// <param name="lbl">The label that will be updated</param>
        public TimeKeeper(IMainForm mainform)
        {
            mainForm = mainform;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            accumulatedTime = new TimeSpan(0);
            //formatSeekbarCursor();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            timerStart = DateTime.Now;
            timer.Start();
        }

        /// <summary>
        /// Pauses the timer
        /// </summary>
        public void Pause()
        {
            accumulatedTime += DateTime.Now - timerStart;
            timer.Stop();
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        public void Reset()
        {
            timer.Stop();
            accumulatedTime = new TimeSpan(0);
        }

        /// <summary>
        /// Sets the accumulated time for the song Timer functionality
        /// </summary>
        /// <param name="time"></param>
        public void setAccumulatedTime(TimeSpan time)
        {
            accumulatedTime = time;
            Timer_Tick(null,null);
        }

        /// <summary>
        /// Gets the elasped song time for song Timer functionality
        /// </summary>
        /// <returns></returns>
        public TimeSpan getAccumulatedTime()
        {
            return accumulatedTime;
        }

        // function that actually updates the time
        private void Timer_Tick(object sender, EventArgs e)
        {
            accumulatedTime += DateTime.Now - timerStart;
            timerStart = DateTime.Now;
            mainForm.setTimeLabel(accumulatedTime < max ? accumulatedTime : max);
            mainForm.UpdateCursor(accumulatedTime.TotalSeconds / max.TotalSeconds);
        }

        #endregion

    }
}
