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

namespace ShoppingCart.Adapters
{
    public class InventoryAdapter : RecyclerView.Adapter
    {
        private List<Inventory> _inventory;
        public InventoryAdapter(List<Inventory> inventory)
        {
            this._inventory = inventory;
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
            InventoryView viewHolder = holder as InventoryView;
            viewHolder.Title.Text = _inventory[position].Title;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);
            TextView title = row.FindViewById<TextView>(Resource.Id.txtTitle);
            InventoryView view = new InventoryView(row);
            view.Title = title;
            return view;
        }
    }

    public class InventoryView : RecyclerView.ViewHolder
    {
        public View view { get; set; }
        public TextView Title { get; set; }
        public InventoryView(View v) : base(v)
        {
            view = v;
        }
    }
}