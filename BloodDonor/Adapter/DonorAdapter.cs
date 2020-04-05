using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using BloodDonor.DataModels;

namespace BloodDonor.Adapter
{
    class DonorAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DonorAdapterClickEventArgs> ItemClick;
        public event EventHandler<DonorAdapterClickEventArgs> ItemLongClick;
        List<Donor> DonorsList;

        public DonorAdapter(List<Donor> data)
        {
            DonorsList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.donor_row, parent, false);
            var vh = new DonorAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var donor = DonorsList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as DonorAdapterViewHolder;
            holder.nameDonorTextView.Text = donor.FullName;
            holder.locationDonorTextView.Text = donor.City + ", " + donor.Country;
        }

        public override int ItemCount => DonorsList.Count;

        void OnClick(DonorAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DonorAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class DonorAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView nameDonorTextView;
        public TextView locationDonorTextView;

        public ImageView bloodGroupImageView;

        public RelativeLayout callLayout;
        public RelativeLayout emailLayout;
        public RelativeLayout deleteLayout;


        public DonorAdapterViewHolder(View itemView, Action<DonorAdapterClickEventArgs> clickListener,
                            Action<DonorAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            nameDonorTextView = itemView.FindViewById<TextView>(Resource.Id.nameDonorTextView);
            locationDonorTextView = itemView.FindViewById<TextView>(Resource.Id.locationDonorTextView);

            bloodGroupImageView = itemView.FindViewById<ImageView>(Resource.Id.bloodGroupImageView);

            callLayout = itemView.FindViewById<RelativeLayout>(Resource.Id.callLayout);
            emailLayout = itemView.FindViewById<RelativeLayout>(Resource.Id.emailLayout);
            deleteLayout = itemView.FindViewById<RelativeLayout>(Resource.Id.deleteLayout);

            itemView.Click += (sender, e) => clickListener(new DonorAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DonorAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class DonorAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}