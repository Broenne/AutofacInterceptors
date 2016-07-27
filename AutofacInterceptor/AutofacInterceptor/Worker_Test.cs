using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace AutofacInterceptor
{
    public class Worker_Test
    {
        [Test]
        public void Do()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Worker>()
                   .EnableClassInterceptors();


            builder.RegisterType<LoggerInterceptor>()
                   .Named<IInterceptor>("log-calls");

            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();

            var scope = builder.Build();
            var worker = scope.Resolve<Worker>();
            worker.GetValue();
            worker.GetValue();
            var worker2 = scope.Resolve<Worker>();
            worker2.GetValue();

            var scope2 = scope.BeginLifetimeScope();
            var worker3 = scope2.Resolve<Worker>();
            worker3.GetValue();
        }
    }
    

    public class Logger : ILogger
    {
        private int i = 0;
        public void Write(string one, string two)
        {
            i++;
            Console.WriteLine("count: " + i);
            Console.WriteLine("Calling method {0} with parameters {1}... ", one, two);
        }

        public void WriteLine(string one)
        {
            Console.WriteLine("\nDone: result was {0}.", one);
        }
    }

    public interface ILogger
    {
        void Write(string one, string two);
        void WriteLine(string one);
    }

}
