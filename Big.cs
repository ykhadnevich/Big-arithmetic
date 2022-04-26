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
    private BigInteger Sub(BigInteger another)
    {
        if (_isPositive && !another._isPositive)
            return Add(new BigInteger(another.ToString().Remove(0, 1)));
        if (!_isPositive && another._isPositive)
            return new BigInteger("-" + another.Add(new BigInteger(ToString().Remove(0, 1))));
        if (!_isPositive && !another._isPositive)
        {
            BigInteger modAnother = new BigInteger(another.ToString().Remove(0, 1));
            BigInteger modFirst = new BigInteger(this.ToString().Remove(0, 1));
            return modAnother.Sub(modFirst);
        }

        string result = "";
        string firstNum;
        string secondNum;
        bool negative = false;
        if (FindBigger(ToString(), another.ToString()) == ToString())
        {
            firstNum = ToString();
            secondNum = another.ToString();
        }
        else
        {
            negative = true;
            firstNum = another.ToString();
            secondNum = ToString();
        }
        if (firstNum.Length > secondNum.Length)
        {
            int difference = firstNum.Length - secondNum.Length;
            for (int i = 0; i < difference; i++)
            {
                secondNum = "0" + secondNum;
            }
        }
        bool toTake = false;
        for (int i = secondNum.Length - 1; i >= 0; i--)
        {
            int takeIndex = i;
            while (toTake)
            {
                if (firstNum[takeIndex] - '0' >= 1)
                {
                    int newDigit = firstNum[takeIndex] - '0' - 1;
                    firstNum = replaceByIndex(firstNum, takeIndex, Convert.ToChar(newDigit.ToString()));
                    toTake = false;
                }
                else
                {
                    firstNum = replaceByIndex(firstNum, takeIndex, '9');
                    takeIndex--;
                }
            }
            int diff;
            if (firstNum[i] - '0' >= secondNum[i] - '0')
            {
                diff = (firstNum[i] - '0') - (secondNum[i] - '0');
                result = diff + result;
            }
            else
            {
                toTake = true;
                string modifiedFirst = "1" + firstNum[i];
                diff = Int32.Parse(modifiedFirst) - (secondNum[i] - '0');
                result = diff + result;
            }
        }

        while (result[0] == '0')
        {
            result = result.Remove(0, 1);
        }
        return negative ? new BigInteger("-" + result) : new BigInteger(result);
    }
    private BigInteger Add(BigInteger another)
    {
        if (_isPositive && !another._isPositive)
            return Sub(new BigInteger(another.ToString().Remove(0, 1)));
        if (!_isPositive && another._isPositive)
            return another.Sub(new BigInteger(this.ToString().Remove(0, 1)));
        if (!_isPositive && !another._isPositive)
        {
            BigInteger modAnother = new BigInteger(another.ToString().Remove(0, 1));
            BigInteger modFirst = new BigInteger(this.ToString().Remove(0, 1));
            return new BigInteger("-" + modFirst.Add(modAnother));
        }
        string result = "";
        string firstNum = ToString();
        string secNum = another.ToString();
        if (firstNum.Length > secNum.Length)
        {
            int difference = firstNum.Length - secNum.Length;
            for (int i = 0; i < difference; i++)
            {
                secNum = "0" + secNum;
            }
        }
        else if (secNum.Length > firstNum.Length)
        {
            int difference = secNum.Length - firstNum.Length;
            for (int i = 0; i < difference; i++)
            {
                firstNum = "0" + firstNum;
            }
        }
        string toCount = "";
        for (int i = secNum.Length - 1; i >= 0; i--)
        {
            if (toCount != "")
            {
                for (int y = i; y >= -1; y--)
                {
                    if (y == -1)
                    {
                        firstNum = '1' + firstNum;
                        secNum = '0' + secNum;
                        i += 1;
                        break;
                    }
                    if (firstNum[y] != '9')
                    {
                        int digit = firstNum[y] - '0' + 1;
                        firstNum = replaceByIndex(firstNum, y, Convert.ToChar(digit.ToString()));
                        break;
                    }
                    
                    {
                        firstNum = replaceByIndex(firstNum, y, '0');
                    }
                }
            }
            toCount = Convert.ToString(firstNum[i] - '0' + secNum[i] - '0');
            result = toCount[^1] + result;
            toCount = toCount.Remove(0, 1);
        }
        if (toCount != "")
        {
            result = '1' + result;
        }
        return new BigInteger(result);
    }
    private string replaceByIndex(string str, int index, char symbol)
    {
        string result = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i != index)
            {
                result += str[i];
            }
            else
            {
                result += symbol;
            }
        }
        return result;
    }

    private string FindBigger(string first1, string second1)
    {
        if (first1.Length > second1.Length)
            return first1;
        else if (second1.Length > first1.Length)
            return second1;
        else
        {
            for (int i = 0; i < first1.Length; i++)
            {
                if (first1[i] - '0' > second1[i] - '0')
                    return first1;
            }
        }
        return second1;
    }
    
}
