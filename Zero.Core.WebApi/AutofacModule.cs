using Autofac;
using System;
using System.Linq;
using System.Reflection;
using Zero.Core.IServices.Base;

namespace Zero.Core.WebApi
{
    public class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string basePath = AppContext.BaseDirectory;
            //服务层放入容器
            string servicePath = basePath + "Zero.Core.Services.dll";
            var service = Assembly.LoadFile(servicePath);
            var supportService = typeof(ISupportService);
            builder.RegisterAssemblyTypes(service)
                 .Where(w => supportService.IsAssignableFrom(w))
                 .AsImplementedInterfaces();

            //仓储层放入容器
            string repositoryPath = basePath + "Zero.Core.Repositories.dll";
            var repository = Assembly.LoadFile(repositoryPath);
            builder.RegisterAssemblyTypes(repository)
                //.Where(w => supportRepository.IsAssignableFrom(w) || w.BaseType.GetType() == typeof(IUnitOfWork))
                .AsImplementedInterfaces();
        }

    }
}
