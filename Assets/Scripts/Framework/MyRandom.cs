using System;

public class MyRandom
{
    private Random random;
    private static MyRandom instance;
    public static MyRandom Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    public MyRandom()
    {
        random = new();
    }

    public int NextInt()
    {
        return random.Next();
    }

    public int NextInt(int min, int max)
    {
        return random.Next(min, max);
    }

    public float NextFloat()
    {
        return (float)random.NextDouble();
    }

    public float NextFloat(float min, float max)
    {
        return (float)(min + (random.NextDouble() * (max - min)));
    }
    public bool NextBool(){
        return random.Next(0, 2)==0;
    }
}