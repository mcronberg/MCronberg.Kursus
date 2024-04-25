
namespace MCronberg.Kursus
{
    public class TerningBæger
    {

        public Terning[] Terninger { get; private set; }

        public TerningBæger(int antalTerninger)
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

}
