﻿namespace Groups;
public class Cycle
{
    private int[] _arr;
    private int _maxEl;

    public int GetEl(int ind) => _arr[ind];
    public Cycle(params int[] a)
    {
        _arr = a;
        _maxEl = -1;
        foreach (int x in a)
            if (x > _maxEl)
                _maxEl = x;
    }

    public int GetMax() => _maxEl;

    public Permutation ToPermutation()
    {
        return ToPermutation(this);
    }

    public static bool TryParse(string s, out Cycle c)
    {
        if (s.Length == 0)
        {
            c = new Cycle(new int[]{});
            return false;
        }

        try
        {
            c = Parse(s);
            return true;
        }
        catch (Exception)
        {
            c = new Cycle(new int[]{});
            return false;
        }
    }

    public static Cycle Parse(string s)
    {
        // переделать чтобы было со скобками
        string[] k = s.Split(" ");
        int[] a = new int[k.Length];
        for (int i = 0; i < a.Length; i++)
        {
            a[i] = int.Parse(k[i]);
        }

        return new Cycle(a);
    }

    // public int GetOrder()
    // {
    //     return ToPermutation(this).GetOrder();
    // }

    public static Permutation ToPermutation(Cycle c, int len)
    {
        if (len < c._maxEl)
            throw new Exception();
        
        int[] a = new int[len];
        for (int i = 0; i < c._arr.Length; i++)
        {
            if (i == c._arr.Length - 1)
            {
                a[c._arr[i] - 1] = c._arr[0];
                break;
            }

            a[c._arr[i] - 1] = c._arr[i + 1];
        }

        for (int i = 0; i < len; i++)
        {
            if (a[i] != 0)
                continue;
            a[i] = i + 1;
        }

        return new Permutation(a);
    }


    public static Permutation ToPermutation(Cycle c)
    {
        return ToPermutation(c, c._maxEl);
    }

    public static Permutation operator *(Cycle a, Cycle b)
    {
        return ToPermutation(a, Math.Max(a._maxEl, b._maxEl)) * ToPermutation(b, Math.Max(a._maxEl, b._maxEl));
    }

    public override string ToString()
    {
        return "(" + String.Join(" ", _arr) + ")";
    }
}