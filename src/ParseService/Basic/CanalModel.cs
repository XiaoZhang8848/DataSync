namespace ParseService.Basic;

public record CanalModel<T> where T : class, new()
{
    public string Type { get; init; }
    public string Database { get; init; }
    public string Table { get; init; }
    public Dictionary<string, string> MysqlType { get; init; } = new();
    public List<Dictionary<string, string>> Data { get; init; } = new();
    public List<T> SourceData { get; init; } = new();

    public void ParseData()
    {
        // 对表、库进行操作时 类型可能为QUERY 不处理
        if (Type != "INSERT" || Type != "UPDATE" || Type != "DELETE")
        {
            return;
        }
        
        var typeCodes = GetTypeCode();
        var t = new T();
        var properties = t.GetType().GetProperties();

        foreach (var dict in Data)
        {
            foreach (var property in properties)
            {
                object value = dict[property.Name];
                property.SetValue(t, Convert.ChangeType(value, typeCodes[property.Name]));
            }

            SourceData.Add(t);
        }
    }

    private Dictionary<string, TypeCode> GetTypeCode()
    {
        var typeCodes = new Dictionary<string, TypeCode>();
        foreach (var item in MysqlType)
            if (item.Value.StartsWith("int"))
                typeCodes.Add(item.Key, TypeCode.Int32);
            else if (item.Value.StartsWith("longtext"))
                typeCodes.Add(item.Key, TypeCode.String);
            else if (item.Value.StartsWith("datetime")) typeCodes.Add(item.Key, TypeCode.DateTime);
        return typeCodes;
    }
}