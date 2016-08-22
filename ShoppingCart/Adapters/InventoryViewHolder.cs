using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace ShoppingCart.Adapters
{
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
                        SKU = txtTitle.Text
                    });
                }
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