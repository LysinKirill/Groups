using System.Collections;
using System.Text;

namespace Groups;

/// <summary>
/// Класс, предстваляющий конечные группы
/// </summary>
/// <typeparam name="T"></typeparam>
public class Group<T> : Semigroup<T>
{
    private string _name = "G";
    public T Id { get; init; }
    

    public Group(HashSet<T> set, Func<T, T, T> operation, Func<T, T, bool> equals, Func<T, T> copy, bool validate = true) : base(set, operation, equals, copy, validate)
    {
        if (validate)
        {
            if (GetIdentity() is null)
                throw new ArgumentException("The given set doesn't form a group under provided operation");
            Id = GetIdentity()!;
            if (!CheckGroup())
                throw new ArgumentException("The given set doesn't form a group under provided operation");
        }
        // !!!При передаче false последним аргументом программа не проверяет на корректность переданные аргументы, могут возникнуть ошибки
        // !!!Передавайте false в качестве последнего аргумента только в том случае, если вы уверены, что данное множество и операция образуют полугруппу
        // При передаче false последним аргументом программа может работать значительно быстрее, т.к. не происходит многочисленных вычислений для множеств с большим количеством элементов
        else
        {
            Id = GetIdentity()!;
        }
    }

    /*
    public static int Pow(int a, int n)
    {
        if (n == 0)
            return a == 0 ? throw new ArgumentException("Cannot raise zero to the power of zero.") : 1;
        if (n < 0)
            throw new ArgumentException("This implementation of power only takes non-negative second argument");
        if (n % 2 == 0)
        {
            int t = Pow(a, n / 2);
            return t * t;
        }
        return a * Pow(a, n - 1);
    }

    */
    
    public T Pow(T el, int power)
    {
        T a = GetElementCopy(el);
        if (power < 0)
            return Pow(Inverse(a), -power);
        
        if(power == 0)
            return Id;

        if (power % 2 == 0)
        {
            T t = Pow(a, power / 2);
            return Mult(t, t);
        }
        return Mult(Pow(a, power - 1), a);

    }

    private bool CheckGroup()
    {
        // if(!CheckAssociativity())
        //     Console.WriteLine("Assoc");
        
        // if(!CheckClosure())
        //     Console.WriteLine("Closure");
        
        return CheckInverses();
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
        foreach (T x in _set)
        {
            if (CheckIdentity(x))
                return x;
        }

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

    public int GetGroupOrder() => _set.Count;

    public void SetName(string name) => _name = name;
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Group ({_name}, *):\n" +
                  $"{_name} = {{\n  ");
        sb.Append(String.Join(",\n  ", _set));
        sb.Append("\n}");
        return sb.ToString();
    }

    public Dictionary<int, int> CountOrders()
    {
        Dictionary<int, int> orders = new Dictionary<int, int>();
        foreach (var el in _set)
        {
            int ord = GetElementOrder(el);
            if (!orders.ContainsKey(ord))
                orders[ord] = 0;
            orders[ord]++;
        }

        return orders;
    }
}