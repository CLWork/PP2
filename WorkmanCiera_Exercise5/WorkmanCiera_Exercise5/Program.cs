using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkmanCiera_Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Ciera Workman
             * Project & Portfolio 2
             * Exercise 5
             * Rock Paper Scissors Lizard Spock
             */
            Program instance = new Program();
            List<string> gameChoices = instance.PopulateList();
            Console.WriteLine("------ Welcome! ------");
            instance.DisplayInstructions();

            bool programIsRunning = true;
            
            //Allows game to loop until player decides to no longer play.
            while (programIsRunning)
            {
                //Variables to keep track of scores.
                int playerWins = 0;
                int computerWins = 0;
                int ties = 0;
                //Game loops for 10 rounds.
                for (int i = 1; i < 11; i++)
                {
                    Console.WriteLine($"Round {i}.");
                    string playerIcon = instance.ChooseYourIcon(gameChoices);
                    int computerChoice = instance.ComputerChoice(gameChoices);
                    string computerIcon = gameChoices[computerChoice];
                    string whoWins = instance.ChooseWinner(playerIcon, computerIcon);
                    if (whoWins == "player")
                    {
                        playerWins = playerWins + 1;
                    } else if (whoWins == "computer")
                    {
                        computerWins = computerWins + 1;
                        
                    } else if (whoWins == "tie")
                    {
                        ties = ties + 1;
                    }
                    else
                    {
                        //Handles unexpected output.
                        Console.WriteLine("Not a valid round.");
                    }

                }

                //Outputs total wins to the user. Asks if they are to replay or not.
                Console.WriteLine($"Player Wins: {playerWins} - Ties: {ties} - Computer Wins: {computerWins}");
                Console.WriteLine("Replay? Y/N");
                string replayGame = Console.ReadLine().ToLower();
                switch (replayGame)
                {
                    case "y":
                    case "yes":
                        {
                            //Resets the score keepers.
                            playerWins = 0;
                            computerWins = 0;
                            ties = 0;
                            break;
                        }
                    case "n":
                    case "no":
                        {
                            //Quits program
                            programIsRunning = false;
                            break;
                        }
                }
                

            }
        }
       
        //Displays the instructions at the beginning of the game.
        void DisplayInstructions()
        { 
            Console.WriteLine("How to Play: Choose one of the following - Rock, Paper, Scissors, Lizard, Spock.");
            Console.WriteLine("Rock beats Scissors.\r\n" +
                "Paper beats Rock.\r\n" +
                "Scisoors beats Paper.\r\n" +
                "Rock beats Lizard.\r\n" +
                "Lizard beats Spock.\r\n" +
                "Scissors beats Lizard.\r\n" +
                "Lizard beats Paper.\r\n" +
                "Paper beats Spock.\r\n" +
                "Spock beats Rock.\r\n" +
                "Game Length: 10 Rounds\r\n");
        }

        //Populates the list of icons the player and computer have to choose from.
        List<string> PopulateList()
        {
            List<string> _gameChoices = new List<string>();
            string rock = "rock";
            _gameChoices.Add(rock);
            string scissors = "scissors";
            _gameChoices.Add(scissors);
            string paper = "paper";
            _gameChoices.Add(paper);
            string lizard = "lizard";
            _gameChoices.Add(lizard);
            string spock = "spock";
            _gameChoices.Add(spock);

            return _gameChoices;
        }

        //Allows the user to choose their icon/game piece.
        string ChooseYourIcon(List<string> _gameChoices)
        {
            string _playerIcon = null;

            Console.WriteLine("Player, Select Your Icon:");
            Console.WriteLine("1. Rock\r\n" +
                "2. Paper\r\n" +
                "3. Scissors\r\n" +
                "4. Lizard\r\n" +
                "5. Spock\r\n");
            string input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "1":
                case "rock":
                    {
                        _playerIcon = _gameChoices[0];
                        
                        return _playerIcon;
                    }
                case "2":
                case "paper":
                    {
                        _playerIcon = _gameChoices[1];
                        return _playerIcon;
                    }
                case "3":
                case "scissors":
                    {
                        _playerIcon = _gameChoices[2];
                        return _playerIcon;
                    }
                case "4":
                case "lizard":
                    {
                        _playerIcon = _gameChoices[3];
                        return _playerIcon;
                    }
                case "5":
                case "spock":
                    {
                        _playerIcon = _gameChoices[4];
                        return _playerIcon;
                    }
                default:
                    Console.WriteLine($"Your entry of: {input} is invalid. Please try again.");
                    return _playerIcon;
                    
            }
            

        }

        //Computer chooses their icon/game piece.
        int ComputerChoice(List<string> _gameChoices)
        {
            Random rndm = new Random();
            int randomIcon = rndm.Next(0, 4);
            return randomIcon;
            
        }
        
        //Takes the program through a series of If/If Else statements to determine the winner.
        string ChooseWinner(string _playerIcon, string _computerIcon)
        {
            string winner = null;

            if (_playerIcon == "rock" && _computerIcon == "rock")
            {
                winner = "tie";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!");
                return winner;
            }
            else if (_playerIcon == "paper" && _computerIcon == "paper")
            {
                winner = "tie";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!");
                return winner;

            } else if (_playerIcon == "scissors" && _computerIcon == "scissors")
            {
                winner = "tie";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!");
                return winner;

            } else if (_playerIcon == "lizard" && _computerIcon == "lizard")
            {
                winner = "tie";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!");
                return winner;
            }
            else if (_playerIcon == "spock" && _computerIcon == "spock")
            {
                winner = "tie";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!");
                return winner;
            }
            else if (_playerIcon == "rock" && _computerIcon == "paper")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            }
            else if (_playerIcon == "paper" && _computerIcon == "rock")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;
            }
            else if (_playerIcon == "rock" && _computerIcon == "scissors")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;

            } else if (_playerIcon == "scissors" && _computerIcon == "rock")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            }
            else if (_playerIcon == "rock" && _computerIcon == "lizard")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;

            }
            else if (_playerIcon == "lizard" && _computerIcon == "rock")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            }
            else if (_playerIcon == "rock" && _computerIcon == "spock")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            } else if (_playerIcon == "spock" && _computerIcon == "rock")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;
            }
            else if (_playerIcon == "paper" && _computerIcon == "scissors")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            } else if (_playerIcon == "scissors" && _computerIcon == "paper")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;
            }
            else if (_playerIcon == "paper" && _computerIcon == "lizard")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            } else if (_playerIcon == "lizard" && _computerIcon == "paper")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;
            }
            else if (_playerIcon == "paper" && _computerIcon == "spock")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;

            } else if (_playerIcon == "spock" && _computerIcon == "paper")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;
            }
            else if (_playerIcon == "scissors" && _computerIcon == "lizard")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;

            } else if (_playerIcon == "lizard" && _computerIcon == "scissors")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;
            }
            else if (_playerIcon == "scissors" && _computerIcon == "spock")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;

            } else if(_playerIcon == "spock" && _computerIcon == "scissors")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;
            }
            else if (_playerIcon == "lizard" && _computerIcon == "spock")
            {
                winner = "player";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!");
                return winner;

            } else if(_playerIcon == "spock" && _computerIcon == "lizard")
            {
                winner = "computer";
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!");
                return winner;
            }
            else
            {
                winner = "none";
                Console.WriteLine("Pairing not compatiable.");
                return winner;
            }
            


        }
    }
}
