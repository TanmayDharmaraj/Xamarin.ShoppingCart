using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using ShoppingCart.Adapters;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using System.Collections.Generic;

namespace ShoppingCart
{
    [Activity(Label = "Farm Fresh", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView _RecylerView;
        private RecyclerView.LayoutManager _LayoutManager;
        private InventoryAdapter _Adapter;
        private ICartService cartService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Resolve Stuff
            using (var scope = App.Container.BeginLifetimeScope())
            {
                cartService = App.Container.Resolve<ICartService>();
                _Adapter = App.Container.Resolve<InventoryAdapter>();
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            _RecylerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _LayoutManager = new LinearLayoutManager(this);
            _RecylerView.SetLayoutManager(_LayoutManager);
            _RecylerView.SetAdapter(_Adapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            List<Cart> cart = InventoryAdapter.cartService.GetAllItems();
            DrawerLayout container = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            string snac_text = "";
            foreach (Cart cartItem in cart)
            {
                snac_text += string.Format("{0} ({1}) ", cartItem.SKU, cartItem.Quantity);
            }
            if (snac_text != "")
            {
                Snackbar snackbar = Snackbar.Make(container, snac_text, Snackbar.LengthLong);
                snackbar.Show();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(container, "Add some items to the cart!", Snackbar.LengthShort);
                snackbar.Show();
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}