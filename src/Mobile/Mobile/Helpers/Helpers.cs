using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Mobile.Helpers
{
    public static class Helpers
    {
        public static ObservableCollection<T> ConvertListToObservableCollection<T>(List<T> list)
        {
            var oc = new ObservableCollection<T>();
            foreach (var item in list)
                oc.Add(item);
            return oc;
        }
    }
}
