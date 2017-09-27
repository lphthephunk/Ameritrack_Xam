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

            if (_map != null)
            {
                _map.MapClick += googleMap_MapClick;
                _map.MyLocationEnabled = _formsMap.IsShowingUser;

                // marker click event to edit the faults
                _map.MarkerClick += _map_MarkerClick;

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


        /// <summary>
        /// This could be used to calculate which button is pressed on the pin popup NOT WORKING CURRENTLY UNUSED
        /// </summary>
        /// <param name="e"></param>
        //private void DetermineWhichButtonIsTouched(MotionEvent e)
        //{
        //    // TODO: find out why the buttons aren't giving off dimensions
        //    int leftOfEditBtn = EditPinBtn.Left;
        //    int topOfEditBtn = EditPinBtn.Top;

        //    Rect editBtnArea = new Rect(leftOfEditBtn, topOfEditBtn, leftOfEditBtn + EditPinBtn.Width, topOfEditBtn + EditPinBtn.Height);

        //    int rightOfDeleteBtn = ((Android.Views.View)(DeletePinBtn.Parent)).Right;
        //    int topOfDeleteBtn = ((Android.Views.View)(DeletePinBtn.Parent)).Top;

        //    Rect delteBtnArea = new Rect(rightOfDeleteBtn, topOfDeleteBtn, rightOfDeleteBtn + DeletePinBtn.Width, topOfDeleteBtn + DeletePinBtn.Height);

        //    if (editBtnArea.Contains((int)e.GetX(), (int)e.GetY()))
        //    {
        //        switch (e.ActionMasked)
        //        {
        //            case MotionEventActions.Down:
        //                System.Diagnostics.Debug.WriteLine("Edit button pressed");
        //                break;
        //            case MotionEventActions.Up:
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else if (delteBtnArea.Contains((int)e.GetX(), (int)e.GetY()))
        //    {
        //        switch (e.ActionMasked)
        //        {
        //            case MotionEventActions.Down:
        //                System.Diagnostics.Debug.WriteLine("Delete button pressed");
        //                break;
        //            case MotionEventActions.Up:
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

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

        private Pin ConvertMarkerToPin(Marker _marker)
        {
            return new Pin
            {
                Position = new Position(_marker.Position.Latitude, _marker.Position.Longitude)
            };
        }

        private MarkerOptions CreateMarkerFromCustomPin(Pin customPin)
        {
            var markerIcon = new MarkerOptions();
            markerIcon.SetPosition(new LatLng(customPin.Position.Latitude, customPin.Position.Longitude));
            markerIcon.SetTitle(customPin.Label);
            markerIcon.SetSnippet(customPin.Address);
            markerIcon.Draggable(true);

            return markerIcon;
        }

        private void UpdatePins()
        {
            if (_map != null)
            {
                _map.Clear();

                foreach (var pin in _formsMap.ListOfPins)
                {
                    _map.AddMarker(CreateMarkerFromCustomPin(pin));
                }
            }
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((MapExtension)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));

            // display the new pin
            UpdatePins();
        }
    }

    ///// <summary>
    ///// Adapter class used to inflate the custom pin popup window
    ///// </summary>
    //public class CustomMarkerPopupAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
    //{
    //    private LayoutInflater _layoutInflater = null;

    //    public CustomMarkerPopupAdapter(LayoutInflater inflater)
    //    {
    //        _layoutInflater = inflater;
    //    }

    //    public Android.Views.View GetInfoWindow(Marker marker)
    //    {
    //        return null;
    //    }

    //    public Android.Views.View GetInfoContents(Marker marker)
    //    {
    //        var customPopup = _layoutInflater.Inflate(Resource.Layout.MapInfo, null);

    //        //marker.Title = customPopup.FindViewById<TextView>(Resource.Id.PinDeletionText).Text;
            
    //        return customPopup;
    //    }
    //}
}