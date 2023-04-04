using System.Collections;
using System.Text;

namespace Groups;

public class MultiGroup<T1, T2, T3> : IEnumerable<(T1, T2, T3)>
{
    private Group<T1> _g1;
    private Group<T2> _g2;
    private Group<T3> _g3;

    public MultiGroup(Group<T1> a, Group<T2> b, Group<T3> c)
    {
        _g1 = a;
        _g2 = b;
        _g3 = c;
    }

    public int GetElementOrder((T1, T2, T3) el)
    {
        int order = 1;
        (T1, T2, T3) t = (_g1.GetElementCopy(el.Item1), _g2.GetElementCopy(el.Item2), _g3.GetElementCopy(el.Item3));
        while (!(_g1.GEquals(t.Item1, _g1.Id) && _g2.GEquals(t.Item2, _g2.Id) && _g3.GEquals(t.Item3, _g3.Id)))
        {
            t.Item1 = _g1.Add(t.Item1, el.Item1) ?? throw new InvalidOperationException();
            t.Item2 = _g2.Add(t.Item2, el.Item2) ?? throw new InvalidOperationException();
            t.Item3 = _g3.Add(t.Item3, el.Item3) ?? throw new InvalidOperationException();
            order++;
        }

        return order;
    }

    public IEnumerator<(T1, T2, T3)> GetEnumerator()
    {
        return (from g1 in _g1
            from g2 in _g2
            from g3 in _g3
            select (g1, g2, g3)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var x in this)
        {
            sb.Append(string.Join(",\t", x));
            sb.Append("\n");
        }

        return sb.ToString();
    }
}