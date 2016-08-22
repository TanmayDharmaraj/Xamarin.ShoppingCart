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
using Android.Support.V4.Widget;
using Android.Support.V4.View;

using Autofac;
using ShoppingCart.Core.Services;
using ShoppingCart.Core.Models;

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

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            using (var scope = App.Container.BeginLifetimeScope())
            {
                cartService = App.Container.Resolve<ICartService>();
            }
            _RecylerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _LayoutManager = new LinearLayoutManager(this);
            _RecylerView.SetLayoutManager(_LayoutManager);
            _Adapter = new InventoryAdapter(new List<Inventory>() {
                new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-mint-bunch1-v-1-pc.png",Title="Fresh Mint Bunch",Cost=14},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-coconut-regular1-v-1-nos.png",Title="Fresh Coconut Regular",Cost=21},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-basket-broccoli-v-500-g.png",Title=" Broccoli",Cost=300},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-sweet-potato1-v-900-g.png",Title="Fresh Sweet Potato",Cost=58.5},new Inventory(){Image="https://p1.zopnow.com/images/products/140/hypercity-fresh-arvi-roots-v-250-g.png",Title="Fresh Colacasia Arvi Roots",Cost=14.75},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-broad-beans-papadi1-v-250-g.png",Title="Fresh Broad Beans Avare Chikadi (Papadi)",Cost=24.75},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-cluster-beans-v-250-g.png",Title="Fresh Cluster Beans",Cost=19.75},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-basket-brinjal-bharta-v-500-g.png",Title="Brinjal Bharta",Cost=26},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-bitter-gourd-v-750-g.png",Title="Fresh Bitter Gourd",Cost=44.25},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-basket-potato-v-1-kg-1.png",Title="Potato",Cost=28},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-brinjal-kateri1-v-500-g.png",Title="Fresh Brinjal Kateri",Cost=27},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-tinda1-v-250-g-1.png",Title="Fresh Tinda",Cost=21.25},new Inventory(){Image="https://p2.zopnow.com/images/products/140/hypercity-fresh-snake-gourd-v-500-g.png",Title="Fresh Snake Gourd",Cost=22.5},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-basket-capsicum-red-yellow-packed-v-350-g.png",Title="Capsicum Red Yellow Packed",Cost=128},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-basket-lady-finger-v-350-g.png",Title="Lady Finger",Cost=19.25},new Inventory(){Image="https://p1.zopnow.com/images/products/140/fresh-basket-tomato-v-500-g.png",Title="Tomato",Cost=21.6},new Inventory(){Image="https://p2.zopnow.com/images/products/140/hypercity-fresh-pointer-gourd-parval-v-500-g.png",Title="Fresh Pointer Gourd (Parval)",Cost=29.5},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-basket-cherry-tomato-red-v-200-g.png",Title="Cherry Tomato Red",Cost=110},new Inventory(){Image="https://p2.zopnow.com/images/products/140/fresh-ridge-gourd-v-500-g.png",Title="Fresh Ridge Gourd",Cost=26.5},new Inventory(){Image="https://p2.zopnow.com/images/products/140/hypercity-fresh-lemon-grass-v-200-g.png",Title="Fresh Lemon Grass",Cost=27}
            });

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

