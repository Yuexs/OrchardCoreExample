using System;
using System.Threading.Tasks;
using AliYunSecurityTokenServiceExample.Services;

namespace AliYunSecurityTokenServiceExample
{
    internal class Program
    {
        private static IAliYunSecurityTokenService _aliYunSecurityTokenService;

        static async Task Main(string[] args)
        {
            BootStrapper.Start();
            _aliYunSecurityTokenService = BootStrapper.Resolve<IAliYunSecurityTokenService>();

            var aliYunSecurityToken = await _aliYunSecurityTokenService.GetAliYunSecurityToken("directory");

            Console.WriteLine("SecurityTokenService\r\n");
            Console.WriteLine($"AccessKeyId:{aliYunSecurityToken.AccessKeyId}\r\n");
            Console.WriteLine($"AccessKeySecret:{aliYunSecurityToken.AccessKeySecret}\r\n");
            Console.WriteLine($"SecurityToken:{aliYunSecurityToken.SecurityToken}\r\n");
            Console.WriteLine($"Expiration:{aliYunSecurityToken.Expiration}\r\n");
            Console.Read();
        }
    }
}
