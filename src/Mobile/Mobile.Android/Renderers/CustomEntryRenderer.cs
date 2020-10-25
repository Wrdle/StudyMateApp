using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Mobile.Droid;
using Mobile.Components;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
[assembly: ExportRenderer(typeof(CustomEditor), typeof(EditorCustomRenderer))]
namespace Mobile.Droid
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
#pragma warning disable CS0618 // Type or member is obsolete
                this.Control.SetBackgroundDrawable(gd);
#pragma warning restore CS0618 // Type or member is obsolete
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}