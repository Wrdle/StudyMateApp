using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mobile.Helpers
{
    public class Helpers
    {
        public static ObservableCollection<T> convertListToObservableCollection<T>(List<T> list)
        {
            var oc = new ObservableCollection<T>();
            foreach (var item in list)
                oc.Add(item);
            return oc;
        }
    }
}
