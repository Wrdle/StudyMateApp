using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        //------------------------------
        //          Fields
        //------------------------------

        // Email
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        // Password
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // Error
        private string _error;
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        // IsEditing
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        // IsLoading
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        // Commands
        public ICommand LoginCommand { get; private set; }

        //------------------------------
        //          Constructors
        //------------------------------

        public LoginViewModel()
        {
            Email = "";
            Password = "";


            LoginCommand = new Command(
                execute: async () =>
                {
                    try
                    {
                        await UserStore.Login(Email, Password);
                        Application.Current.MainPage = new AppShell();
                    }
                    catch (Exception)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Email or password is incorrect.");
                    }
                },
                canExecute: () =>
                {
                    return !IsEditing && !IsLoading;
                });
        }

        //------------------------------
        //          Methods
        //------------------------------


    }
}
