using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using BloodDonor.DataModels;
using FR.Ganfra.Materialspinner;

namespace BloodDonor.Fragments
{
  
    public class NewDonorFragment : Android.Support.V4.App.DialogFragment
    {
        MaterialSpinner materialSpinner;
        List<string> bloodGroups;
        ArrayAdapter<string> spinnerAdapter;
        private string selectedBloodGroup;

        TextInputLayout fullNameText;
        TextInputLayout emailText;
        TextInputLayout phoneNumberText;
        TextInputLayout cityText;
        TextInputLayout countryText;
        Button saveButton;

        public event EventHandler<DonorDetailEventArgs> OnDonorRegistered;

        public class DonorDetailEventArgs : EventArgs
        {
            public Donor Donor { get; set; }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.add_new, container, false);
            ConnectViews(view);
            SetupSpinner();
            return view;
        }

        private void ConnectViews(View view)
        {
            fullNameText = view.FindViewById<TextInputLayout>(Resource.Id.fullNameText);
            emailText = view.FindViewById<TextInputLayout>(Resource.Id.emailText);
            phoneNumberText = view.FindViewById<TextInputLayout>(Resource.Id.phoneNumberText);
            cityText = view.FindViewById<TextInputLayout>(Resource.Id.cityText);
            countryText = view.FindViewById<TextInputLayout>(Resource.Id.countryText);
            saveButton = view.FindViewById<Button>(Resource.Id.saveButton);
            materialSpinner = view.FindViewById<MaterialSpinner>(Resource.Id.bloodGroupSpinner);

            saveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string fullname, email, phonenumber, city, country;
            fullname = fullNameText.EditText.Text;
            email = emailText.EditText.Text;
            phonenumber = phoneNumberText.EditText.Text;
            city = cityText.EditText.Text;
            country = countryText.EditText.Text;

            if (fullname.Length < 5)
            {
                Toast.MakeText(Activity, "Please provide a valid username", ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(Activity, "Please provide a valid email", ToastLength.Short).Show();
                return;
            }
            else if (phonenumber.Length<9)
            {
                Toast.MakeText(Activity, "Please provide a valid phone number", ToastLength.Short).Show();
                return;
            }
            else if (city.Length<2)
            {
                Toast.MakeText(Activity, "Please provide a valid city", ToastLength.Short).Show();
                return;
            }
            else if (country.Length<2)
            {
                Toast.MakeText(Activity, "Please provide a valid country", ToastLength.Short).Show();
                return;
            }
            else if (selectedBloodGroup.Length<2)
            {
                Toast.MakeText(Activity, "Please select a blood group", ToastLength.Short).Show();
                return;
            }
            Donor donor = new Donor(){ 
                BloodGroup = selectedBloodGroup,
                City = city,
                Country = country,
                Email = email,
                FullName = fullname, PhoneNumber = phonenumber };

            OnDonorRegistered?.Invoke(this, new DonorDetailEventArgs() { Donor = donor });


        }

        private void SetupSpinner()
        {
            bloodGroups = new List<string> { "0+", "0-", "A+", "A-", "B+", "B-", "AB+", "AB-" };
            spinnerAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, bloodGroups);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            materialSpinner.Adapter = spinnerAdapter;
            materialSpinner.ItemSelected += MaterialSpinner_ItemSelected;
        }

        private void MaterialSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != -1)
            {
                selectedBloodGroup = bloodGroups[e.Position];
            }
            
        }
    }
}