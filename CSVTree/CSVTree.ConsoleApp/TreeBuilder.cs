using System.Text;
using CSVTree.Domain.Entities;

namespace CSVTree.ConsoleApp;

/// <summary>
/// Класс для построения и вывода дерева из CSV-файла.
/// </summary>
internal class TreeBuilder
{
    /// <summary>
    /// Считывает CSV и возвращает словарь Record по ID.
    /// </summary>
    public Dictionary<int, Record> ReadCsv(string path) =>
        File.ReadAllLines(path, Encoding.UTF8)
            .Skip(1)
            .Select(line => line.Split(',', StringSplitOptions.TrimEntries))
            .Where(parts => parts.Length == 3)
            .Select(parts => new Record(int.Parse(parts[0]), int.Parse(parts[1]), parts[2]))
            .ToDictionary(r => r.Id);

    /// <summary>
    /// Строит дерево и возвращает список корней (ParentId = 0).
    /// </summary>
    public List<Record> BuildTree(Dictionary<int, Record> nodes)
    {
        var roots = new List<Record>();

        foreach (var node in nodes.Values)
        {
            if (node.ParentId == 0)
                roots.Add(node);
            else if (nodes.TryGetValue(node.ParentId, out var parent))
                parent.Childrens.Add(node);
        }

        return roots;
    }

    /// <summary>
    /// Итеративно сортирует дерево по Text на каждом уровне.
    /// </summary>
    public void SortTree(List<Record> roots)
    {
        var stack = new Stack<Record>(roots);

        while (stack.Count > 0)
        {
            var node = stack.Pop();

            node.Childrens.Sort((a, b) => string.Compare(a.Text, b.Text, StringComparison.Ordinal));

            foreach (var child in node.Childrens)
                stack.Push(child);
        }
    }

    /// <summary>
    /// Итеративно формирует строковое представление дерева с отступами (2 пробела на уровень).
    /// </summary>
    public string PrintTree(List<Record> roots)
    {
        var sb = new StringBuilder();
        var stack = new Stack<(Record node, int level)>();

        foreach (var root in roots.OrderBy(r => r.Text).Reverse())
            stack.Push((root, 0));

        while (stack.Count > 0)
        {
            var (node, level) = stack.Pop();
            sb.AppendLine(new string(' ', level * 2) + node.Text);

            foreach (var child in node.Childrens.OrderByDescending(c => c.Text))
                stack.Push((child, level + 1));
        }

        return sb.ToString();
    }
}