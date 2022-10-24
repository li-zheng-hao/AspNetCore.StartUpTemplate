﻿using System.Security.Cryptography;
using AspNetCore.StartUpTemplate.Configuration;
using AspNetCore.StartUpTemplate.Core;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;

namespace AspNetCore.StartUpTemplate.Auth;

public class TokenHelper
{
    /// <summary>
    /// 对称加密Token
    /// </summary>
    /// <param name="model"></param>
    /// <param name="expireSeconds">过期时间,单位秒,默认15分钟</param>
    /// <returns></returns>
    public static string CreateToken(UserData? model=null,int expireSeconds=60*15)
    {
        var config=IocHelper.ResolveSingleton<GlobalConfig>();
        var token = JwtBuilder.Create()
                 .WithAlgorithm(new HMACSHA256Algorithm())
                 .AddClaim("expireTime", DateTime.Now.AddSeconds(expireSeconds))
                 .AddClaim("userData", model)
                 .WithSecret(config.Jwt.Key)
                 .Encode();
        return token;
    }
    /// <summary>
    ///  对称解密
    /// </summary>
    public static TokenModel ResolveToken(string token)
    {
        var config=IocHelper.ResolveSingleton<GlobalConfig>();
        var json = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .MustVerifySignature()
                .WithSecret(config.Jwt.Key)// 这个secret是关键，要和上面加密的secret一致
                .Decode(token);
        var res= JsonConvert.DeserializeObject<TokenModel>(json);
        return res;
    }
    /// <summary>
    /// 验证token是否有效
    /// </summary>
    /// <param name="token"></param>
    /// <returns>0 通过 1超时 2其他错误</returns>
    public static (int,TokenModel?) ValidateToken(string token)
    {
        try
        {
            var model=ResolveToken(token);
            if (model.ExpireTime < DateTime.Now)
                return (1,model);
            return (0,model);
        }
        catch (Exception)
        {
            return (2,null);
        }
        
    }
}