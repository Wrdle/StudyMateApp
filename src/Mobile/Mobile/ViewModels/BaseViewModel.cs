using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Mobile.Models;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
    }
}
