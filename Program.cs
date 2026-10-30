namespace DirectoryTreeViewer;
using System;
using System.IO;

class Program
{
    private static readonly ConsoleColor[] Colors = [
        ConsoleColor.Cyan,
        ConsoleColor.Blue,
        ConsoleColor.DarkMagenta,
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Yellow,
    ];
    
    static void Main(string[] args)
    {
        string currentPath = args switch
        {
            [var givenPath] => givenPath,
            _ => Directory.GetCurrentDirectory()
        };

        if (!Directory.Exists(currentPath))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{currentPath} not found");
            Console.ResetColor();
            return;
        }
        
        Walk(currentPath); 
    }
    
    static void Walk(string basePath, int depth = 0)
    {
        var contents = Directory.EnumerateFileSystemEntries(basePath);
        foreach (var path in contents)
        {
            var displayName = path.Replace(basePath + "/", "");
            var displayDepth = String.Concat(String.Concat(Enumerable.Repeat("   ", depth)), "├─");
            
            //Apply color for visual depth
            Console.ForegroundColor = Colors[depth % Colors.Length];
            
            Console.WriteLine($"{displayDepth} {displayName}");
            
            Console.ResetColor();
            
            if (Directory.Exists(path))
            {
                Walk(path, depth + 1);
            }
        }
    }
}