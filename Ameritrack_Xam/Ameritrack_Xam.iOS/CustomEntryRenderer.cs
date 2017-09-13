using Mobile.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Mobile.iOS.Renderers
{
	public class CustomEntryRenderer : EntryRenderer
	{
		private CoreAnimation.CALayer line;

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			line = null;

			if (Control == null || e.NewElement == null) return;

			Control.BorderStyle = UIKit.UITextBorderStyle.None;

			line = new CoreAnimation.CALayer();
			line.BorderColor = Color.White.ToCGColor();
			line.BackgroundColor = Color.White.ToCGColor();
			line.Frame = new CoreGraphics.CGRect(0, Frame.Height / 1.25, Frame.Width * 2, 1f);

            Control.Layer.AddSublayer(line);
		}
	}
}
