public class Scrap : AbstractSObject
{
    int amount;

    public Scrap(int amount) 
    {
        Name = "Scrap";
        Description = "Some space scrap floating around";

        this.amount = amount;
    }

}
