using System;
using Autofac;
using Autofac.Core;
using AliYunSecurityTokenServiceExample.Services;

namespace AliYunSecurityTokenServiceExample
{
    public static class BootStrapper
    {
        private static ILifetimeScope _rootScope;

        /// <summary>
        /// 启动依赖注入
        /// </summary>
        public static void Start()
        {
            if (_rootScope != null)
            {
                return;
            }

            var builder = new ContainerBuilder();

            // 授权服务
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().WithParameters(new Parameter[]
            {
                new NamedParameter("clientId", Configs.AuthorizationClientId),
                new NamedParameter("clientSecret", Configs.AuthorizationClientSecret)
            });
            // 阿里云临时安全凭证
            builder.RegisterType<AliYunSecurityTokenService>().As<IAliYunSecurityTokenService>();
            _rootScope = builder.Build();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            _rootScope.Dispose();
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T Resolve<T>()
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }
    }
}
