using System.Collections;
using System.Text;

namespace Groups;

public class Semigroup<T> : IEnumerable<T>
{
    protected HashSet<T> _set = new HashSet<T>();
    
    //public abstract T Mult(T el1);

    public Func<T, T, T> Mult;
    public Func<T, T, bool> GEquals;
    public Func<T, T> GetElementCopy;

    public Semigroup(HashSet<T> set, Func<T, T, T> operation, Func<T, T, bool> equals, Func<T, T> copy, bool validate = true)
    {
        _set = set;
        Mult = operation;
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
}