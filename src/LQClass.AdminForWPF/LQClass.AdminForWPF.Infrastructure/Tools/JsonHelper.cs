using Newtonsoft.Json;
using System.IO;

namespace LQClass.AdminForWPF.Infrastructure.Tools
{
  public class JsonHelper
	{
		/// <summary>
		/// json字符串格式化输出
		/// </summary>
		/// <param name="sourceJsonStr"></param>
		/// <returns></returns>
		public static string FormatJsonString(string sourceJsonStr)
		{
			//格式化json字符串
			JsonSerializer serializer = new JsonSerializer();
			TextReader tr = new StringReader(sourceJsonStr);
			JsonTextReader jtr = new JsonTextReader(tr);
			object obj = serializer.Deserialize(jtr);
			if (obj != null)
			{
				StringWriter textWriter = new StringWriter();
				JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
				{
					Formatting = Formatting.Indented,
					Indentation = 4,
					IndentChar = ' '
				};
				serializer.Serialize(jsonWriter, obj);
				return textWriter.ToString();
			}
			else
			{
				return sourceJsonStr;
			}
		}
	}
}
