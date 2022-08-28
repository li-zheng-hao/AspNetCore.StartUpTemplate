﻿namespace AspNetCore.StartUpTemplate.Configuration;

using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

/// <summary>
/// 获取Appsettings配置信息
/// </summary>
public class AppSettingsHelper
{
    private static IConfiguration Configuration { get; set; }

    public AppSettingsHelper(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 封装要操作的字符
    /// AppSettingsHelper.GetContent(new string[] { "JwtConfig", "SecretKey" });
    /// </summary>
    /// <param name="sections">节点配置</param>
    /// <returns></returns>
    public static string GetContent(params string[] sections)
    {
        try
        {

            if (sections.Any())
            {
                return Configuration[string.Join(":", sections)];
            }
        }
        catch (Exception) { }

        return "";
    }

}
