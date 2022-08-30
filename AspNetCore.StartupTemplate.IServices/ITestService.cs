using AspNetCore.StartUpTemplate.AOP;
using AspNetCore.StartUpTemplate.Model;
using Autofac.Extras.DynamicProxy;

namespace AspNetCore.StartUpTemplate.IService;
[Intercept(typeof(TransactionInterceptor))]
public interface ITestService:IBaseService<Users> 
{
    void TestNestedTransOk();
    void TestNestedTransError();
}