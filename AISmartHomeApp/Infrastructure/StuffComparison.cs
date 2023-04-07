using System.Reflection;

public static class StuffComparison {

    public static void Compare<T1, T2>(T1 a, T2 b) {
        Console.WriteLine("Hello World");
            // you can also use .GetProperties() if you actually want Properties with getters.
        Dictionary<string,string> aFields = typeof(T1).GetProperties().ToDictionary(x => x.Name, y => y.PropertyType.ToString());
        Dictionary<string,string> bFields = typeof(T2).GetProperties().ToDictionary(x => x.Name, y => y.PropertyType.ToString());

        // foreach(var bField in bFields){
        //     if (!aFields.Any(f=> f.Name == bField.Name && f.PropertyType == bField.PropertyType)) {
        //         throw new KeyNotFoundException($"{typeof(T1).Name} needs field {bField.Name} with type {bField.PropertyType} -  set from {typeof(T2).Name}");
        //     }
        //     else {
        //         Console.WriteLine($"{bField.Name} exists in {typeof(T1).Name}");
        //     }
        // }

        Console.WriteLine($"AAAA - {typeof(T1)}\tFields:{typeof(T1).GetFields().Count()}\tProperties:{typeof(T1).GetProperties().Count()}");
        Console.WriteLine($"BBBB - {typeof(T2)}\tFields:{typeof(T2).GetFields().Count()}\tProperties:{typeof(T2).GetProperties().Count()}");
        // foreach(var monkey in (typeof(T1).GetFields())) {
        //     Console.WriteLine($"Monkey {monkey.Name}");
        // }
        foreach(var bKV in bFields){
            //Console.WriteLine($"{bKV.Key} - {bKV.Value} - {aFields.ContainsKey(bKV.Key)}");
            if (!aFields.ContainsKey(bKV.Key)) {
                Console.WriteLine($"\tpublic {bKV.Value} {bKV.Key} {{get; set;}}");
                
            }
        }

    }

}