using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using ShoppingCart.Core.Services;


namespace ShoppingCart
{
    [Application(Icon = "@drawable/ic_launcher", Label = "Farm Fresh")]
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
            builder.RegisterType<MainActivity>();

            App.Container = builder.Build();

            base.OnCreate();
        }
    }
}