using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Card> Deck { get; set; } = NewDeck();
        public ObservableCollection<Card> PlayerHand { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> DealerHand { get; set; } = new ObservableCollection<Card>();
        public static bool isActive;
        public int DealerHandTotal = 0;
        public int PlayerHandTotal = 0;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<Card> Shuffle(ObservableCollection<Card> deck)
        {
            Random rand = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                int j = rand.Next(deck.Count);
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
            return deck;
        }
        public void StartNewGame()
        {
            isActive = true;
            Deck = Shuffle(NewDeck());
            PlayerHand.Clear();
            DealerHand.Clear();
            DealPlayer();
            DealPlayer();
            DealDealer();
        }

        public void DealDealer()
        {
            do
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                DealerHand.Add(card);
                DealerHandTotal = CalculateHandTotal(DealerHand);
            } while (DealerHand.Count < 2);
            return;
        }

        public void DealPlayer()
        {
            if (Deck.Count > 0)
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                PlayerHand.Add(card);
                PlayerHandTotal = CalculateHandTotal(PlayerHand);
            }
            return;
        }

        public static ObservableCollection<Card> NewDeck()
        {
            ObservableCollection<Card> deck = new ObservableCollection<Card>();
            string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add(new Card(suit, rank));
                }
            }
            return deck;
        }


        public int CalculateHandTotal(ObservableCollection<Card> hand)
        {
            int total = 0;
            int aceCount = 0;
            foreach (Card card in hand)
            {
                total += card.Value;
                if (card.Rank == "Ace")
                {
                    aceCount++;
                }
            }
            while (aceCount > 0 && total <= 11) // if total is 11 or less (really it is 10 or less, but I'm counting the 'set' values first
            {
                total += 10; // counting aces as 11 instead of 1 (adding 10, already counted the 1 when iterating over the collection)
                aceCount--;
            } 
            return total;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonHit_Click(object sender, RoutedEventArgs e)
        {
            DealPlayer();
        }

        private void ButtonNewRound_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
        private void ButtonOpenDeckInspector_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}