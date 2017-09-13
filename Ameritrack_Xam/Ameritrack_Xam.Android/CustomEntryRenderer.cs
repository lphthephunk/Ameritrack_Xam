using Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Mobile.Droid.Renderers
{
	public class CustomEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control == null || e.NewElement == null) return;

			Control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Color.White.ToAndroid());
		}
	}
}