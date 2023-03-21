using System.Collections;
using System.Text;

namespace Groups;

/// <summary>
/// Класс, предстваляющий конечные группы
/// </summary>
/// <typeparam name="T"></typeparam>
public class Group<T> : IEnumerable<T>
{
    private HashSet<T> _set = new HashSet<T>();
    public T Id { get; init; }

    
    //public abstract T Mult(T el1);

    public Func<T, T, T> Mult;
    public Func<T, T, bool> GEquals;
    public Func<T, T> GetElementCopy;

    public Group(HashSet<T> set, Func<T, T, T> operation, Func<T, T, bool> equals, Func<T, T> copy)
    {
        _set = set;
        Mult = operation;
        GEquals = equals;
        GetElementCopy = copy;
        if(GetIdentity() is null)
            throw new ArgumentException("The given set doesn't form a group under provided operation");
        Id = GetIdentity()!;
        if (!CheckGroup())
            throw new ArgumentException("The given set doesn't form a group under provided operation");
    }


    public T Pow(T el, int power)
    {
        T a = GetElementCopy(el);
        
        if(power == 0)
            return Id;
        if (power < 0)
        {
            a = Inverse(a);
            power *= -1;
        }
        
        T x = GetElementCopy(a);
        for (int i = 1; i < power; i++)
        {
            x = Mult(x, a);
        }

        return x;
    }

    private bool CheckGroup()
    {
        if(!CheckAssociativity())
            Console.WriteLine("Assoc");
        
        if(!CheckClosure())
            Console.WriteLine("Closure");
        
        if(GetIdentity() is null)
            Console.WriteLine("Identity");
        
        if(!CheckInverses())
            Console.WriteLine("Inverses");
        
        return CheckAssociativity() && CheckClosure() && CheckInverses();
    }
    
    //Мегапроверка
    private bool CheckAssociativity()
    {

        foreach(T a in _set)
            foreach(T b in _set)
                foreach(T c in _set)
                    if (!GEquals(Mult(Mult(a, b), c), Mult(a, Mult(b, c))))
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
                if (!_set.Contains(Mult(a, b)))
                {
                    Console.WriteLine($"Not closed under operation: {Mult(a, b)} is not in the set");
                    return false;
                }

                
            }
        }

        return true;
    }

    private bool CheckIdentity(T a)
    {
        foreach(T x in _set)
            if (!(GEquals(Mult(x, a), Mult(a, x)) && GEquals(Mult(x, a), x)))
                return false;
        return true;
    }

    private T? GetIdentity()
    {
        // Возможно стоит добавить проверку на единственность нейтрального элемента
        foreach(T x in _set)
            if (CheckIdentity(x))
                return x;
        
        Console.WriteLine("No identity element!");
        return default;
    }

    private bool CheckInverses()
    {
        foreach (T x in _set)
        {
            bool flag = false;
            foreach (T y in _set)
                if (GEquals(Mult(x, y), Id) && GEquals(Mult(y, x), Id))
                {
                    flag = true;
                    break;
                }

            if (!flag)
            {
                Console.WriteLine($"Doesn't contain an inverse for {x}");
                return false;
            }
            
        }
        return true;
    }

    public T Inverse(T x)
    {
        foreach(T y in _set)
            if (GEquals(Mult(x, y), Id) && GEquals(Mult(y, x), Id))
                return y;
        
        throw new Exception("Разраб даун, в группе не нашлось обратного элемента");
    }

    public int GetElementOrder(T el)
    {
        if (!_set.Contains(el))
            throw new ArgumentException($"{el} does not belong to the group");
        T x = GetElementCopy(el);
        int order = 1;
        while (!GEquals(x, Id))
        {
            x = Mult(x, el);
            order++;
        }
        return order;
    }

    public int GetOrder() => _set.Count;


    public IEnumerator<T> GetEnumerator()
    {
        return _set.GetEnumerator();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Group (G, *):\n" +
                  "G = {\n  ");
        sb.Append(String.Join(",\n  ", _set));
        sb.Append("\n}");
        return sb.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}