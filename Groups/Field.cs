namespace Groups;

public class Field<T> : Ring<T>
{
    public T MultiplicativeId { get; init; }

    public Field(HashSet<T> set, Func<T, T, T> add, Func<T, T, T> mult, Func<T, T, bool> equals, Func<T, T> copy, bool validate = true) : base(set, add, mult, equals, copy, validate)
    {
        if (GetMultiplicativeIdentity() is null)
            throw new ArgumentException("The given set doesn't form a group under provided operation");
        MultiplicativeId = GetMultiplicativeIdentity()!;

        if (!CheckMultiplicativeInverses())
            throw new ArgumentException("The given set doesn't form a group under provided operation");
    }
    
    
    
    private bool CheckMultiplicativeIdentity(T a)
    {
        foreach(T x in AdditiveGroup.Set)
            if (!(MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(x, a), MultiplicativeSemigroup.AddFunc(a, x)) &&
                  MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(x, a), x)))
                return false;
        return true;
    }

    private T? GetMultiplicativeIdentity()
    {
        // Возможно стоит добавить проверку на единственность нейтрального элемента
        foreach (T x in MultiplicativeSemigroup.Set)
        {
            if (CheckMultiplicativeIdentity(x))
                return x;
        }

        Console.WriteLine("No identity element!");
        return default;
    }

    private bool CheckMultiplicativeInverses()
    {
        foreach (T x in MultiplicativeSemigroup.Set)
        {
            if(MultiplicativeSemigroup.GEquals(x, AdditiveGroup.Id))
                continue;
            bool flag = false;
            foreach (T y in MultiplicativeSemigroup.Set)
                if (MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(x, y), Id) && MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(y, x), Id))
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

    public T MultiplicativeInverse(T x)
    {
        if (MultiplicativeSemigroup.GEquals(x, AdditiveGroup.Id))
            throw new ArgumentException("The field has no inverse for additive identity");

        foreach (T y in MultiplicativeSemigroup.Set)
        {
            if (MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(x, y), MultiplicativeId) &&
                MultiplicativeSemigroup.GEquals(MultiplicativeSemigroup.AddFunc(y, x), MultiplicativeId))
                return y;
        }

        throw new Exception("Разраб даун, в группе не нашлось обратного элемента");
    }

    public T AdditiveInverse(T x) => AdditiveGroup.Inverse(x);
}