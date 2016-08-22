using Android.App;
using Android.Runtime;
using Autofac;
using ShoppingCart.Adapters;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Services;
using System;

namespace ShoppingCart
{
    [Application(Icon = "@drawable/ic_launcher", Label = "@string/app_name")]
    public class App : Application
    {
        public static IContainer Container { get; set; }

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
        }

        public override void OnCreate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new CartService()).As<ICartService>().SingleInstance();
            builder.RegisterInstance(new InventoryService()).As<IInventoryService>().SingleInstance();
            builder.RegisterType<InventoryAdapter>();
            App.Container = builder.Build();

            base.OnCreate();
        }
    }
}