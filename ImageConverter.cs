using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Blackjack
{

    public class ImageConverter : IValueConverter
    {
        // take Card object, return BitmapImage
        public object Convert(object value)
        {
            if (value is Card card)
            {
                // Clubs = 0 (King) to 12 (Queen)
                // Diamonds = 13 (King) to 25 (Queen)
                // Hearts = 26 (King) to 38 (Queen)
                // Spades = 39 (King) to 51 (Queen)
                int suitValue = card.Suit switch
                {
                    "Clubs" => 0,
                    "Diamonds" => 13,
                    "Hearts" => 26,
                    "Spades" => 39,
                    _ => -1 // error
                };
                int rankValue = card.Rank switch
                {
                    "King" => 0,
                    "Ace" => 1,
                    "2" => 2,
                    "3" => 3,
                    "4" => 4,
                    "5" => 5,
                    "6" => 6,
                    "7" => 7,
                    "8" => 8,
                    "9" => 9,
                    "10" => 10,
                    "Jack" => 11,
                    "Queen" => 12,
                    _ => -1 // error
                };
                string imageFilename = "Images/p" + (suitValue + rankValue) + ".png";
                BitmapImage cardImage = new BitmapImage(new Uri(imageFilename, UriKind.Relative));
                return cardImage;
            }
            return value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
