namespace Biblioteca;

public class LibraryCatalog
{
    private const int Capacity = 20;
    private Book[] books = new Book[Capacity];
    private int count = 0;

    // Número de comparaciones de la última búsqueda binaria
    public int LastComparisons { get; private set; }

    public int Count => count;

    // Agrega un libro manteniendo el arreglo ORDENADO por Id (ascendente)
    public bool AddBook(Book book)
    {
        if (count >= Capacity)
        {
            Console.WriteLine("El catálogo está lleno (20 libros).");
            return false;
        }

        // Evitar IDs repetidos
        for (int i = 0; i < count; i++)
        {
            if (books[i].Id == book.Id)
            {
                Console.WriteLine($"Ya existe un libro con el ID {book.Id}.");
                return false;
            }
        }

        // Buscar la posición de inserción y desplazar a la derecha
        int pos = count - 1;
        while (pos >= 0 && books[pos].Id > book.Id)
        {
            books[pos + 1] = books[pos];
            pos--;
        }

        books[pos + 1] = book;
        count++;
        return true;
    }

    // Búsqueda binaria manual. Devuelve el libro o null.
    public Book? BinarySearchById(Book[] books, int count, int id, bool trace = false)
    {
        int left = 0;
        int right = count - 1;
        int comparisons = 0;
        int iteration = 0;

        if (trace)
        {
            Console.WriteLine("Iter | left | right | middle | ID central | Decisión");
            Console.WriteLine("-----+------+-------+--------+------------+---------------------------");
        }

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            int currentLeft = left;
            int currentRight = right;
            iteration++;
            comparisons++; // un elemento central examinado

            int centralId = books[middle].Id;
            string decision;

            if (centralId == id)
            {
                decision = "Encontrado";
                if (trace)
                    Console.WriteLine($"{iteration,4} | {currentLeft,4} | {currentRight,5} | {middle,6} | {centralId,10} | {decision}");

                LastComparisons = comparisons;
                return books[middle];
            }
            else if (id < centralId)
            {
                decision = $"{id} < {centralId} -> right = {middle - 1}";
                right = middle - 1;
            }
            else
            {
                decision = $"{id} > {centralId} -> left = {middle + 1}";
                left = middle + 1;
            }

            if (trace)
                Console.WriteLine($"{iteration,4} | {currentLeft,4} | {currentRight,5} | {middle,6} | {centralId,10} | {decision}");
        }

        LastComparisons = comparisons;
        return null;
    }

    // Acceso al arreglo interno para llamar al método con la firma pedida
    public Book? SearchById(int id, bool trace = false)
    {
        return BinarySearchById(books, count, id, trace);
    }

    public void PrintAll()
    {
        if (count == 0)
        {
            Console.WriteLine("El catálogo está vacío.");
            return;
        }

        for (int i = 0; i < count; i++)
            Console.WriteLine($"Pos {i,2}: {books[i]}");
    }
}