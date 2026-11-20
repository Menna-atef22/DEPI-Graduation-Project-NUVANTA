namespace Nuvanta.Application.DTOs;

public class TableProbeDto
{
    public string Table { get; set; } = string.Empty;
    public bool Ok { get; set; }
    public int RowCount { get; set; }
    public string? Error { get; set; }
}

public class DatabaseHealthDto
{
    public bool CanConnect { get; set; }
    public int TablesChecked => Tables.Count;
    public int TablesOk => Tables.Count(t => t.Ok);
    public int TablesFailed => Tables.Count(t => !t.Ok);
    public List<TableProbeDto> Tables { get; set; } = new();
}
