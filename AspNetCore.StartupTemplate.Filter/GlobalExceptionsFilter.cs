﻿using System;
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

namespace AspNetCore.StartUpTemplate.Filter;
/// <summary>
/// 接口全局异常错误日志
/// </summary>
public class GlobalExceptionsFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionsFilter> _logger;

    public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        _logger.LogError($"全局异常 {context.Exception}");
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
        context.Result = new JsonResult(jm);
    }

}


