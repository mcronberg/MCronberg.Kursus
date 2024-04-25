using MCronberg.Kursus;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Terning terning = new();
Console.WriteLine(terning.ToString());
Bæger bæger = new(5);
Console.WriteLine(bæger.ToString());
bæger.Ryst();
Console.WriteLine(bæger.ToString());

Spillekort spillekort = new(SpillekortKulør.Hjerter, SpillekortVærdi.Knægt, SpillekortPlacering.Op, 10);
Console.WriteLine(spillekort.ToString());

SpillekortBunke b1 = SpillekortBunke.OpretBunke(2, 2);
b1.Bland();
Console.WriteLine(b1.ToString());

SpillekortBunke b2 =  new();
b1.FlytKortTilBunke(b2, 5);
Console.WriteLine(b2.ToString());
