using AspNetCore.StartUpTemplate.AOP;
using AspNetCore.StartUpTemplate.IRepository;
using AspNetCore.StartUpTemplate.IService;
using AspNetCore.StartUpTemplate.Model;
using Autofac.Extras.DynamicProxy;

namespace AspNetCore.StartUpTemplate.Services;
public class UserService:BaseServices<Users>,IUserService
{
    private readonly IUserRepository _dal;

    public UserService(IUnitOfWork unitOfWork,IUserRepository userRepository) : base(unitOfWork)
    {
        base.BaseRepo = userRepository;
        _dal = userRepository;
    }
}