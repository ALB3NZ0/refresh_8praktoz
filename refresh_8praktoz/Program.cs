using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class Figure
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Figure() { }

    public Figure(string name, int width, int height)
    {
        Name = name;
        Width = width;
        Height = height;
    }
}

public class FileManager
{
    public static string[] LoadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            switch (fileExtension)
            {
                case ".txt":
                    return File.ReadAllLines(filePath);
                case ".json":
                    string jsonData = File.ReadAllText(filePath);
                    Figure figure = JsonConvert.DeserializeObject<Figure>(jsonData);
                    return new[] { $"Name: {figure.Name}", $"Width: {figure.Width}", $"Height: {figure.Height}" };
                case ".xml":
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    XmlNode root = xmlDoc.DocumentElement;
                    string[] lines = new string[root.ChildNodes.Count];
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlNode childNode = root.ChildNodes[i];
                        lines[i] = $"{childNode.Name}: {childNode.InnerText}";
                    }
                    return lines;
                default:
                    Console.WriteLine("Unsupported file format.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }

        return new string[] { };
    }

    public static void SaveFile(string filePath, string[] content)
    {
        string fileExtension = Path.GetExtension(filePath).ToLower();
        switch (fileExtension)
        {
            case ".txt":
                File.WriteAllLines(filePath, content);
                break;
            case ".json":
                Figure figure = new Figure
                {
                    Name = content[0].Split(':')[1].Trim(),
                    Width = int.Parse(content[1].Split(':')[1].Trim()),
                    Height = int.Parse(content[2].Split(':')[1].Trim())
                };
                string jsonData = JsonConvert.SerializeObject(figure, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                break;
            case ".xml":
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Figure));
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    xmlSerializer.Serialize(writer, new Figure
                    {
                        Name = content[0].Split(':')[1].Trim(),
                        Width = int.Parse(content[1].Split(':')[1].Trim()),
                        Height = int.Parse(content[2].Split(':')[1].Trim())
                    });
                }
                break;
            default:
                Console.WriteLine("Unsupported file format.");
                break;
        }
    }
}

public class TextEditor
{
    private Figure currentFigure;
    private string filePath;

    public void Run()
    {
        Console.WriteLine("Enter file path: ");
        filePath = Console.ReadLine();

        while (true)
        {
            Console.Clear();
            DisplayFigureInfo();

            Console.WriteLine("Options:");
            Console.WriteLine("1. Edit Figure");
            Console.WriteLine("2. Save (F1)");
            Console.WriteLine("3. Exit (Escape)");

            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    EditFigure();
                    break;
                case ConsoleKey.F1:
                    FileManager.SaveFile(filePath, new[] { $"Name: {currentFigure.Name}", $"Width: {currentFigure.Width}", $"Height: {currentFigure.Height}" });
                    Console.WriteLine("File saved successfully.");
                    Console.ReadKey();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void DisplayFigureInfo()
    {
        string[] content = FileManager.LoadFile(filePath);
        if (content.Length > 0)
        {
            currentFigure = new Figure
            {
                Name = content[0].Split(':')[1].Trim(),
                Width = int.Parse(content[1].Split(':')[1].Trim()),
                Height = int.Parse(content[2].Split(':')[1].Trim())
            };

            Console.WriteLine($"Name: {currentFigure.Name}");
            Console.WriteLine($"Width: {currentFigure.Width}");
            Console.WriteLine($"Height: {currentFigure.Height}");
            Console.WriteLine();
        }
    }

    private void EditFigure()
    {
        Console.Clear();
        Console.WriteLine("Edit Figure:");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Width");
        Console.WriteLine("3. Height");
        Console.WriteLine("4. Back");

        ConsoleKeyInfo editKey = Console.ReadKey();
        switch (editKey.Key)
        {
            case ConsoleKey.D1:
                Console.Write("Enter new Name: ");
                currentFigure.Name = Console.ReadLine();
                break;
            case ConsoleKey.D2:
                Console.Write("Enter new Width: ");
                currentFigure.Width = int.Parse(Console.ReadLine());
                break;
            case ConsoleKey.D3:
                Console.Write("Enter new Height: ");
                currentFigure.Height = int.Parse(Console.ReadLine());
                break;
            case ConsoleKey.D4:
                break;
        }
    }

    public static void Main()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.Run();
    }
}