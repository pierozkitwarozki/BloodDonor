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
        public event EventHandler<DonorAdapterClickEventArgs> CallClick;
        public event EventHandler<DonorAdapterClickEventArgs> EmailClick;
        public event EventHandler<DonorAdapterClickEventArgs> DeleteClick;
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
            var vh = new DonorAdapterViewHolder(itemView, OnClick, OnLongClick, OnCallClick, OnEmailClick, OnDeleteClick);
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

            if (donor.BloodGroup == "0+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.o_positive);
            }
            else if (donor.BloodGroup == "0-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.o_negative);
            }
            else if (donor.BloodGroup == "A+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.a_positive);
            }
            else if (donor.BloodGroup == "A-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.a_negative);
            }
            else if (donor.BloodGroup == "B+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.b_positive);
            }
            else if (donor.BloodGroup == "B-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.b_negative);
            }
            else if (donor.BloodGroup == "AB+")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.ab_positive);
            }
            else if (donor.BloodGroup == "AB-")
            {
                holder.bloodGroupImageView.SetImageResource(Resource.Drawable.ab_negative);
            }
        }

        public override int ItemCount => DonorsList.Count;

        void OnClick(DonorAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DonorAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnCallClick(DonorAdapterClickEventArgs args) => CallClick?.Invoke(this, args);
        void OnEmailClick(DonorAdapterClickEventArgs args) => EmailClick?.Invoke(this, args);
        void OnDeleteClick(DonorAdapterClickEventArgs args) => DeleteClick?.Invoke(this, args);

        

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
                            Action<DonorAdapterClickEventArgs> longClickListener, Action<DonorAdapterClickEventArgs> callClickListener,
                            Action<DonorAdapterClickEventArgs> emailClickListener, Action<DonorAdapterClickEventArgs> deleteClickListener) : base(itemView)
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
            callLayout.Click += (sender, e) => callClickListener(new DonorAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            emailLayout.Click += (sender, e) => emailClickListener(new DonorAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteLayout.Click += (sender, e) => deleteClickListener(new DonorAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class DonorAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }

    
}