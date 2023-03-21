using Groups;

class Program
{
    public static void Main(string[] args)
    {
        var S3set = Permutation.S(3);
        // Симметрическая группа
        Group<Permutation> S3 = new Group<Permutation>(
            S3set.ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy());
        
        foreach (var x in S3)
        {
            Console.WriteLine($"ord({x}) = {S3.GetElementOrder(x)}");
        }
        Console.WriteLine(S3.Id);

        // Z5 по сложению
        Group<int> Z5 = new Group<int>(
            new HashSet<int> { 0, 1, 2, 3, 4 },
            (a, b) => (a + b) % 5,
            (a, b) => a == b,
            a => a);
        Console.WriteLine(Z5.Id);
        
        // Z5 по умножению
        Group<int> Z5m = new Group<int>(
             new HashSet<int> { 1, 2, 3, 4 },
             (a, b) => (a * b) % 5,
             (a, b) => a == b,
             a => a);
        
        Console.WriteLine(Z5m.GetElementOrder(3));
    }
}