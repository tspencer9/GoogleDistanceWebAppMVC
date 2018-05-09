using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GoogleDistanceWebAppMVC.ViewModels
{
    public class DistanceRoute
    {
        [Required(ErrorMessage = "A valid address or place must be entered.")]
        public string Location1 { get; set; }

        [Required(ErrorMessage = "A valid address or place must be entered.")]
        public string Location2 { get; set; }

        public string Distance { get; set; }
        public string Duration { get; set; }

        private string BuildUrlForLocationId(string location)
        {
            string locationForUrl = "";
            string[] locationAsArray;
            locationAsArray = (location.Split());

            for (int i = 0; i < locationAsArray.Length; i++)
            {
                if (i < locationAsArray.Length - 1)
                    locationForUrl += locationAsArray[i] + "+";
                else
                    locationForUrl += locationAsArray[i];
            }

            return "https://maps.googleapis.com/maps/api/place/textsearch/json?key=AIzaSyAe19SG-FjgoyLwiZFXo3V5cWFXO_KKsrg&query=" + locationForUrl;
        }

        private string BuildUrlForDistance(string id1, string id2)
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?key=AIzaSyAMxfjuYn6x00PmtahL7skkas0vnQqZblA&units=imperial&origins=";
            return url + "place_id:" + id1 + "&destinations=place_id:" + id2;
        }

        public async void FindDistance()
        {
            string[] locationUrls = { BuildUrlForLocationId(this.Location1), BuildUrlForLocationId(this.Location2) },
                idLocations = new string[2];
            HttpClient http = new HttpClient();

            for (int i = 0; i < idLocations.Length; i++)
            {
                var responseId = await http.GetAsync(locationUrls[i]);

                if (responseId.IsSuccessStatusCode)
                {
                    var result = responseId.Content.ReadAsStringAsync();
                    RootLocationBase root = JsonConvert.DeserializeObject<RootLocationBase>(result.Result);
                    try
                    {
                        idLocations[i] = root.results[0].place_id;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }

            var responseDistance = await http.GetAsync(BuildUrlForDistance(idLocations[0], idLocations[1]));

            if (responseDistance.IsSuccessStatusCode)
            {
                var result = responseDistance.Content.ReadAsStringAsync().Result;
                RootDistanceBase root = JsonConvert.DeserializeObject<RootDistanceBase>(result);
                this.Distance = root.rows[0].elements[0].distance.text;
                this.Duration = root.rows[0].elements[0].duration.text;
            }
        }
    }
}