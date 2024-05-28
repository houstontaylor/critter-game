public class Nico : ItemTaker
{
    public override string ItemName { get { return "HardDrive"; } }
    public override void Interact()
    {
        if (!TakeItem()) {
            return;
        }

        // TODO: Trigger cutscene and advance rail
        // need a reference to the stop we're moving past
    }
}