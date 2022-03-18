using System.IO;
using Newtonsoft.Json;

namespace LQClass.AdminForWPF.Infrastructure.Tools;

public class JsonHelper
{
    /// <summary>
    ///     json字符串格式化输出
    /// </summary>
    /// <param name="sourceJsonStr"></param>
    /// <returns></returns>
    public static string FormatJsonString(string sourceJsonStr)
    {
        //格式化json字符串
        var serializer = new JsonSerializer();
        TextReader tr = new StringReader(sourceJsonStr);
        var jtr = new JsonTextReader(tr);
        var obj = serializer.Deserialize(jtr);
        if (obj != null)
        {
            var textWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }

        return sourceJsonStr;
    }
}