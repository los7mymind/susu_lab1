using System;
using System.Collections.Generic;

class Stack<T>
{
    private List<T> elements = new List<T>();

    public void Push(T item)
    {
        elements.Add(item);
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty");
        }

        int lastIndex = elements.Count - 1;
        T poppedItem = elements[lastIndex];
        elements.RemoveAt(lastIndex);
        return poppedItem;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty");
        }

        return elements[elements.Count - 1];
    }

    public bool IsEmpty()
    {
        return elements.Count == 0;
    }

    public int Count()
    {
        return elements.Count;
    }
}

class MazeSolver
{
    private char[,] maze;
    private int rows;
    private int columns;

    public MazeSolver(char[,] maze)
    {
        this.maze = maze;
        this.rows = maze.GetLength(0);
        this.columns = maze.GetLength(1);
    }

    public bool FindPath(int startX, int startY)
    {
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        bool success = FindPathRecursive(startX, startY, path);

        if (success)
        {
            Console.WriteLine("Проход найден.");
            Console.WriteLine("Путь в лабиринте:");
            foreach (var point in path)
            {
                Console.WriteLine($"({point.Item1}, {point.Item2})");
            }
        }
        else
        {
            Console.WriteLine("Путь не найден.");
        }

        return success;
    }

    private bool FindPathRecursive(int x, int y, List<Tuple<int, int>> path)
    {
        if (x < 0 || x >= rows || y < 0 || y >= columns || maze[x, y] == 'X')
        {
            return false;
        }

        path.Add(new Tuple<int, int>(x, y));

        if (x == rows - 1 && y == columns - 1)
        {
            return true;
        }

        maze[x, y] = 'X';

        
        if (FindPathRecursive(x + 1, y, path) || // Вниз
            FindPathRecursive(x, y + 1, path) || // Вправо
            FindPathRecursive(x - 1, y, path) || // Вверх
            FindPathRecursive(x, y - 1, path))   // Влево
        {
            return true;
        }

        path.RemoveAt(path.Count - 1);
        return false;
    }
}

class Program
{
    static void Main()
    {
        char[,] maze2 = {
            { 'O', 'O', 'O', 'X', 'O' },
            { 'X', 'X', 'O', 'X', 'O' },
            { 'O', 'O', 'O', 'O', 'O' },
            { 'O', 'X', 'X', 'X', 'X' },
            { 'O', 'O', 'O', 'O', 'O' }
        };

        MazeSolver solver = new MazeSolver(maze2);
        bool pathFound = solver.FindPath(0, 0);

        if (pathFound)
        {
            Console.WriteLine("Проход найден.");
        }
    }
}
