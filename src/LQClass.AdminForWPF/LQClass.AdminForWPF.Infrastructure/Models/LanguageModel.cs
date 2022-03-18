using System.Collections.Generic;

namespace LQClass.AdminForWPF.Infrastructure.Models;

public class LanguageModel
{
    private static List<LanguageModel> languages;
    public string Name { get; set; }
    public string Key { get; set; }

    public static List<LanguageModel> GetLanguages()
    {
        if (languages == null)
            languages = new List<LanguageModel>
            {
                new() { Name = "中文", Key = "zh-CN" },
                new() { Name = "English", Key = "en" }
            };
        return languages;
    }
}