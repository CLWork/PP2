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
            bool replay = false;
            while (programIsRunning)
            {
                for (int i = 1; i < 11; i++)
                {
                    Console.WriteLine($"Round {i}.");
                    string playerIcon = instance.ChooseYourIcon(gameChoices);
                    int computerChoice = instance.ComputerChoice(gameChoices);
                    string computerIcon = gameChoices[computerChoice];
                    instance.ChooseWinner(playerIcon, computerIcon, replay);

                    
                }

                //if (i == 10)
                //{
                //    Console.WriteLine("Play Again? Y/N");
                //    string input = Console.ReadLine().ToLower();
                //    switch (input)
                //    {
                //        case "y":
                //        case "yes":
                //            {
                //                replay = true;
                //                break;
                //            }
                //        case "n":
                //        case "no":
                //            {
                //                programIsRunning = false;
                //                break;
                //            }
                //    }
            }
        }
       

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

        int ComputerChoice(List<string> _gameChoices)
        {
            Random rndm = new Random();
            int randomIcon = rndm.Next(0, 4);
            return randomIcon;
            
        }
        
        void ChooseWinner(string _playerIcon, string _computerIcon, bool _replay)
        {
            int computerWin = 0;
            int playerWin = 0;
            int tie = 0;

            if (_playerIcon == "rock" && _computerIcon == "rock")
            {
                tie += tie + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "paper" && _computerIcon == "paper")
            {
                tie += 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "scissors" && _computerIcon == "scissors")
            {
                tie += tie + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "lizard" && _computerIcon == "lizard")
            {
                tie += tie + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "spock" && _computerIcon == "spock")
            {
                tie += tie + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nTie!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "rock" && _computerIcon == "paper")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            }
            else if (_playerIcon == "paper" && _computerIcon == "rock")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "rock" && _computerIcon == "scissors")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "scissors" && _computerIcon == "rock")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            }
            else if (_playerIcon == "rock" && _computerIcon == "lizard")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            }
            else if (_playerIcon == "lizard" && _computerIcon == "rock")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            }
            else if (_playerIcon == "rock" && _computerIcon == "spock")
            {
                computerWin = computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "spock" && _computerIcon == "rock")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "paper" && _computerIcon == "scissors")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "scissors" && _computerIcon == "paper")
            {
                playerWin = playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "paper" && _computerIcon == "lizard")
            {
                computerWin = computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "lizard" && _computerIcon == "paper")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "paper" && _computerIcon == "spock")
            {
                playerWin = playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "spock" && _computerIcon == "paper")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "scissors" && _computerIcon == "lizard")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if (_playerIcon == "lizard" && _computerIcon == "scissors")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "scissors" && _computerIcon == "spock")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if(_playerIcon == "spock" && _computerIcon == "scissors")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else if (_playerIcon == "lizard" && _computerIcon == "spock")
            {
                playerWin += playerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nPlayer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");

            } else if(_playerIcon == "spock" && _computerIcon == "lizard")
            {
                computerWin += computerWin + 1;
                Console.WriteLine($"Player Chose: {_playerIcon} Computer Chose: {_computerIcon}\r\nComputer wins!\r\n Player Wins: {playerWin} Ties: {tie} Computer Wins: {computerWin}");
            }
            else
            {
                Console.WriteLine("Pairing not compatiable.");
            }
            


        }
    }
}
