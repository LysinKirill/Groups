﻿using Groups;

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
        
        Group<Dihedral.DihedralEl> D5 = new Group<Dihedral.DihedralEl>(
            Dihedral.D(5).ToHashSet(),
            (a, b) => a * b,
            (a, b) => a == b,
            a => a.Copy());

        
        Group<Permutation> S4 = new Group<Permutation>(
            Permutation.S(4).ToHashSet(), 
            (a, b) => a * b, 
             (a, b) => a == b, 
             (a) => a.Copy());
        
        Group<Permutation> S3 = new Group<Permutation>(
            Permutation.S(3).ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy());

        
        // Если вы хотите работать с симметрическими группами порядка более 5, то обязательно последним аргументом указывайте false тогда программа не будет проверять
        // все условия группы (и, соответственно, будет работать значительно быстрее), но Sn всегда является группой
        Group<Permutation> S5 = new Group<Permutation>(
            Permutation.S(5).ToHashSet(), 
            (a, b) => a * b, 
            (a, b) => a == b, 
            (a) => a.Copy(),
            false);
        
        
        Group<int> Z4 = new Group<int>(
             new HashSet<int> { 0, 1, 2, 3 },
             (a, b) => (a + b) % 4,
             (a, b) => a == b,
             a => a);
        
        Group<int> Z2 = new Group<int>(
            new HashSet<int> { 0, 1},
            (a, b) => (a + b) % 2,
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
        Group<int> Z1 = new Group<int>(
            new HashSet<int>(){0},
            (a, b) => a,
            (a, b) => a == b,
            a => a
        );
        
        MultiGroup<Dihedral.DihedralEl, Permutation, int>
            multiGroup = new MultiGroup<Dihedral.DihedralEl, Permutation, int>(D3, S3, Z4);
        
        // Console.WriteLine("Группа D3");
        // foreach(var x in D3.CountOrders())
        //     Console.WriteLine($"ord = {x.Key}: count = {x.Value}");
        //
        // Console.WriteLine("\nГруппа S3");
        // foreach(var x in S3.CountOrders())
        //     Console.WriteLine($"ord = {x.Key}: count = {x.Value}");
        //
        // Console.WriteLine("\nГруппа Z4");
        // foreach(var x in Z4.CountOrders())
        //     Console.WriteLine($"ord = {x.Key}: count = {x.Value}");
        
        
        // Укажите порядок элемента, который вы ищете
        
        //
        // const int order = 6;
        //
        // int counter = 0;
        // foreach (var x in multiGroup)
        // {
        //     //Console.WriteLine(x);
        //     //Console.WriteLine(multiGroup.GetElementOrder(x));
        //     if (multiGroup.GetElementOrder(x) == order)
        //     {
        //         counter++; 
        //         // Вывод элементов указанного порядка. Можно закомментировать
        //         Console.WriteLine(x);
        //     }
        // }
        // Console.WriteLine("____________________");
        // Console.WriteLine(counter);
        
        D4.ShowCayleyTable();
        Console.ReadLine();
    }
}
