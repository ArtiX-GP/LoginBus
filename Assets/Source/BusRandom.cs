using System;

public class BusRandom
{
    private static Random random;
    private static object syncObj = new object();

    public static void InitRandomNumber(int seed) {
        random = new Random(seed);
    }

    public static int GenerateRandomNumber(int max, int min = 0) {
        lock (syncObj) {
            if (random == null)
                random = new Random(); // Or exception...
            return random.Next(min, max);
        }
    }
}
