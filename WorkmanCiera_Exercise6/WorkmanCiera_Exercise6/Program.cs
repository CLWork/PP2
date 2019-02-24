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

            List<string> players = instance.NameThePlayers();
            bool programIsRunning = true;
            while (programIsRunning)
            {
                
                instance.GiveCardsToPlayers(cardDeck, players);

                Console.WriteLine("Replay Game With Same Players? (Y/N)");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "y":
                    case "yes":
                        {
                            cardDeck = instance.PopulateCards();
                            break;
                        }
                    case "n":
                    case "no":
                        {
                            programIsRunning = false;
                            break;
                        }
                    default:
                        Console.WriteLine($"Your entry of {input} was invalid, please try again.");
                        break;
                }


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

            for (int i = 0; i < 13; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerOneCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            for (int i = 0; i < 13; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerTwoCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            for (int i = 0; i < 13; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerThreeCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            for (int i = 0; i < 13; i++)
            {
                int rndmIndex = rmdm.Next(0, _cardDeck.Count);
                playerFourCards.Add(_cardDeck[rndmIndex]);
                _cardDeck.Remove(_cardDeck[rndmIndex]);
            }

            Console.Write($"{_players[0]}'s Cards: ");
            DisplayCards(playerOneCards);
           
            int playerOneTotal = GiveValueToCards(playerOneCards);
            Console.Write($" Total: {playerOneTotal}");
            
            Console.WriteLine("");

            Console.ResetColor();

            Console.Write($"{_players[1]}'s Cards: ");
            DisplayCards(playerTwoCards);
            int playerTwoTotal = GiveValueToCards(playerTwoCards);
            Console.Write($" Total: {playerTwoTotal}");

            Console.ResetColor();
            Console.WriteLine("");

            Console.Write($"{_players[2]}'s Cards: ");
            DisplayCards(playerThreeCards);
            int playerThreeTotal = GiveValueToCards(playerThreeCards);
            Console.Write($" Total: {playerThreeTotal}");

            Console.ResetColor();
            Console.WriteLine("");

            Console.Write($"{_players[3]}'s Cards: ");
            DisplayCards(playerFourCards);
            Console.ResetColor();
            int playerFourTotal = GiveValueToCards(playerFourCards);
            Console.Write($" Total: {playerFourTotal}");

            Console.WriteLine("");
            int scoreChecker = playerOneTotal + playerTwoTotal + playerThreeTotal + playerFourTotal;
            Console.WriteLine($"Score Checker: {scoreChecker}");
        }
        void DisplayCards(List<string> _playerCards)
        {
            for (int i = 0; i < _playerCards.Count; i++)
            {

                if (_playerCards[i] == "K♥" || _playerCards[i] == "Q♥" || _playerCards[i] == "J♥" || _playerCards[i] == "2♥" || _playerCards[i] == "3♥" || _playerCards[i] == "4♥" || _playerCards[i] == "5♥" || _playerCards[i] == "6♥" || _playerCards[i] == "7♥" || _playerCards[i] == "8♥" ||
                    _playerCards[i] == "9♥" || _playerCards[i] == "10♥" || _playerCards[i] == "A♥" || _playerCards[i] == "K♦" || _playerCards[i] == "Q♦" || _playerCards[i] == "J♦" || _playerCards[i] == "A♦" || _playerCards[i] == "2♦" || _playerCards[i] == "3♦" || _playerCards[i] == "4♦" ||
                    _playerCards[i] == "5♦" || _playerCards[i] == "6♦" || _playerCards[i] == "7♦" || _playerCards[i] == "8♦" || _playerCards[i] == "9♦" || _playerCards[i] == "10♦")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {_playerCards[i]} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {_playerCards[i]} ");
                    Console.ResetColor();
                }
            }
        }
        int GiveValueToCards(List<string> _playerCards)
        {
            int playerTotal = 0;
            for (int i = 0; i < _playerCards.Count; i++)
            {
                if (_playerCards[i] == "K♥" || _playerCards[i] == "Q♥" || _playerCards[i] == "J♥" || _playerCards[i] == "K♦" || _playerCards[i] == "Q♦" 
                    || _playerCards[i] == "J♦" || _playerCards[i] == "K♠" || _playerCards[i] == "Q♠" || _playerCards[i] == "J♠"
                    || _playerCards[i] == "K♣" || _playerCards[i] == "Q♣" || _playerCards[i] == "J♣")
                {
                    playerTotal = playerTotal + 12;
                    
                }
                else if (_playerCards[i] == "A♥" || _playerCards[i] == "A♦" || _playerCards[i] == "A♠" || _playerCards[i] == "A♣")
                {
                    playerTotal = playerTotal + 15;
   
                }
                else if (_playerCards[i] == "2♥" || _playerCards[i] == "2♦" || _playerCards[i] == "2♠" || _playerCards[i] == "2♣")
                {
                    playerTotal = playerTotal + 2;
                    
                }
                else if (_playerCards[i] == "3♥" || _playerCards[i] == "3♦" || _playerCards[i] == "3♠" || _playerCards[i] == "3♣")
                {
                    playerTotal = playerTotal + 3;
                    
                }
                else if (_playerCards[i] == "4♥" || _playerCards[i] == "4♦" || _playerCards[i] == "4♠" || _playerCards[i] == "4♣")
                {
                    playerTotal = playerTotal + 4;
                    
                }
                else if (_playerCards[i] == "5♥" || _playerCards[i] == "5♦" || _playerCards[i] == "5♠" || _playerCards[i] == "5♣")
                {
                    playerTotal = playerTotal + 5;
                }
                else if (_playerCards[i] == "6♥" || _playerCards[i] == "6♦" || _playerCards[i] == "6♠" || _playerCards[i] == "6♣")
                {
                    playerTotal = playerTotal + 6;

                }
                else if (_playerCards[i] == "7♥" || _playerCards[i] == "7♦" || _playerCards[i] == "7♠" || _playerCards[i] == "7♣")
                {
                    playerTotal = playerTotal + 7;

                }
                else if (_playerCards[i] == "8♥" || _playerCards[i] == "8♦" || _playerCards[i] == "8♠" || _playerCards[i] == "8♣")
                {
                    playerTotal = playerTotal + 8;

                }
                else if (_playerCards[i] == "9♥" || _playerCards[i] == "9♦" || _playerCards[i] == "9♠" || _playerCards[i] == "9♣")
                {
                    playerTotal = playerTotal + 9;

                }
                else if (_playerCards[i] == "10♥" || _playerCards[i] == "10♦" || _playerCards[i] == "10♠" || _playerCards[i] == "10♣")
                {
                    playerTotal = playerTotal + 10;

                }
                
                
            }

            return playerTotal;
        }
    }
}
