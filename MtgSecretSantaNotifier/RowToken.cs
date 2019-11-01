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
        public const int ZipCode = 8;
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
            return AttributeList.Where(x => x.Item1 == propertyName).FirstOrDefault()?.Item2;
        }

        public static readonly List<Tuple<String,int>> AttributeList = new List<Tuple<String, int>>()
        {
            new Tuple<string,int>("Id",0 ),
            new Tuple<string,int>("GifterId",1),
            new Tuple<string,int>("Email",2),
            new Tuple<string,int>("RedditName",3),
            new Tuple<string,int>("Name",4),
            new Tuple<string,int>("Address",5),
            new Tuple<string,int>("City",6),
            new Tuple<string,int>("State",7),
            new Tuple<string,int>("ZipCode",8),
            new Tuple<string,int>("Country",9),
            new Tuple<string,int>("Country2",10),
            new Tuple<string,int>("ContactMeathod",11),
            new Tuple<string,int>("MilitaryStatus",12),
            new Tuple<string,int>("NewPlayerStatus",13),
            new Tuple<string,int>("ShirtSize",14),
            new Tuple<string,int>("CurrentDecksPlayed",15),
            new Tuple<string,int>("PlayerProfile",16),
            new Tuple<string,int>("Commander",17),
            new Tuple<string,int>("FavoriteColors",18),
            new Tuple<string,int>("FavoriteCharacter",19),
            new Tuple<string,int>("PreferredSleeveColorBrand",20),
            new Tuple<string,int>("FavoriteArtist",21),
            new Tuple<string,int>("CardVersionPreferrences",22),
            new Tuple<string,int>("FavoriteDraftFormat",23),
            new Tuple<string,int>("FavoriteSet",24),
            new Tuple<string,int>("MostUsedTokens",25),
            new Tuple<string,int>("WhiteWhale",26),
            new Tuple<string,int>("Projects",27),
            new Tuple<string,int>("SoughtSingles",28),
            new Tuple<string,int>("HomemadeGiftsBoolean",29),
            new Tuple<string,int>("WhatNotToGet",30),
            new Tuple<string,int>("AdditionalRequests",31),
            new Tuple<string,int>("OtherInterests",32),
            new Tuple<string,int>("ShipOutOfCountry",33),
            new Tuple<string,int>("GifteePreferrencesBoolean",34),
            new Tuple<string,int>("GifteePreferrencesString",35),
            new Tuple<string,int>("VolunteerGifterBoolean",36),
        };
    }
}
