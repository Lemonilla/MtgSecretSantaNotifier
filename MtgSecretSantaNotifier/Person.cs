using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    public class Person
    {
        public string SantaEmail { get; set; }


        public string Id { get; set; }
        public string GifterId { get; set; }
       // Your Info
        public string Email { get; set; }
        public string RedditName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Country2 { get; set; }
        public string ContactMeathod { get; set; }
        public string MilitaryStatus { get; set; }
        public string NewPlayerStatus { get; set; }
        // Interests
        public string ShirtSize { get; set; }
        public string CurrentDecksPlayed { get; set; }
        public string PlayerProfile { get; set; }
        public string Commander { get; set; }
        public string FavoriteColors { get; set; }
        public string FavoriteCharacter { get; set; }
        public string PreferredSleeveColorBrand { get; set; }
        public string FavoriteArtist { get; set; }
        public string CardVersionPreferrences { get; set; }
        public string FavoriteDraftFormat { get; set; }
        public string FavoriteSet { get; set; }
        public string MostUsedTokens { get; set; }
        public string WhiteWhale { get; set; }
        public string Projects { get; set; }
        public string SoughtSingles { get; set; }
        public string HomemadeGiftsBoolean { get; set; }
        public string WhatNotToGet { get; set; }
        public string AdditionalRequests { get; set; }
        public string OtherInterests { get; set; }
        // Shipping Pref 
        public string ShipOutOfCountry { get; set; }
        public string GifteePreferrencesBoolean { get; set; }
        public string GifteePreferrencesString { get; set; }
        public string VolunteerGifterBoolean { get; set; }


        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
