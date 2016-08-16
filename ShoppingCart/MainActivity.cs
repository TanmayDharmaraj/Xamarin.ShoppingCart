using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using ShoppingCart.Adapters;
using ShoppingCart.Models;
using System.Collections.Generic;

namespace ShoppingCart
{
    [Activity(Label = "ShoppingCart", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView _RecylerView;
        private RecyclerView.LayoutManager _LayoutManager;
        //private RecyclerView.Adapter _Adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _RecylerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _LayoutManager = new LinearLayoutManager(this);
            _RecylerView.SetLayoutManager(_LayoutManager);
            _RecylerView.SetAdapter(new InventoryAdapter(new List<Inventory>() {
                new Inventory()
                {
                    Title="Title1"
                },
                new Inventory()
                {
                    Title="Title2"
                }
            }));
        }
    }
}

