﻿using System;
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
using Android.Gms.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps.Model;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MapExtension), typeof(CustomMapRenderer))]
namespace Ameritrack_Xam.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;
        private MapExtension _formsMap;

        //private List<Ameritrack_Xam.PCL.Models.CustomPin> PinsList = new List<PCL.Models.CustomPin>();

        private List<Pin> PinsList = new List<Pin>();

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

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

            UpdatePins();
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
            UpdatePins();
            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(e.Point.Latitude, e.Point.Longitude)
            //};

            //PinsList.Add(pin);
        }
    }
}