namespace Groups;

public class Ring<T>
{
    protected Group<T> AdditiveGroup;
    protected Semigroup<T> MultiplicativeSemigroup;
    
    public T Id => AdditiveGroup.Id;
    
    public T Mult(T el1, T el2)
    {
        if (!AdditiveGroup.Set.Contains(el1) || !AdditiveGroup.Set.Contains(el2))
            throw new ArgumentException("The group does not contain such elements");
        return MultiplicativeSemigroup.AddFunc(el1, el2);
    }

    public T Add(T el1, T el2) => AdditiveGroup.Add(el1, el2);
    
    
    public Ring(HashSet<T> set, Func<T, T, T> add, Func<T, T, T> mult, Func<T, T, bool> equals, Func<T, T> copy, bool validate = true)
    {
        AdditiveGroup = new Group<T>(set, add, equals, copy, validate);
        MultiplicativeSemigroup = new Semigroup<T>(set, mult, equals, copy, validate);
    }
}