
namespace MCronberg.Kursus
{
    public class Terning
    {
        public int Værdi { get; private set; }

        public Terning()
        {
            Ryst();
        }

        public Terning(int værdi)
        {
            if (værdi < 1 || værdi > 6)
                throw new ApplicationException("Forkert værdi");
            this.Værdi = værdi;            
        }

        public void Ryst()
        {            
            this.Værdi = Random.Shared.Next(1, 7);            
        }

        public override string ToString()
        {
            return $"[{this.Værdi}]";
        }

    }
}
