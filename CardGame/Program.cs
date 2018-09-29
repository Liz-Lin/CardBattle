using System;

namespace CardGame
{
    class Player
    {
        int[] originalCards = new int[6];
        int[] currentCards = new int[6];
        int numCards = 6;
        int score = 12;

        public Player(int extra1, int extra2)
        {
            Random rnd = new Random();

            while (extra1 == extra2)
            {
                extra2 = rnd.Next(1, 5);
            }
            
            for (int i = 0;  i<4; ++i)
            {
                originalCards[i] = i+1;
                currentCards[i] = i+1;
            }
            originalCards[4] = extra1;
            currentCards[4] = extra1;
            originalCards[5] = extra2;
            currentCards[5] = extra2;
        }

        public int PlayCard ()
        {
            if (numCards == 1)
                Restore();
            Random rnd = new Random();
            int index = rnd.Next(numCards--);
            int card = currentCards[index];
            currentCards[index] = currentCards[numCards];
            return card;
        }

        public int PlayCardByChoice()
        {
            if (numCards == 1)
                Restore();

            bool done = false;
            int card=-1;
            while(!done)
            {
                card = PrintFullHandAndEnter();

                for (int i = 0; i < numCards; ++i)
                {
                    if (currentCards[i] == card)
                    {
                        currentCards[i] = currentCards[--numCards];
                        done = true;
                        break;
                    }
                }

            }
            return card;
        }
        public int PrintFullHandAndEnter()
        {
            Console.Write("Your Current Cards are: ");
            for (int i = 0; i < numCards; ++i)
                Console.Write(currentCards[i] + "\t");
            Console.Write("\nEnter your card please: ");
            int card = Convert.ToInt32(Console.ReadLine());
            return card;
        }
        public void Restore()
        {
            numCards = 6;
            for (int i = 0; i < numCards; ++i)
                currentCards[i] = originalCards[i];
        }

        public int calculateNewScore(int myCard, int theirCard)
        {
            if (myCard - theirCard >= 2 || theirCard - myCard == 1)
            {
                score -= theirCard;
            }
            return score;
        }

    }

    class Program
    {
        static void  Watch()
        {
            Player player1 = new Player(1, 4);
            Player player2 = new Player(1, 3);
            int score1 = 12, score2 = 12, round = 0;
            int card1, card2;
            while (score1 > 0 && score2 > 0)
            {
                Console.WriteLine("Round {0}\t\t{1} VS {2}", ++round, score1, score2);
                card1 = player1.PlayCard();
                card2 = player2.PlayCard();
                Console.WriteLine("Player1's card: {0}\tPlayer2's card: {1}", card1, card2);
                score1 = player1.calculateNewScore(card1, card2);
                score2 = player2.calculateNewScore(card2, card1);
                Console.WriteLine("Player1's score: {0}\tPlayer2's score: {1}", score1, score2);
            }

        }

        static void PlayAgainstComputer()
        {
            Random rnd = new Random();
            int extra1 = rnd.Next(1, 5), extra2 = rnd.Next(1, 5);
            Player player1 = new Player(rnd.Next(1,5),rnd.Next(1,5) );
            Player player2 = new Player(extra1,extra2);
            Console.WriteLine("Your extra cards are {0}\t{1}", extra1,extra2);

            int score1 = 12, score2 = 12, round = 0;
            int card1, card2;
            while (score1 > 0 && score2 > 0)
            {
                Console.WriteLine("\nRound {0}\t\t{1} VS {2}", ++round, score1, score2);
                card1 = player1.PlayCard();
                card2 = player2.PlayCardByChoice();
                Console.WriteLine("Player1's card: {0}\t\t\tPlayer2's card: {1}", card1, card2);
                score1 = player1.calculateNewScore(card1, card2);
                score2 = player2.calculateNewScore(card2, card1);
                //Console.WriteLine("Player1's score: {0}\tPlayer2's score: {1}", score1, score2);
            }
            string message = score1 > score2 ? "You Lost!!" : "You Win";
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {

            bool done = false;
            while (!done)
            {
                Console.WriteLine("\n\n0 for watch, 1 for play aginest computer, -1 to quit");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        Watch();
                        break;
                    case 1:
                        PlayAgainstComputer();
                        break;
                    case -1:
                        done = true;
                        break;
                    default:
                        Console.WriteLine("0 for watch, 1 for play aginest computer, -1 to quit");
                        break;
                }
            }

        }
    }
}
