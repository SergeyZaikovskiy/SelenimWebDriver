namespace CreditCards.UITests
{
    using System.Threading;

    internal static class DemoHelper
    {
        /// <summary>
        /// Thread pause
        /// </summary>
        /// <param name="timeToSleep">Thead time sleep, ms</param>
        public static void Pause(int timeToSleep= 1500)
        {
            Thread.Sleep(timeToSleep);
        }
    }
}
