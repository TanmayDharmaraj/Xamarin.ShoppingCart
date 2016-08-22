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
using Android.Support.V7.Widget;
using ShoppingCart.Models;
using Android.Graphics;
using ShoppingCart.Helpers;
using Square.Picasso;
using ShoppingCart.Core.Services;
using Autofac;

namespace ShoppingCart.Adapters
{
    public class InventoryAdapter : RecyclerView.Adapter
    {
        private List<Inventory> _inventory;
        public static ICartService cartService;
        //private static Dictionary<string, Cart> _Cart;

        //public static Dictionary<string, Cart> Cart
        //{
        //    get { return _Cart; }
        //    set { _Cart = value; }
        //}

        public InventoryAdapter(List<Inventory> inventory)
        {
            this._inventory = inventory;
            using (var scope = App.Container.BeginLifetimeScope())
            {
                cartService = App.Container.Resolve<ICartService>();
            }
            //_Cart = new Dictionary<string, Models.Cart>();
        }

        public override int ItemCount
        {
            get
            {
                return _inventory.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            InventoryViewHolder viewHolder = holder as InventoryViewHolder;
            viewHolder.txtTitle.Text = _inventory[position].Title;
            viewHolder.txtCost.Text = string.Format("{0} {1}/{2}", "Rs.", _inventory[position].Cost.ToString(), "KG");
            viewHolder.txtQuantity.Text = _inventory[position].Quantity.ToString();

            Picasso.With(viewHolder.view.Context)
                .Load(_inventory[position].Image)
                .Placeholder(Resource.Drawable.placeholder)
                .Into(viewHolder.imgListImage);

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);
            TextView title = row.FindViewById<TextView>(Resource.Id.txtTitle);
            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.txtQuantity);
            TextView txtCost = row.FindViewById<TextView>(Resource.Id.txtCost);
            ImageView imgView = row.FindViewById<ImageView>(Resource.Id.list_image);
            Button btnAdd = row.FindViewById<Button>(Resource.Id.btnAdd);
            Button btnAddQuantity = row.FindViewById<Button>(Resource.Id.btnAddQuantity);
            Button btnSubtractQuantity = row.FindViewById<Button>(Resource.Id.btnSubtractQuantity);

            LinearLayout quantity_container = btnAdd.RootView.FindViewById<LinearLayout>(Resource.Id.layout_quantity);
            quantity_container.Visibility = ViewStates.Gone;


            InventoryViewHolder view = new InventoryViewHolder(row);
            view.txtTitle = title;
            view.txtCost = txtCost;
            view.imgListImage = imgView;
            view.btnAdd = btnAdd;
            view.btnAddQuantity = btnAddQuantity;
            view.btnSubtractQuantity = btnSubtractQuantity;
            view.txtQuantity = txtQuantity;
            return view;
        }
    }

    public class InventoryViewHolder : RecyclerView.ViewHolder
    {

        public View view { get; set; }
        public ImageView imgListImage { get; set; }
        public TextView txtTitle { get; set; }
        public TextView txtCost { get; set; }
        public Button btnAdd { get; set; }
        public Button btnAddQuantity { get; set; }
        public Button btnSubtractQuantity { get; set; }
        public TextView txtQuantity { get; set; }
        public InventoryViewHolder(View v) : base(v)
        {
            view = v;
            Button btnAdd = view.FindViewById<Button>(Resource.Id.btnAdd);
            btnAdd.Click += BtnAdd_Click;

            Button btnAddQuantity = view.FindViewById<Button>(Resource.Id.btnAddQuantity);
            btnAddQuantity.Click += BtnAddQuantity_Click;

            Button btnSubtractQuantity = view.FindViewById<Button>(Resource.Id.btnSubtractQuantity);
            btnSubtractQuantity.Click += BtnSubtractQuantity_Click;
        }

        private void BtnAddQuantity_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt16(txtQuantity.Text) + 1;
            if (quantity >= 1)
            {
                if (InventoryAdapter.cartService.GetItem(txtTitle.Text) != null)
                {
                    InventoryAdapter.cartService.GetItem(txtTitle.Text).Quantity++;
                }
                else
                {
                    InventoryAdapter.cartService.Add(new Core.Models.Cart()
                    {
                        Quantity = quantity,
                        SKU = txtTitle.Text,
                        //CostPerUnit = Convert.ToDouble(txtCost.Text)
                    });
                }

                //if (InventoryAdapter.Cart.ContainsKey(txtTitle.Text))
                //{
                //    InventoryAdapter.Cart[txtTitle.Text].Quantity++;
                //}
                //else
                //{
                //    InventoryAdapter.Cart.Add(txtTitle.Text, new Cart() { Quantity = quantity, SKU = txtTitle.Text });    
                //}

            }
            txtQuantity.Text = quantity.ToString();

        }

        private void BtnSubtractQuantity_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt16(txtQuantity.Text) - 1;
            if (quantity <= 0)
            {
                quantity = 0;
                txtQuantity.Text = quantity <= 0 ? "0" : quantity.ToString();
                if (InventoryAdapter.cartService.GetItem(txtTitle.Text) != null)
                {
                    InventoryAdapter.cartService.RemoveItem(InventoryAdapter.cartService.GetItem(txtTitle.Text));
                }
                //if (InventoryAdapter.Cart.ContainsKey(txtTitle.Text))
                //{
                //    InventoryAdapter.Cart.Remove(txtTitle.Text);
                //}
                btnAdd.Visibility = ViewStates.Visible;
                LinearLayout layout_quantity = view.FindViewById<LinearLayout>(Resource.Id.layout_quantity);
                layout_quantity.Visibility = ViewStates.Gone;
                btnAddQuantity.Visibility = ViewStates.Gone;
                btnSubtractQuantity.Visibility = ViewStates.Gone;
                txtQuantity.Visibility = ViewStates.Gone;
            }
            else
            {
                if (InventoryAdapter.cartService.GetItem(txtTitle.Text) != null)
                {
                    InventoryAdapter.cartService.GetItem(txtTitle.Text).Quantity--;
                    txtQuantity.Text = quantity.ToString();
                }
                //if (InventoryAdapter.Cart.ContainsKey(txtTitle.Text))
                //{
                //    InventoryAdapter.Cart[txtTitle.Text].Quantity--;
                //    txtQuantity.Text = quantity.ToString();
                //}
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Visibility = ViewStates.Gone;
            LinearLayout layout_quantity = view.FindViewById<LinearLayout>(Resource.Id.layout_quantity);
            layout_quantity.Visibility = ViewStates.Visible;
            btnAddQuantity.Visibility = ViewStates.Visible;
            btnSubtractQuantity.Visibility = ViewStates.Visible;
            txtQuantity.Visibility = ViewStates.Visible;
        }
    }
}