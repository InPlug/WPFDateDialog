namespace View
{
    /// <summary>
    /// Leeres statisches Singleton, kann für Locking verwendet werden.
    /// </summary>
    /// <remarks>
    /// 05.09.2022 Erik Nagel: created.
    /// </remarks>
    public sealed class LockHelper
    {
        private static LockHelper? instance = null;
        private static readonly object padlock = new object();

        LockHelper()
        {
        }

        /// <summary>
        /// Verwendung: lock(LockHelper.Instance)
        /// </summary>
        public static LockHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LockHelper();
                    }
                    return instance;
                }
            }
        }
    }
}
