using System.Threading;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        /// <summary>
        /// Thread pause
        /// </summary>
        /// <param name="timeToSleep">Thead time sleep, ms</param>
        public static void Pause(int timeToSleep= 3000)
        {
            Thread.Sleep(timeToSleep);
        }
    }
}
