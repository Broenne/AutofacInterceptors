using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy2;
using Castle.DynamicProxy;

namespace AutofacInterceptor
{
    [Intercept("log-calls")]
    public class Worker
    {
        public virtual int GetValue()
        {
            return 42;
            // Do some calculation and return a value
        }
    }
}


   
