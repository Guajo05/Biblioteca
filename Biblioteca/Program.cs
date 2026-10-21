using Biblioteca;

var catalog = new LibraryCatalog();

// Libros iniciales (ya en orden ascendente por ID)
catalog.AddBook(new Book(10, "Clean Code", "Robert C. Martin", 2008));
catalog.AddBook(new Book(20, "The Pragmatic Programmer", "Andrew Hunt", 1999));
catalog.AddBook(new Book(30, "Design Patterns", "Erich Gamma", 1994));
catalog.AddBook(new Book(40, "Refactoring", "Martin Fowler", 1999));
catalog.AddBook(new Book(50, "Clean Architecture", "Robert C. Martin", 2017));
catalog.AddBook(new Book(60, "Domain-Driven Design", "Eric Evans", 2003));
catalog.AddBook(new Book(70, "Algorithms", "Robert Sedgewick", 1983));
catalog.AddBook(new Book(80, "Introduction to Algorithms", "Thomas H. Cormen", 1990));
catalog.AddBook(new Book(90, "Code Complete", "Steve McConnell", 1993));
catalog.AddBook(new Book(100, "Working Effectively with Legacy Code", "Michael Feathers", 2004));

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== BIBLIOTECA =====");
    Console.WriteLine("1. Listar catálogo");
    Console.WriteLine("2. Buscar libro por ID (binaria)");
    Console.WriteLine("3. Agregar libro (mantiene el orden)");
    Console.WriteLine("4. Ejecutar los 5 casos de prueba");
    Console.WriteLine("5. Mostrar traza de búsqueda por ID");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    string? option = Console.ReadLine();
    Console.WriteLine();

    switch (option)
    {
        case "1":
            catalog.PrintAll();
            break;

        case "2":
            SearchAndShow(catalog, trace: false);
            break;

        case "3":
            AddBookFromInput(catalog);
            break;

        case "4":
            RunTestCases(catalog);
            break;

        case "5":
            SearchAndShow(catalog, trace: true);
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

static int ReadInt(string message)
{
    while (true)
    {
        Console.Write(message);
        if (int.TryParse(Console.ReadLine(), out int value))
            return value;

        Console.WriteLine("Valor inválido, escribe un número entero.");
    }
}

static void SearchAndShow(LibraryCatalog catalog, bool trace)
{
    int id = ReadInt("ID a buscar: ");
    Console.WriteLine();

    Book? found = catalog.SearchById(id, trace);

    Console.WriteLine();
    if (found != null)
        Console.WriteLine($"Encontrado: {found}");
    else
        Console.WriteLine($"No existe un libro con el ID {id}.");

    Console.WriteLine($"Comparaciones: {catalog.LastComparisons}");
}

static void AddBookFromInput(LibraryCatalog catalog)
{
    int id = ReadInt("ID: ");
    Console.Write("Título: ");
    string title = Console.ReadLine() ?? "";
    Console.Write("Autor: ");
    string author = Console.ReadLine() ?? "";
    int year = ReadInt("Año: ");

    if (catalog.AddBook(new Book(id, title, author, year)))
        Console.WriteLine("Libro agregado en su posición ordenada.");
}

static void RunTestCases(LibraryCatalog catalog)
{
    int[] ids = { 10, 50, 70, 100, 999 };
    string[] cases = { "Primer libro", "Zona central", "Libro intermedio", "Último libro", "No existe" };

    Console.WriteLine("ID  | Caso             | ¿Encontrado? | Comparaciones");
    Console.WriteLine("----+------------------+--------------+--------------");

    for (int i = 0; i < ids.Length; i++)
    {
        Book? result = catalog.SearchById(ids[i]);
        string found = result != null ? "Sí" : "No";
        Console.WriteLine($"{ids[i],-3} | {cases[i],-16} | {found,-12} | {catalog.LastComparisons}");
    }
}