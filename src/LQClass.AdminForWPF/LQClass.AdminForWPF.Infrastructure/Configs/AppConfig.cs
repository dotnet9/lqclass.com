using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.AdminForWPF.Infrastructure.Configs
{
    /// <summary>
    /// 对应config.json文件
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 开发环境配置
        /// </summary>
        public ConfigDesc Development { get; set; }

        /// <summary>
        /// 生产环境配置
        /// </summary>
        public ConfigDesc Production { get; set; }
    }

    /// <summary>
    /// 配置详细信息
    /// </summary>
    public class ConfigDesc
    {
        /// <summary>
        /// api地址
        /// </summary>
        public string API { get; set; }
    }
}
