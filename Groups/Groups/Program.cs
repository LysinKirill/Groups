using Groups;

class Program
{
    public static void Main(string[] args)
    {
        // Сначала объявляете нужные вам группы как показано ниже
        Group<Dihedral.DihedralEl> D3 = new Group<Dihedral.DihedralEl>(
            Dihedral.D(3).ToHashSet(),
            (a, b) => a * b,
            (a, b) => a == b,
            a => a.Copy());
        
        Group<Dihedral.DihedralEl> D4 = new Group<Dihedral.DihedralEl>(
            Dihedral.D(4).ToHashSet(),
            (a, b) => a * b,
            (a, b) => a == b,
            a => a.Copy());

        
        Group<Permutation> S4 = new Group<Permutation>(
            Permutation.S(4).ToHashSet(), 
            (a, b) => a * b, 
             (a, b) => a == b, 
             (a) => a.Copy());
        
        // Если вы хотите работать с симметрическими группами порядка более 5, то обязательно последним аргументом указывайте false тогда программа не будет проверять
        // все условия группы (и, соответственно, будет работать значительно быстрее), но Sn всегда является группой
        Group<Permutation> S8 = new Group<Permutation>(
            Permutation.S(8).ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy(),
            false);
        
        
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

        // Далее создайте объект MultiGroup - прямое произведение нужных вам групп
        
        // В качестве параметризующих типов указывайте типы элементов ваших групп
        // Симметрическая группа - Permutation
        // Диэдральная группа - Dihedral.DihedralEl
        // Zn - int
        
        // В качестве параметров передайте созданные ранее группы
        // Пример для D3 × D4 × Z4
        //MultiGroup<Permutation, Permutation, int> 
        //    multiGroup = new MultiGroup<Permutation, Permutation, int>(S3, S4, Z4);
        
        // Укажите порядок элемента, который вы ищете
        
        /*
        const int order = 6;
        
        int counter = 0;
        foreach (var x in multiGroup)
        {
            if (multiGroup.GetElementOrder(x) == order)
            {
                counter++;
                // Вывод элементов указанного порядка. Можно закомментировать
                Console.WriteLine(x);
            }
        }
        Console.WriteLine("____________________");
        Console.WriteLine(counter);
        */
        
        foreach(var x in S8.CountOrders())
            Console.WriteLine($"ord = {x.Key}; count = {x.Value}");
    }
}
