﻿/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *           Versions: 1.0                                             *
 *         CreateTime: 2020-02-05 19:20:08
 *        Description: 
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AspNetCore.StartUpTemplate.Contract;
using AspNetCore.StartUpTemplate.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
namespace AspNetCore.StartUpTemplate.Filter;
/// <summary>
/// 接口全局异常错误日志
/// </summary>
public class GlobalExceptionsFilter : IExceptionFilter
{

    public void OnException(ExceptionContext context)
    {
        LogManager.GetCurrentClassLogger().LogErrorEx("全局捕获异常",context.Exception);
        HttpStatusCode status = HttpStatusCode.InternalServerError;

        //处理各种异常
        var jm = new ResponseResult
        {
            Status = false,
            Code = (int)status,
            Msg = "系统返回异常，请联系管理员进行处理！",
            Data = context.Exception
        };
        context.ExceptionHandled = true;
        context.Result = new ObjectResult(jm);

    }

}


