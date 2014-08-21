namespace Tanpohp.Utils
{
    public static class Swap
    {
        /// <summary>
        /// Swaps two references.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="instanceOne">First instance.</param>
        /// <param name="instanceTwo">Second instance.</param>
        public static void Perform<T>(ref T instanceOne, ref T instanceTwo)
        {
            var tmpTwo = instanceTwo;
            instanceTwo = instanceOne;
            instanceOne = tmpTwo;
        }
    }
}
