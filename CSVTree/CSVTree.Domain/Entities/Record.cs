namespace CSVTree.Domain.Entities;

/// <summary>
/// Класс-сущность для записи из CSV файла
/// </summary>
public class Record
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Идентификатор родительской записи
    /// </summary>
    public int ParentId { get; private set; }

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Список с дочерними записями
    /// </summary>
    public List<Record> Childrens { get; } = [];

    /// <summary>
    /// Конструктор для создания записи
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="parentId">Идентификатор родителя</param>
    /// <param name="text">Текст записи</param>
    public Record(int id, int parentId, string? text)
    {
        Id = id;
        ParentId = parentId;
        Text = text ?? string.Empty;
    }
}