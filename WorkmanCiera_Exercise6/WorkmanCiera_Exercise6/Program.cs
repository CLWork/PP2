using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
 



namespace WorkmanCiera_Exercise6
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            /*Ciera Workman
             * Project & Portfolio 2
             * Card Game
             */
            Program instance = new Program();
            List<string> cardDeck = instance.PopulateCards();

            instance.DisplayInstructions();

            bool programIsRunning = true;
            while (programIsRunning)
            {
                List<string> players = instance.NameThePlayers();
                instance.GiveCardsToPlayers(cardDeck, players);


                Utility.PauseBeforeContinuing();
            }


        }
        void DisplayInstructions()
        {
            Console.WriteLine("Players are to be dealt cards from the deck. Once all cards are dealt, the player with the most points win.");
        }
        List<string> NameThePlayers()
        {
            List<string> _players = new List<string>();

            string player1 = Validation.StringValidation("Enter Player One's name: ");
            _players.Add(player1);
            string player2 = Validation.StringValidation("Enter Player Two's name: ");
            _players.Add(player2);
            string player3 = Validation.StringValidation("Enter Player Three's name: ");
            _players.Add(player3);
            string player4 = Validation.StringValidation("Enter Player Four's name: ");
            _players.Add(player4);

            return _players;
        }
        List<string> PopulateCards()
        {
            List<string> _cardDeck = new List<string>();

            string kingHearts = "K♥";
            string queenHearts = "Q♥";
            string jackHearts = "J♥";
            string aceHearts = "A♥";
            string twoHearts = "2♥";
            string threeHearts = "3♥";
            string fourHearts = "4♥";
            string fiveHearts = "5♥";
            string sixHearts = "6♥";
            string sevenHearts = "7♥";
            string eightHearts = "8♥";
            string nineHearts = "9♥";
            string tenHearts = "10♥";

            _cardDeck.Add(kingHearts);
            _cardDeck.Add(queenHearts);
            _cardDeck.Add(jackHearts);
            _cardDeck.Add(aceHearts);
            _cardDeck.Add(twoHearts);
            _cardDeck.Add(threeHearts);
            _cardDeck.Add(fourHearts);
            _cardDeck.Add(fiveHearts);
            _cardDeck.Add(sixHearts);
            _cardDeck.Add(sevenHearts);
            _cardDeck.Add(eightHearts);
            _cardDeck.Add(nineHearts);
            _cardDeck.Add(tenHearts);

            string kingDiamonds = "K♦";
            string queenDiamonds = "Q♦";
            string jackDiamonds = "J♦";
            string aceDiamonds = "A♦";
            string twoDiamonds = "2♦";
            string threeDiamonds = "3♦";
            string fourDiamonds = "4♦";
            string fiveDiamonds = "5♦";
            string sixDiamonds = "6♦";
            string sevenDiamonds = "7♦";
            string eightDiamonds = "8♦";
            string nineDiamonds = "9♦";
            string tenDiamonds = "10♦";

            _cardDeck.Add(kingDiamonds);
            _cardDeck.Add(queenDiamonds);
            _cardDeck.Add(jackDiamonds);
            _cardDeck.Add(aceDiamonds);
            _cardDeck.Add(twoDiamonds);
            _cardDeck.Add(threeDiamonds);
            _cardDeck.Add(fourDiamonds);
            _cardDeck.Add(fiveDiamonds);
            _cardDeck.Add(sixDiamonds);
            _cardDeck.Add(sevenDiamonds);
            _cardDeck.Add(eightDiamonds);
            _cardDeck.Add(nineDiamonds);
            _cardDeck.Add(tenDiamonds);

            string kingSpades = "K♠";
            string queenSpades = "Q♠";
            string jackSpades = "J♠";
            string aceSpades = "A♠";
            string twoSpades = "2♠";
            string threeSpades = "3♠";
            string fourSpades = "4♠";
            string fiveSpades = "5♠";
            string sixSpades = "6♠";
            string sevenSpades = "7♠";
            string eightSpades = "8♠";
            string nineSpades = "9♠";
            string tenSpades = "10♠";

            _cardDeck.Add(kingSpades);
            _cardDeck.Add(queenSpades);
            _cardDeck.Add(jackSpades);
            _cardDeck.Add(aceSpades);
            _cardDeck.Add(twoSpades);
            _cardDeck.Add(threeSpades);
            _cardDeck.Add(fourSpades);
            _cardDeck.Add(fiveSpades);
            _cardDeck.Add(sixSpades);
            _cardDeck.Add(sevenSpades);
            _cardDeck.Add(eightSpades);
            _cardDeck.Add(nineSpades);
            _cardDeck.Add(tenSpades);

            string kingClubs = "K♣";
            string queenClubs = "Q♣";
            string jackClubs = "J♣";
            string aceClubs = "A♣";
            string twoClubs = "2♣";
            string threeClubs = "3♣";
            string fourClubs = "4♣";
            string fiveClubs = "5♣";
            string sixClubs = "6♣";
            string sevenClubs = "7♣";
            string eightClubs = "8♣";
            string nineClubs = "9♣";
            string tenClubs = "10♣";

            _cardDeck.Add(kingClubs);
            _cardDeck.Add(queenClubs);
            _cardDeck.Add(jackClubs);
            _cardDeck.Add(aceClubs);
            _cardDeck.Add(twoClubs);
            _cardDeck.Add(threeClubs);
            _cardDeck.Add(fourClubs);
            _cardDeck.Add(fiveClubs);
            _cardDeck.Add(sixClubs);
            _cardDeck.Add(sevenClubs);
            _cardDeck.Add(eightClubs);
            _cardDeck.Add(nineClubs);
            _cardDeck.Add(tenClubs);

            return _cardDeck;

        }
        void GiveCardsToPlayers(List<string> _cardDeck, List<string> _players)
        {
            List<string> playerOneCards = new List<string>();
            List<string> playerTwoCards = new List<string>();
            List<string> playerThreeCards = new List<string>();
            List<string> playerFourCards = new List<string>();
            Random rmdm = new Random();

            for (int i = 0; i < 14; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerOneCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            for (int i = 0; i < 14; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerTwoCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            for (int i = 0; i < 14; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerThreeCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            Console.Write($"{_players[0]}'s Cards: ");
            for (int i = 0; i < playerOneCards.Count; i++)
            {
                Console.Write(playerOneCards[i] + " ");
            }
            Console.WriteLine("");

            Console.Write($"{_players[1]}'s Cards: ");
            for (int i = 0; i < playerTwoCards.Count; i++)
            {
                Console.Write(playerTwoCards[i] + " ");
            }

            Console.WriteLine("");

            Console.Write($"{_players[2]}'s Cards: ");
            for (int i = 0; i < playerThreeCards.Count; i++)
            {
                Console.Write(playerThreeCards[i] + " ");
            }
        }
        void DisplayCards(List<string> _playerCards)
        {
            for (int i = 0; i < _playerCards.Count; i++)
            {
                if (_playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" ||
                    _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" ||
                    _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "" || _playerCards[i] == "")
                {

                }
            }
        }
    }
}
