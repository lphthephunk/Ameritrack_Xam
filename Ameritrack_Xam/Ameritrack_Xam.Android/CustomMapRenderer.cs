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
using static Android.Gms.Maps.GoogleMap;
using Android.Graphics;
using System.Timers;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(MapExtension), typeof(CustomMapRenderer))]
namespace Ameritrack_Xam.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;
        private MapExtension _formsMap;

        private bool isPinPopupShown = false;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            this.NativeMap = googleMap;
            _map.UiSettings.ZoomControlsEnabled = true;
            _map.UiSettings.ZoomGesturesEnabled = true;
            _map.UiSettings.RotateGesturesEnabled = true;

            UpdatePins();

            if (_map != null)
            {
                _map.MapLongClick += googleMap_MapLongClick;
                _map.MyLocationEnabled = _formsMap.IsShowingUser;

                // marker click event to edit the faults
                _map.MarkerClick += _map_MarkerClick;

                foreach (var pin in _formsMap.ListOfPins)
                {
                    if (pin != null)
                    {
                        var markerIcon = new MarkerOptions();
                        markerIcon.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
                        markerIcon.SetTitle(pin.Label);
                        markerIcon.SetSnippet(pin.Address);

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
        }

        /// <summary>
        /// Handles the Marker Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            ((MapExtension)Element).OnPinTap(ConvertMarkerToPin(e.Marker));
        }

        /// <summary>
        /// Sets the fault image in the custom InfoWindow if available
        /// </summary>
        private void SetFaultImage(Android.Widget.ImageView faultImageView, Marker selectedMarker)
        {
            // TODO: query the database for this current marker's bitmap fault image
            //faultImageView.SetImageBitmap()
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

                _map.MapLongClick -= googleMap_MapLongClick;
            }

            base.OnElementChanged(e);

            if (Control != null)
            {
                // get the maps

                _formsMap = (MapExtension)e.NewElement;
                ((MapView)Control).GetMapAsync(this);
            }
        }

        private Pin ConvertMarkerToPin(Marker _marker)
        {
            return new Pin
            {
                Position = new Position(_marker.Position.Latitude, _marker.Position.Longitude)
            };
        }

        private MarkerOptions CreateMarkerFromPCLPin(Pin customPin)
        {
            var markerIcon = new MarkerOptions();
            markerIcon.SetPosition(new LatLng(customPin.Position.Latitude, customPin.Position.Longitude));
            markerIcon.SetTitle(customPin.Label);
            markerIcon.SetSnippet(customPin.Address);
            markerIcon.Draggable(false);

            return markerIcon;
        }

        private void UpdatePins()
        {
            if (_map != null)
            {
                _map.Clear();

                foreach (var pin in _formsMap.ListOfPins)
                {
                    _map.AddMarker(CreateMarkerFromPCLPin(pin));
                }
            }
        }

        private void googleMap_MapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            ((MapExtension)Element).OnMapLongTouch(new Position(e.Point.Latitude, e.Point.Longitude));

            // display the new pin
            //UpdatePins();
        }
    }
}