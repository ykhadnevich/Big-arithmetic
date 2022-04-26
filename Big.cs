namespace ConsoleApp4;

public class BigInteger
{
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    private readonly int[] _numbers;
    private readonly bool _isPositive  = true;

    
    public override string ToString()
    {
        string value = "";
        if (!_isPositive)
        {
            value = value + "-";
        }
        foreach (var i in _numbers)
        {
            value = value + (char)i;
        }
        return value;
    }
    public BigInteger(string value)
    {
        if (value[0] == '-')
        {
            _isPositive = false;
            value = value.Remove(0, 1);
        }
        _numbers = new int[value.Length];
        for(int i = 0; i < value.Length; i++)
        {
            _numbers[i] = value[i];
        }
    }
}
