using System;
using System.Collections.Generic;

namespace Superscrot
{
    /// <summary>
    /// Provides information about taken screenshots.
    /// </summary>
    public class History
    {
        private List<Screenshot> _history;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public History()
        {
            _history = new List<Screenshot>();
        }

        /// <summary>
        /// Gets the number of screenshots taken.
        /// </summary>
        public int Count
        {
            get { return _history.Count; }
        }

        /// <summary>
        /// Gets information about the last screenshot.
        /// </summary>
        /// <returns>
        /// A <see cref="Superscrot.Screenshot"/> object that holds information
        /// about the last screenshot.
        /// </returns>
        public Screenshot Peek()
        {
            if (Count == 0) return null;

            Screenshot lastScreenshot = _history[0];
            return lastScreenshot;
        }

        /// <summary>
        /// Gets information about the last screenshot, and removes it from the list.
        /// </summary>
        /// <returns>
        /// A <see cref="Superscrot.Screenshot"/> object that holds information
        /// about the last screenshot.
        /// </returns>
        public Screenshot Pop()
        {
            if (Count == 0) return null;

            Screenshot lastScreenshot = Peek();
            _history.RemoveAt(0);
            return lastScreenshot;
        }

        /// <summary>
        /// Adds information about a taken screenshot.
        /// </summary>
        /// <param name="screenshot">
        /// The <see cref="Superscrot.Screenshot"/> to add.
        /// </param>
        public void Push(Screenshot screenshot)
        {
            _history.Insert(0, screenshot);
        }
    }
}
