
namespace MCronberg.Kursus
{
    public class Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi, SpillekortRetning retning, int point)
    {

        public SpillekortKulør Kulør { get; private set; } = kulør;
        public SpillekortVærdi Værdi { get; private set; } = værdi;
        public SpillekortRetning Retning { get; private set; } = retning;
        public int Point { get; private set; } = point;
        public bool ErJoker { get; private set; }


        public static SpillekortKulør FindTilfældigSpillekortKulør()
        {
            Random rnd = Random.Shared;
            return (SpillekortKulør)rnd.Next(0, 4);
        }

        public static SpillekortVærdi FindTilfældigSpillekortVærdi()
        {
            Random rnd = Random.Shared;
            return (SpillekortVærdi)rnd.Next(0, 13);
        }

        public static Spillekort FindTilfældigtKort()
        {
            return new Spillekort(FindTilfældigSpillekortKulør(), FindTilfældigSpillekortVærdi());
        }

        public Spillekort(int point, bool erJoker) : this(SpillekortKulør.Spar, SpillekortVærdi.Es, SpillekortRetning.Op, point)
        {
            this.ErJoker = erJoker;
        }

        public Spillekort() : this(SpillekortKulør.Spar, SpillekortVærdi.Es, SpillekortRetning.Op, 0)
        {
        }

        public Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi) : this(kulør, værdi, SpillekortRetning.Op, 0)
        {
        }

        public Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi, int point) : this(kulør, værdi, SpillekortRetning.Op, point)
        {
        }

        public void Vend()
        {
            this.Retning = this.Retning == SpillekortRetning.Op ? SpillekortRetning.Ned : SpillekortRetning.Op;
        }

        public SpillekortFarve Farve
        {
            get
            {
                if (this.Kulør == SpillekortKulør.Hjerter || this.Kulør == SpillekortKulør.Ruder)
                {
                    return SpillekortFarve.Rød;
                }
                else
                {
                    return SpillekortFarve.Sort;
                }
            }
        }

        public override string ToString()
        {
            string s = this.Kulør switch
            {
                SpillekortKulør.Hjerter => "\u2665",
                SpillekortKulør.Ruder => "\u2666",
                SpillekortKulør.Klør => "\u2663",
                SpillekortKulør.Spar => "\u2660",
                _ => ""
            };
            var v = "";
            switch (this.Værdi)
            {
                case SpillekortVærdi.To:
                case SpillekortVærdi.Tre:
                case SpillekortVærdi.Fire:
                case SpillekortVærdi.Fem:
                case SpillekortVærdi.Seks:
                case SpillekortVærdi.Syv:
                case SpillekortVærdi.Otte:
                case SpillekortVærdi.Ni:
                case SpillekortVærdi.Ti:
                    if((int)Værdi < 10)
                        v = $"{((int)Værdi)+ " "}";
                    else
                        v = $"{(int)Værdi}";
                    break;
                case SpillekortVærdi.Knægt:
                    v = "KN";
                    break;
                case SpillekortVærdi.Dame:
                    v = "DA";
                    break;
                case SpillekortVærdi.Konge:
                    v = "KO";
                    break;
                case SpillekortVærdi.Es:
                    v = "ES";
                    break;
            }
            if (this.ErJoker)
            {
                return "[ JO ]";
            }
            if (this.Retning == SpillekortRetning.Ned)
            {
                return "[****]";
            }
            return $"[{s} {v}]";
        }
    }

}
