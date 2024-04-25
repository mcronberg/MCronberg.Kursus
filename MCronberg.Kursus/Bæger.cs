
namespace MCronberg.Kursus
{
    public class Bæger
    {

        public Terning[] Terninger { get; private set; }

        public Bæger(int antalTerninger)
        {
            this.Terninger = new Terning[antalTerninger];
            for (int i = 0; i < antalTerninger; i++)
            {
                this.Terninger[i] = new Terning();
            }
        }

        public void Ryst()
        {
            foreach (var item in this.Terninger)
            {
                item.Ryst();
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in this.Terninger)
            {
                s += item.ToString() + " ";
            }
            return s;
        }

        public void Sorter()
        {
            this.Terninger = [.. this.Terninger.OrderBy(i => i.Værdi)];
        }
    }

    public enum SpillekortKulør { Spar, Hjerter, Ruder, Klør }
    public enum SpillekortVærdi { To = 2, Tre, Fire, Fem, Seks, Syv, Otte, Ni, Ti, Knægt, Dame, Konge, Es }

    public enum SpillekortFarve { Rød, Sort }
    public enum SpillekortPlacering { Op, Ned }

    public class Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi, SpillekortPlacering placering, int point)
    {

        public SpillekortKulør Kulør { get; private set; } = kulør;
        public SpillekortVærdi Værdi { get; private set; } = værdi;
        public SpillekortPlacering Placering { get; private set; } = placering;
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

        public Spillekort(int point, bool erJoker) : this(SpillekortKulør.Spar, SpillekortVærdi.Es, SpillekortPlacering.Op, point)
        {
            this.ErJoker = erJoker;
        }

        public Spillekort() : this(SpillekortKulør.Spar, SpillekortVærdi.Es, SpillekortPlacering.Op, 0)
        {
        }

        public Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi) : this(kulør, værdi, SpillekortPlacering.Op, 0)
        {
        }

        public Spillekort(SpillekortKulør kulør, SpillekortVærdi værdi, int point) : this(kulør, værdi, SpillekortPlacering.Op, point)
        {
        }

        public void Vend()
        {
            this.Placering = this.Placering == SpillekortPlacering.Op ? SpillekortPlacering.Ned : SpillekortPlacering.Op;
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
            if (this.Placering == SpillekortPlacering.Ned)
            {
                return "[****]";
            }
            return $"[{s} {v}]";
        }
    }

    public class SpillekortBunke
    {

        public List<Spillekort> Kort { get; private set; } = [];

        public static SpillekortBunke OpretBunke(int antalKortspil = 1, int antalJokere = 0)
        {
            if (antalKortspil < 1) throw new ArgumentException("antalKortspil skal være mindst 1");
            if (antalJokere < 0) throw new ArgumentException("antalJokere skal være mindst 0");

            SpillekortBunke bunke = new();
            for (int i = 0; i < antalKortspil; i++)
            {
                foreach (SpillekortKulør kulør in Enum.GetValues(typeof(SpillekortKulør)))
                {
                    foreach (SpillekortVærdi værdi in Enum.GetValues(typeof(SpillekortVærdi)))
                    {
                        bunke.TilføjKort(new Spillekort(kulør, værdi));
                    }
                }
            }
            for (int i = 0; i < antalJokere; i++)
            {
                bunke.TilføjKort(new Spillekort(0, true));
            }
            return bunke;
        }


        public void FlytKortTilBunke(SpillekortBunke bunke, int antalKort)
        {
            if (antalKort > this.Kort.Count) throw new ArgumentException("Der er ikke så mange kort i bunken");
            for (int i = 0; i < antalKort; i++)
            {
                bunke.TilføjKort(this.Kort[i]);
            }
            this.Kort.RemoveRange(0, antalKort);
        }

        public void TilføjKort(Spillekort kort)
        {
            this.Kort.Add(kort);
        }

        public void FjernKort(Spillekort kort)
        {
            this.Kort.Remove(kort);
        }

        public void Bland()
        {
            this.Kort = [.. this.Kort.OrderBy(a => Guid.NewGuid())];
        }

        public void Sorter()
        {
            this.Kort = [.. this.Kort.OrderBy(a => a.Kulør).ThenBy(a => a.Værdi)];
        }

        public void VendAlleKort()
        {
            foreach (var item in this.Kort)
            {
                item.Vend();
            }
        }

        public void FjernAlleKort()
        {
            this.Kort.Clear();
        }

        public void FjernAlleKort(SpillekortKulør kulør)
        {
            this.Kort.RemoveAll(a => a.Kulør == kulør);
        }

        public void FjernAlleKort(SpillekortVærdi værdi)
        {
            this.Kort.RemoveAll(a => a.Værdi == værdi);
        }

        public void FjernAlleKort(SpillekortKulør kulør, SpillekortVærdi værdi)
        {
            this.Kort.RemoveAll(a => a.Kulør == kulør && a.Værdi == værdi);
        }

        public void FjernAlleKort(SpillekortFarve farve)
        {
            this.Kort.RemoveAll(a => a.Farve == farve);
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in this.Kort)
            {
                s += item.ToString() + " ";
            }
            return s;
        }
    }

}
