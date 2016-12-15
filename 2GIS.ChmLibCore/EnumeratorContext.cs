namespace CHMsharp
{
    using System;

    /// <summary>
    /// The enumerator context.
    /// </summary>
    public class EnumeratorContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether is goal reached.
        /// </summary>
        public bool IsGoalReached { get; set; }

        /// <summary>
        /// Gets or sets the enumerate exception.
        /// </summary>
        public Exception EnumerateException { get; set; }
    }
}
