using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Ameritrack_Xam.Pages.Views;
using Ameritrack_Xam.Droid;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(MapExtension), typeof(CustomMapRenderer))]
namespace Ameritrack_Xam.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;
        private MapExtension _formsMap;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            this.NativeMap = googleMap;
            _map.UiSettings.ZoomControlsEnabled = true;
            _map.UiSettings.ZoomGesturesEnabled = true;
            _map.UiSettings.RotateGesturesEnabled = true;

            if (_map != null)
            {
                _map.MapClick += googleMap_MapClick;
                _map.MyLocationEnabled = _formsMap.IsShowingUser;

                foreach (var customPin in _formsMap.ListOfPins)
                {
                    var markerIcon = new MarkerOptions();
                    markerIcon.SetPosition(new LatLng(customPin.Position.Latitude, customPin.Position.Longitude));
                    markerIcon.SetTitle(customPin.Label);
                    markerIcon.SetSnippet(customPin.Address);

                    _map.AddMarker(markerIcon);

                    switch (_formsMap.MapType)
                    {
                        case MapType.Street:
                            _map.MapType = GoogleMap.MapTypeTerrain;
                            break;
                        case MapType.Satellite:
                            _map.MapType = GoogleMap.MapTypeSatellite;
                            break;
                        case MapType.Hybrid:
                            _map.MapType = GoogleMap.MapTypeHybrid;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            if (_map != null)
            {
                // unsubscribe

                _map.MapClick -= googleMap_MapClick;
            }

            base.OnElementChanged(e);

            if (Control != null)
            {
                // get the maps

                _formsMap = (MapExtension)e.NewElement;
                ((MapView)Control).GetMapAsync(this);
            }
        }

        private MarkerOptions CreateMarkerFromCustomPin(Pin customPin)
        {
            var markerIcon = new MarkerOptions();
            markerIcon.SetPosition(new LatLng(customPin.Position.Latitude, customPin.Position.Longitude));
            markerIcon.SetTitle(customPin.Label);
            markerIcon.SetSnippet(customPin.Address);

            return markerIcon;
        }

        private void UpdatePins()
        {
            if (_map != null)
            {
                _map.Clear();
                foreach (var customPin in _formsMap.ListOfPins)
                {
                    _map.AddMarker(CreateMarkerFromCustomPin(customPin));
                }
            }
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((MapExtension)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));

            // display the new pin
            UpdatePins();

            var builder = new AlertDialog.Builder(this.Context);
            builder.SetTitle("Confirmation")
                .SetMessage("Is this pin location correct?")
                .SetPositiveButton("Yes", (alertSender, args) =>
                {
                    // update the pins so that the new pin is reflected on the Custom Renderer PinsList
                    UpdatePins();

                    // TODO: open the fault-popup
                })
                .SetNegativeButton("No", (alertSender, args) =>
                {
                    // find the most recent pin in the list
                    var lastIndice = _formsMap.ListOfPins.Count();
                    if (lastIndice == 0)
                    {
                        Toast.MakeText(this.Context, "Pin Cancelled", ToastLength.Short).Show();
                        UpdatePins();
                        return;
                    }
                    _formsMap.ListOfPins.RemoveAt(lastIndice - 1);

                    UpdatePins();
                });
            builder.Show().Window.SetGravity(GravityFlags.Bottom);
        }
    }
}