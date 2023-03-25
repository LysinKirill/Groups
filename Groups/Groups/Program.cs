using Groups;

class Program
{
    public static void Main(string[] args)
    {
        // Симметрическая группа 3
        Group<Permutation> S3 = new Group<Permutation>(
            Permutation.S(3).ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy());
        
        Group<Permutation> S4 = new Group<Permutation>(
            Permutation.S(4).ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy());
        
        
        Group<int> Z4 = new Group<int>(
            new HashSet<int> { 0, 1, 2, 3 },
            (a, b) => (a + b) % 4,
            (a, b) => a == b,
            a => a);


        // Z5 по сложению
        // Group<int> Z5 = new Group<int>(
        //     new HashSet<int> { 0, 1, 2, 3, 4 },
        //     (a, b) => (a + b) % 5,
        //     (a, b) => a == b,
        //     a => a);
        // Console.WriteLine(Z5.Id);
        
        // Z5 по умножению
        // Group<int> Z5m = new Group<int>(
        //      new HashSet<int> { 1, 2, 3, 4 },
        //      (a, b) => (a * b) % 5,
        //      (a, b) => a == b,
        //      a => a);

        MultiGroup<Permutation, Permutation, int>
            multiGroup = new MultiGroup<Permutation, Permutation, int>(S3, S4, Z4);

        int counter = 0;
        foreach (var x in multiGroup)
        {
            if (multiGroup.GetElementOrder(x) == 6)
            {
                counter++;
                Console.WriteLine(x);
            }
        }
        Console.WriteLine("____________________");
        Console.WriteLine(counter);
    }
}
