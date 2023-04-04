using System.Collections;
using System.Text;


namespace Groups;

public class Semigroup<T> : IEnumerable<T>
{
    protected HashSet<T> _set;
    
    public HashSet<T> Set => _set;

    public Func<T, T, T> AddFunc;
    public Func<T, T, bool> GEquals;
    public Func<T, T> GetElementCopy;

    public Semigroup(HashSet<T> set, Func<T, T, T> operation, Func<T, T, bool> equals, Func<T, T> copy, bool validate = true)
    {
        _set = set;
        AddFunc = operation;
        GEquals = equals;
        GetElementCopy = copy;
        // !!!При передаче false последним аргументом программа не проверяет на корректность переданные аргументы, могут возникнуть ошибки
        // !!!Передавайте false в качестве последнего аргумента только в том случае, если вы уверены, что данное множество и операция образуют полугруппу
        // При передаче false последним аргументом программа может работать значительно быстрее, т.к. не происходит многочисленных вычислений для множеств с большим количеством элементов
        if (validate && !CheckSemigroup())
            throw new ArgumentException("The given set doesn't form a semigroup under provided operation");
    }
    

    private bool CheckSemigroup()
    {
        if(!CheckAssociativity())
            Console.WriteLine("Assoc");
        
        if(!CheckClosure())
            Console.WriteLine("Closure");
        
        
        return CheckAssociativity() && CheckClosure();
    }
    
    //Мегапроверка
    private bool CheckAssociativity()
    {

        foreach(T a in _set)
            foreach(T b in _set)
                foreach(T c in _set)
                    if (!GEquals(AddFunc(AddFunc(a, b), c), AddFunc(a, AddFunc(b, c))))
                    {
                        Console.WriteLine($"Not associative: ({a} * {b}) * {c} != {a} * ({b} * {c})");
                        return false;
                    }
        return true;
    }

    private bool CheckClosure()
    {
        foreach (T a in _set)
        {
            foreach (T b in _set)
            {
                // Проблема: Contains не сработает, нужно переопределять Equals, GetHashCode
                if (!_set.Contains(AddFunc(a, b)))
                {
                    Console.WriteLine($"Not closed under operation: {AddFunc(a, b)} is not in the set");
                    return false;
                }

                
            }
        }

        return true;
    }
    


    public IEnumerator<T> GetEnumerator()
    {
        return _set.GetEnumerator();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Semigroup (G, *):\n" +
                  "G = {\n  ");
        sb.Append(String.Join(",\n  ", _set));
        sb.Append("\n}");
        return sb.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void ShowCayleyTable(Func<T, string>? func = null)
    {
        if (func is null)
            func = (x) => x.ToString();
        
        int maxLength = -1;
        string[][] lines = new string[_set.Count][];
        List<T> list = _set.ToList();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new string[_set.Count];
            for (int j = 0; j < lines[0].Length; j++)
            {
                lines[i][j] = func(AddFunc(list[i], list[j]));
                maxLength = lines[i][j].Length > maxLength ? lines[i][j].Length : maxLength;
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(new string(' ', maxLength + 1));
        sb.Append("|");
        for (int i = 0; i < lines.Length; i++)
        {
            sb.Append("| " + (func(list[i])).PadRight(maxLength) + " ");
        }
        sb.Append("\n");
        sb.Append(new string('_', (maxLength + 3) * (lines[0].Length + 1)));
        sb.Append("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            sb.Append(func(list[i]).PadRight(maxLength) + " |");
            for (int j = 0; j < lines[0].Length; j++)
            {
                sb.Append("| " + (lines[i][j]).PadRight(maxLength) + " ");
            }
            
            sb.Append(i != lines.Length - 1 ? "\n" + new string(' ', maxLength + 1) + "||" : "");
            if (i != lines.Length - 1)
            {
                for (int j = 0; j < lines[0].Length - 1; j++)
                {
                    sb.Append(new string(' ', maxLength + 2) + "|");
                }

                sb.Append("\n");
            }
        }
        Console.WriteLine(sb);
    }
}