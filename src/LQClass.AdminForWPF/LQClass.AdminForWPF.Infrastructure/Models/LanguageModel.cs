using System.Collections.Generic;

namespace LQClass.AdminForWPF.Infrastructure.Models
{
  public class LanguageModel
  {
    public string Name { get; set; }
    public string Key { get; set; }

    private static List<LanguageModel> languages = null;
    public static List<LanguageModel> GetLanguages()
    {
      if (languages == null)
      {
        languages = new List<LanguageModel>
        {
          new LanguageModel{ Name="中文",Key="zh-CN"},
          new LanguageModel{ Name="English",Key="en"}
        };
      }
      return languages;
    }
  }
}
