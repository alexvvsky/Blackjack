using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    public class Card
    {
        public string Suit { get; init; } // Clubs, Diamonds, Hearts, Spades
        public string Rank { get; init; } // Ace, 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King
        public int Value { get; } // 1/11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10
        public string Name { get; } // e.g. "Ace of Spades", "10 of Hearts", "Queen of Diamonds"
        public BitmapImage CardImage { get; }
        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
            Value = GetValue();
            Name = $"{rank} of {suit}";
            CardImage = (BitmapImage)new ImageConverter().Convert(this);
        }
        
        private int GetValue()
        {
            return Rank switch
            {
                "Ace" => 1, // Ace can be 1 or 11, handled during scoring
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                "8" => 8,
                "9" => 9,
                _ => 10 // for 10, Jack, Queen, King
            };
        }
    }
}
