using System;

public static class Utils
{
    //得到枚举中的随机值
    public static T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(MyRandom.Instance.NextInt(values.Length));
    }
}