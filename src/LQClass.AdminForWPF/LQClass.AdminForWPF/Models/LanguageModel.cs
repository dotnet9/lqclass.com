using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.AdminForWPF.Models
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
