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
        private Label label;
        private DateTime timerStart;
        private TimeSpan accumulatedTime;
        public TimeSpan max;
        public Canvas seekbar;
        public Canvas seekbarCursor;
        #endregion

        #region Constructors
        private TimeKeeper() { }
        /// <summary>
        /// A class used for easy functionality of the current song timer label
        /// </summary>
        /// <param name="lbl">The label that will be updated</param>
        public TimeKeeper(Label lbl,GUIControlDTO controlDTO)
        {
            if(lbl == null)
            {
                throw new ArgumentException("Label is null");
            }
            label = lbl;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            accumulatedTime = new TimeSpan(0);
            seekbar = controlDTO.seekbar;
            seekbarCursor = controlDTO.seekbarCursorCanvas;
            formatSeekbarCursor();
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
            label.Content = accumulatedTime < max ? accumulatedTime.ToString(@"mm\:ss") : max.ToString(@"mm\:ss");
            setSeekbarCursor(accumulatedTime.TotalSeconds / max.TotalSeconds);

        }

        private void formatSeekbarCursor()
        {
            seekbarCursor.Height = seekbar.Height;
            seekbarCursor.Width = seekbar.Width / 50;
            Canvas.SetLeft(seekbarCursor,Canvas.GetLeft(seekbar));
            Canvas.SetTop(seekbarCursor, Canvas.GetTop(seekbar));
        }

        private void setSeekbarCursor(double percent)
        {
            int x = (int)((seekbar.Width / 50) * 49 * percent);
            Canvas.SetLeft(seekbarCursor, x);
        }
        #endregion

    }
}
