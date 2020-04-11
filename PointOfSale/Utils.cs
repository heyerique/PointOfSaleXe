using System;
namespace PointOfSale
{
    public static class Utils
    {
        public static void DoSafe(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
