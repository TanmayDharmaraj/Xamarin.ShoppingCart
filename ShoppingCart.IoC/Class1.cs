using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.IoC
{
    public class Class1
    {
        private void SetIoc()
        {
            var containerBuilder = new Autofac.ContainerBuilder();

            containerBuilder.Register(c => AndroidDevice.CurrentDevice).As<IDevice>();
            containerBuilder.RegisterType<MainViewModel>().AsSelf();

            Resolver.SetResolver(new AutofacResolver(containerBuilder.Build()));
        }

    }
}
