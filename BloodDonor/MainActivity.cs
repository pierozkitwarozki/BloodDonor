using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using BloodDonor.Adapter;
using System.Collections.Generic;
using BloodDonor.DataModels;

namespace BloodDonor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView donorsRecyclerView;
        DonorAdapter donorAdapter;
        List<Donor> donorsList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            donorsRecyclerView = FindViewById<RecyclerView>(Resource.Id.donorsRecyclerView);
            CreateData();
            SetupRecyclerView();
        }

        private void CreateData()
        {
            donorsList = new List<Donor>();
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });
            donorsList.Add(new Donor
            {
                FullName = "Michael Jordan",
                City = "Chicago",
                Country = "US",
                Email = "michael@gmail.com",
                BloodGroup = "0-",
                PhoneNumber = "567 321 090"
            });


        }

        private void SetupRecyclerView()
        {
            donorsRecyclerView.SetLayoutManager(new LinearLayoutManager(donorsRecyclerView.Context));
            donorAdapter = new DonorAdapter(donorsList);
            donorsRecyclerView.SetAdapter(donorAdapter);
        }

    }
}