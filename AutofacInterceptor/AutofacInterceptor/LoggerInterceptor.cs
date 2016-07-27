using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace AutofacInterceptor
{
   
        public class LoggerInterceptor : IInterceptor
        {
            ILogger _output;

            public LoggerInterceptor(AutofacInterceptor.ILogger output)
            {
                _output = output;
            }

            public void Intercept(IInvocation invocation)
            {
                _output.Write(invocation.Method.Name, string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

                invocation.Proceed();

                _output.WriteLine(invocation.ReturnValue.ToString());
            }
        }
    }

