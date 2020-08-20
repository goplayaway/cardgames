using System;
using System.Collections.Generic;
using System.Linq;

namespace cardgamesAPI.Models
{
    [Serializable]
    public class cardsPlayers : ICloneable
    {
        public int playerID { get; set; }
        public List<cardset> cardsPlayer { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    [Serializable]
    public class cardset
    {
        public int cardid { get; set; }
        public string cardnumber { get; set; }
        public string cardtype { get; set; }

        public int cardtypeOrder { get; set; }
        public int cardnumberorder { get; set; } 
    }
    [Serializable]
    public class card : cardset
    {


        public List<string> player { get; set; }

        private List<string> baralho()
        {
            List<string> b = new List<string>();

            string[] cardset = new string[13] { "a", "2", "3", "4", "5", "6", "7", "8", "9", "10", "j", "q", "k" };
            string[] cardtype = new string[4] { "spades", "hearts", "diams", "clubs" };


            foreach (string t in cardtype)
            {
                foreach (string c in cardset)
                {
                    b.Add(t + "," + c);
                }
            }

            return b.OrderBy(x => System.Guid.NewGuid()).ToList();
        }
        private cardsPlayers splitcards(cardsPlayers cx, int a, int b, int playerID)
        {
            var c1 = cx.cardsPlayer.Where(x => x.cardid >= a && x.cardid < b).OrderBy(x => x.cardtypeOrder).ThenBy(x => x.cardnumberorder).ToList();

            cardsPlayers cp1 = new cardsPlayers();
            cp1.playerID = playerID;
            cp1.cardsPlayer = c1;

            return cp1;
        }

        private int cardorder(string cardtype)
        {
            int o = 0;
            switch (cardtype)
            {
                case "hearts":
                    o = 1;
                    break;
                case "spades":
                    o = 2;
                    break;
                case "diams":
                    o = 3;
                    break;
                case "clubs":
                    o = 4;
                    break;
            }

            return o;
        }
        public List<cardsPlayers> shufflePlayers()
        {
            List<string> b = baralho();

            cardset c = new cardset();
            cardsPlayers cx = new cardsPlayers();

            cx.cardsPlayer = new List<cardset>();

            for (int i = 0; i < (13 * 4); i++)
            {
                c.cardid = i + 1;
                c.cardnumber = b[i].Split(',')[1];
                c.cardtype = b[i].Split(',')[0];
                c.cardtypeOrder = cardorder(c.cardtype); 
                c.cardnumberorder = Convert.ToInt32(c.cardnumber.Replace("a", "14").Replace("j", "11").Replace("q", "12").Replace("k", "13"));
                cx.cardsPlayer.Add(c);
                c = new cardset();
            }

            List<cardsPlayers> p = new List<cardsPlayers>();
            p.Add(splitcards(cx, 1, 14, 1));
            p.Add(splitcards(cx, 14, 27, 2));
            p.Add(splitcards(cx, 27, 40, 3));
            p.Add(splitcards(cx, 40, 53, 4));


            return p;

        }

        


    }
}