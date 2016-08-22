using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using Square.Picasso;
using System.Collections.Generic;

namespace ShoppingCart.Adapters
{
    public class InventoryAdapter : RecyclerView.Adapter
    {
        private List<Inventory> _inventory;
        public static ICartService cartService;

        public InventoryAdapter(IInventoryService inventoryService, ICartService _cartService)
        {
            this._inventory = inventoryService.GetInventory();
            cartService = _cartService;
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
}