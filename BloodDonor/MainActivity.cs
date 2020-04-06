using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using BloodDonor.Adapter;
using System.Collections.Generic;
using BloodDonor.DataModels;
using Android.Content;
using Android.Support.Design.Widget;
using BloodDonor.Fragments;
using Newtonsoft.Json;

namespace BloodDonor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView donorsRecyclerView;
        DonorAdapter donorAdapter;
        List<Donor> donorsList;
        NewDonorFragment newDonorFragment;
        TextView noDonorTextView;

        ISharedPreferences sharedPreferences = Application.Context.GetSharedPreferences("donors", FileCreationMode.Private);
        ISharedPreferencesEditor sharedPreferencesEditor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Blood donors";
            donorsRecyclerView = FindViewById<RecyclerView>(Resource.Id.donorsRecyclerView);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            noDonorTextView = FindViewById<TextView>(Resource.Id.noDonorTextView);
            sharedPreferencesEditor = sharedPreferences.Edit();
            fab.Click += Fab_Click;
            RetrieveData(); 
            if(donorsList.Count > 0)
            {
                SetupRecyclerView();
            }
            else
            {
                noDonorTextView.Visibility = Android.Views.ViewStates.Visible;
            }
            
            
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            newDonorFragment = new NewDonorFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            newDonorFragment.Show(trans, "new donor");
            newDonorFragment.OnDonorRegistered += NewDonorFragment_OnDonorRegistered;
        }

        private void NewDonorFragment_OnDonorRegistered(object sender, NewDonorFragment.DonorDetailEventArgs e)
        {
            if (newDonorFragment != null)
            {
                newDonorFragment.Dismiss();
                newDonorFragment = null;
            }
            if (donorsList.Count > 0)
            {
                
                donorsList.Insert(0, e.Donor);
                donorAdapter.NotifyItemInserted(0);
                string jsonString = JsonConvert.SerializeObject(donorsList);
                sharedPreferencesEditor.PutString("donors", jsonString);
                sharedPreferencesEditor.Apply();
            }
            else
            {
                donorsList.Add(e.Donor);
                string jsonString = JsonConvert.SerializeObject(donorsList);
                sharedPreferencesEditor.PutString("donors", jsonString);
                sharedPreferencesEditor.Apply();
                noDonorTextView.Visibility = Android.Views.ViewStates.Invisible;
                SetupRecyclerView();
            }
            
        }

        private void RetrieveData()
        {
            donorsList = new List<Donor>();
            string json = sharedPreferences.GetString("donors", "");
            if (!string.IsNullOrEmpty(json))
            {
                donorsList = JsonConvert.DeserializeObject<List<Donor>>(json);
            }
        }


        private void SetupRecyclerView()
        {
            donorsRecyclerView.SetLayoutManager(new LinearLayoutManager(donorsRecyclerView.Context));
            donorAdapter = new DonorAdapter(donorsList);
            donorAdapter.ItemClick += DonorAdapter_ItemClick;
            donorAdapter.CallClick += DonorAdapter_CallClick;
            donorAdapter.EmailClick += DonorAdapter_EmailClick;
            donorAdapter.DeleteClick += DonorAdapter_DeleteClick;
            donorsRecyclerView.SetAdapter(donorAdapter);
        }

        private void DonorAdapter_DeleteClick(object sender, DonorAdapterClickEventArgs e)
        {
            var donor = donorsList[e.Position];
            Android.Support.V7.App.AlertDialog.Builder deleteAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            deleteAlert.SetTitle("Delete donor");
            deleteAlert.SetMessage("Are you sure to delete " + donor.FullName);
            deleteAlert.SetPositiveButton("Delete", (alert, args) =>
            {
                donorsList.RemoveAt(e.Position);
                donorAdapter.NotifyItemRemoved(e.Position);

                string jsonString = JsonConvert.SerializeObject(donorsList);
                sharedPreferencesEditor.PutString("donors", jsonString);
                sharedPreferencesEditor.Apply();
            });
            deleteAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                deleteAlert.Dispose();
            });
            deleteAlert.Show();
        }

        private void DonorAdapter_EmailClick(object sender, DonorAdapterClickEventArgs e)
        {
            var donor = donorsList[e.Position];
            Android.Support.V7.App.AlertDialog.Builder emailAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            emailAlert.SetMessage("Email " + donor.FullName);
            emailAlert.SetPositiveButton("Send", (alert, args) =>
            {
                Intent intent = new Intent();
                intent.SetType("plain/text");
                intent.SetAction(Intent.ActionSend);
                intent.PutExtra(Intent.ExtraEmail, new string[] {donor.Email });
                intent.PutExtra(Intent.ExtraSubject, "Donation" );
                StartActivity(intent);
            });
            emailAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                emailAlert.Dispose();
            });
            emailAlert.Show();
        }

        private void DonorAdapter_CallClick(object sender, DonorAdapterClickEventArgs e)
        {
            var donor = donorsList[e.Position];

            Android.Support.V7.App.AlertDialog.Builder callAlert = new Android.Support.V7.App.AlertDialog.Builder(this);
            callAlert.SetMessage("Call " + donor.FullName);
            callAlert.SetPositiveButton("Call", (alert, args) =>
            {
                var uri = Android.Net.Uri.Parse("tel: " + donor.PhoneNumber);
                var intent = new Intent(Intent.ActionDial, uri);
                StartActivity(intent);
            });

            callAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                callAlert.Dispose();
            });

            callAlert.Show();
        }

        private void DonorAdapter_ItemClick(object sender, DonorAdapterClickEventArgs e)
        {
            Toast.MakeText(this, "Item Clicked!", ToastLength.Short).Show();
        }
    }
}