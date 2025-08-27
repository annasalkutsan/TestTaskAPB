using System.Text;
using CSVTree.ConsoleApp;

var projectPath = Path.Combine(AppContext.BaseDirectory, @"..\..\..");
var basePath = Path.GetFullPath(Path.Combine(projectPath, "Files"));

if (!Directory.Exists(basePath))
{
    Console.WriteLine($"Папка {basePath} не найдена.");
    return;
}

var inputPath = Path.Combine(basePath, "input.csv");
var outputPath = Path.Combine(basePath, "output.txt");

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Файл {inputPath} не найден.");
    return;
}

var builder = new TreeBuilder();
var nodes = builder.ReadCsv(inputPath);
var roots = builder.BuildTree(nodes);

builder.SortTree(roots);

var result = builder.PrintTree(roots);

File.WriteAllText(outputPath, result, Encoding.UTF8);

Console.WriteLine($"Дерево записано в {outputPath}");