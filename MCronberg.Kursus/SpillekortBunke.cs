
namespace MCronberg.Kursus
{
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
