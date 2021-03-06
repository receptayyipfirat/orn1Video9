﻿using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace orn1Video9
{
   public class MapPage:ContentPage
    {
        private double Latitude;
        private double Longitude;
        public MapPage()
        {
            GetLocation();
        }
        private async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync();
            Latitude = position.Latitude;
            Longitude = position.Longitude;
            CreateMap();
        }
        void CreateMap()
        {
            Map currentMap = new Map
            {
                HasScrollEnabled = true,
                HasZoomEnabled = true,
                MapType = MapType.Street
            };
            Pin microsoftPin = new Pin
            {
                Type = PinType.Place,
                Address = "Microsoft Türkiye İstnabul",
                Label = "Microsoft Türkiye",
                Position = new Position(41.0707118, 29.01545114)
            };

            Pin haliSahaPin = new Pin
            {
                Type = PinType.SearchResult,
                BindingContext = "Saha",
                Label = "Halı Saha",
                Position = new Position()
            };

            currentMap.Pins.Add(microsoftPin);
            currentMap.Pins.Add(haliSahaPin);

            microsoftPin.Clicked += MicrosoftPin_Clicked;


            currentMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromKilometers(1)));

            Content = currentMap;
        }

        private void MicrosoftPin_Clicked(object sender, EventArgs e)
        {
            Pin selectedPin = (Pin)sender;
            DisplayAlert(selectedPin.Label, selectedPin.Address, "OK");

        }
    }
}
