namespace LQClass.ModuleOfLog.DTOs;

public class LogDto
{
    public string LogType { get; set; }

    public string ModuleName { get; set; }

    public string ActionName { get; set; }

    public string ITCode { get; set; }

    public string ActionUrl { get; set; }

    public string ActionTime { get; set; }

    public string Duration { get; set; }

    public string IP { get; set; }

    public string Remark { get; set; }

    public string TempIsSelected { get; set; }

    public string Duration__forecolor { get; set; }

    public string ID { get; set; }

    public string LAY_CHECKED { get; set; }
}

public enum LogType
{
    Normal,
    Exception,
    Debug
}

public class LogTypeInfo
{
    public LogTypeInfo(int key, string value)
    {
        Key = key;
        Value = value;
    }

    public int Key { get; set; }
    public string Value { get; set; }
}