namespace ConsoleApp4;

static class Karatsuba
{
    public static string Multiply(string first, string second)
    {
        bool isNegative = first[0] == '-' ^ second[0] == '-';
        if (first[0] == '-' || second[0] == '-')
        {
            first = first.Replace("-", "");
            second = second.Replace("-", "");
        }
            
        if (first.Length == 1 || second.Length == 1)
            return (int.Parse(first) * int.Parse(second)).ToString();
        int cutPos = getCutPosition(first, second);
        string a = getFirstPart(first, cutPos);
        string b = getSecondPart(first, cutPos);
        string c = getFirstPart(second, cutPos);
        string d = getSecondPart(second, cutPos);
        string ac = Multiply(a, c);
        string bd = Multiply(b, d);
        string abcd = Multiply(StringAdd(a, b), StringAdd(c, d));
        return (isNegative ? "-" : "") + CalculateResult(ac, bd, abcd, b.Length + d.Length);
    }
    static string  getFirstPart(string str, int cutPos)
    {
        return str.Remove(str.Length - cutPos);
    }

    static string getSecondPart(string str, int cutPos)
    {
        return str.Substring(str.Length - cutPos);
    }

    static int getCutPosition(string first, string second)
    {
        int min = Math.Min(first.Length, second.Length);
        if (min == 1)
            return 1;
        if (min % 2 == 0)
            return min / 2;
        return min / 2 + 1;
    }
    static string CalculateResult(string ac, string bd, string abcd, int padding)
    {
        string term0 = StringSub(StringSub(abcd, ac), bd);
        string term1 = term0.PadRight(term0.Length + padding / 2, '0');
        string term2 = ac.PadRight(ac.Length + padding, '0');
        string result = StringAdd(StringAdd(term1, term2), bd);
        return result;
    }

   

    static string StringAdd(string a, string b)
    {
        string result = "";

        if (a.Length > b.Length)
            Swap(ref a, ref b);

        a = a.PadLeft(b.Length, '0');
        int length = a.Length;
        int carry = 0;
        for (int i = length - 1; i >= 0; i--)
        {
            int num1 = int.Parse(a.Substring(i, 1));
            int num2 = int.Parse(b.Substring(i, 1));
            var res = (num1 + num2 + carry) % 10;
            carry = (num1 + num2 + carry) / 10;
            result = result.Insert(0, res.ToString());
        }
        if (carry != 0)
            result = result.Insert(0, carry.ToString());
        return SanitizeResult(result);
    }

    static string StringSub(string a, string b)
    {
        bool resultNegative = false;
        string result = "";

        if (StringIsSmaller(a, b))
        {
            Swap(ref a, ref b);
            resultNegative = true;
        }
        b = b.PadLeft(a.Length, '0');
        int length = a.Length;
        int carry = 0;
        for (int i = length - 1; i >= 0; i--)
        {
            bool nextCarry = false;
            int num1 = int.Parse(a.Substring(i, 1));
            int num2 = int.Parse(b.Substring(i, 1));
            if (num1 - carry < num2)
            {
                num1 += 10;
                nextCarry = true;
            }
            var res = num1 - num2 - carry;
            result = result.Insert(0, res.ToString());
            carry = nextCarry ? 1 : 0;
        }
        result = SanitizeResult(result);
        if (resultNegative)
            return result.Insert(0, "-");
        return result;
    }

    static bool StringIsSmaller(string a, string b)
    {
        if (a.Length < b.Length)
            return true;
        if (a.Length > b.Length)
            return false;
        char[] arrayA = a.ToCharArray();
        char[] arrayB = b.ToCharArray();
        for (int i = 0; i < arrayA.Length; i++)
        {
            if (arrayA[i] < arrayB[i])
                return true;
            if (arrayA[i] > arrayB[i])
                return false;
        }
        return false;
    }

    static void Swap(ref string a, ref string b)
    {
        (a, b) = (b, a);
    }

    static string SanitizeResult(string result)
    {
        result = result.TrimStart(new[] { '0' });
        if (result.Length == 0)
            result = "0";
        return result;
    }
}
