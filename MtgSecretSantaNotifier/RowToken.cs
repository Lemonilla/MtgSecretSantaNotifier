using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgSecretSantaNotifier
{
    /// <summary>
    /// Defines the location of each piece of data within a row.
    /// </summary>
    public class RowToken
    {
        // Row indexies

        public const int Id = 1;
        public const int GifterId = 2;
        // Your Info
        public const int Email = 3;
        public const int RedditName = 5;
        public const int Name = 4;
        public const int Address = 5;
        public const int City = 6;
        public const int State = 7;
        public const int Zipcode = 8;
        public const int Country = 9;
        public const int Country2 = 10;
        public const int ContactMeathod = 11;
        public const int MilitaryStatus = 12;
        public const int NewPlayerStatus = 13;
        // Interests
        public const int ShirtSize = 14; // What's your T-Shirt size?
        public const int CurrentDecksPlayed = 15; // What formats/decks do you play?
        public const int PlayerProfile = 16; // What 'profile' are you?
        public const int Commander = 17; // Who's your commander?
        public const int FavoriteColors = 18; // What's your favorite color/color combo?
        public const int FavoriteCharacter = 19; // Who's your favorite character?
        public const int PreferredSleeveColorBrand = 20; // Preferred sleeve brand/color?
        public const int FavoriteArtist = 21; // Who's your favorite artist?
        public const int CardVersionPreferrences = 22; // Do you like foils/alters/foreign cards?
        public const int FavoriteDraftFormat = 23; // What's your favorite draft format?
        public const int FavoriteSet = 24; // What's your favorite set or block?
        public const int MostUsedTokens = 25; // What are your most used tokens?
        public const int WhiteWhale = 26; // What's your White Whale, something you've been after a long time?
        public const int Projects = 27; // What projects are you trying to complete?
        public const int SoughtSingles = 28; // A there any particular singles you're looking for?
        public const int HomemadeGiftsBoolean = 29; // Do you like handmade gifts?
        public const int WhatNotToGet = 30; // Is there anything you don't want?
        public const int AdditionalRequests = 31; // Any other requests?
        public const int OtherInterests = 32; // What are your interests other than magic?
        // Shipping Pref
        public const int ShipOutOfCountry = 33;
        public const int GifteePreferrencesBoolean = 34;
        public const int GifteePreferrencesString = 35;
        public const int VolunteerGifterBoolean = 36;
        
        public static object GetRowToken(string propertyName)
        {
            return AttributeList.Where(x => x.Name == propertyName).FirstOrDefault()?.Row;
        }
        
        public static readonly List<Attribute> AttributeList = new List<Attribute>()
        {
            new Attribute("Id",0 ),
            new Attribute("GifterId",1),
            new Attribute("Email",2),
            new Attribute("RedditName",3),
            new Attribute("Name",4),
            new Attribute("Address",5),
            new Attribute("City",6),
            new Attribute("State",7),
            new Attribute("Zipcode",8),
            new Attribute("Country",9),
            new Attribute("Country2",10),
            new Attribute("ContactMeathod",11),
            new Attribute("MilitaryStatus",12),
            new Attribute("NewPlayerStatus",13),
            new Attribute("ShirtSize",14),
            new Attribute("CurrentDecksPlayed",15),
            new Attribute("PlayerProfile",16),
            new Attribute("Commander",17),
            new Attribute("FavoriteColors",18),
            new Attribute("FavoriteCharacter",19),
            new Attribute("PreferredSleeveColorBrand",20),
            new Attribute("FavoriteArtist",21),
            new Attribute("CardVersionPreferrences",22),
            new Attribute("FavoriteDraftFormat",23),
            new Attribute("FavoriteSet",24),
            new Attribute("MostUsedTokens",25),
            new Attribute("WhiteWhale",26),
            new Attribute("Projects",27),
            new Attribute("SoughtSingles",28),
            new Attribute("HomemadeGiftsBoolean",29),
            new Attribute("WhatNotToGet",30),
            new Attribute("AdditionalRequests",31),
            new Attribute("OtherInterests",32),
            new Attribute("ShipOutOfCountry",33),
            new Attribute("GifteePreferrencesBoolean",34),
            new Attribute("GifteePreferrencesString",35),
            new Attribute("VolunteerGifterBoolean",36),
        };
    }
}
