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
            a => a.Copy()
        );
        
        Group<Dihedral.DihedralEl> D4 = new Group<Dihedral.DihedralEl>(
            Dihedral.D(4).ToHashSet(),
            (a, b) => a * b,
            (a, b) => a == b,
            a => a.Copy()
        );

        
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

        // Далее создайте объект MultiGroup - прямое произведение нужных вам групп
        
        // В качестве параметризующих типов указывайте типы элементов ваших групп
        // Симметрическая группа - Permutation
        // Диэдральная группа - Dihedral.DihedralEl
        // Zn - int
        
        // В качестве параметров передайте созданные ранее группы
        // Пример для D3 × D4 × Z4
        MultiGroup<Dihedral.DihedralEl, Dihedral.DihedralEl, int> 
            multiGroup = new MultiGroup<Dihedral.DihedralEl, Dihedral.DihedralEl, int>(D3, D4, Z4);
        
        // Укажите порядок элемента, который вы ищете
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
    }
}
