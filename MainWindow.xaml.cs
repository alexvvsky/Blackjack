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
        private DeckInspector? _inspector;
        private ObservableCollection<Card> _Deck;
        public ObservableCollection<Card> Deck
        {
            get => _Deck;
            set
            {
                if (_Deck != null)
                {
                    _Deck.CollectionChanged -= OnCollectionChanged;
                }
                _Deck = value;
                if (_Deck != null)
                {
                    _Deck.CollectionChanged += OnCollectionChanged;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(CardCount));
            }
        }
        public int CardCount => Deck?.Count ?? 0; // defaulting to 0 if null
        public ObservableCollection<Card> PlayerHand { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<Card> DealerHand { get; set; } = new ObservableCollection<Card>();
        private int _DealerHandTotal;
        public int DealerHandTotal
        {
            get => _DealerHandTotal;
            set
            {
                if (_DealerHandTotal != value)
                {
                    _DealerHandTotal = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _PlayerHandTotal;
        public int PlayerHandTotal
        {
            get => _PlayerHandTotal;
            set
            {
                if (_PlayerHandTotal != value)
                {
                    _PlayerHandTotal = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
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
            ButtonHit.IsEnabled = true;
            Deck = Shuffle(NewDeck());
            PlayerHand.Clear();
            DealerHand.Clear();
            PlayerHandTotal = DealPlayer();
            DealerHandTotal = DealDealer();
        }

        public int DealDealer()
        {
            do
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                DealerHand.Add(card);
            } while (DealerHand.Count < 2);
            return CalculateHandTotal(DealerHand);
        }

        public int DealPlayer()
        {
            if (PlayerHand.Count == 0)
            {
                do
                {
                    Card card = Deck[0];
                    Deck.RemoveAt(0);
                    PlayerHand.Add(card);
                } while (PlayerHand.Count < 2);
            }
            else
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                PlayerHand.Add(card);
            }
            return CalculateHandTotal(PlayerHand);
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

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CardCount));
        }

        private void ButtonHit_Click(object sender, RoutedEventArgs e)
        {
            PlayerHandTotal = DealPlayer();
        }

        private void ButtonNewRound_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
        private void ButtonOpenDeckInspector_Click(object sender, RoutedEventArgs e)
        {
            if (_inspector == null)
            {
                _inspector = new DeckInspector();
                _inspector.Closed += (closedSender, args) => _inspector = null;
                _inspector.Show();
                _inspector.DataContext = this;
            }
            else
            {
                _inspector.Activate();
            }
            
        }
        public void MoveItems(int oldIndex, int newIndex)
        {
            if (oldIndex < 0 || oldIndex >= Deck.Count || newIndex < 0 || newIndex >= Deck.Count)
                return;
            Deck.Move(oldIndex, newIndex);
        }


    }
}