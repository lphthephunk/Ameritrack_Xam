using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Ameritrack_Xam.Pages.Views;
using Ameritrack_Xam.iOS;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Maps;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using MapKit;
using CoreGraphics;
using Ameritrack_Xam.PCL.Models;

[assembly: ExportRenderer(typeof(MapExtension), typeof(MapCustomRenderer))]
namespace Ameritrack_Xam.iOS
{
    public class MapCustomRenderer : MapRenderer
    {
        private List<Pin> PinsList = new List<Pin>();

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (MapExtension)e.NewElement;
                var nativeMap = Control as MKMapView;
                PinsList = formsMap.ListOfPins;

                //nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}