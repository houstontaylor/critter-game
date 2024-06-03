using UnityEngine;
public class Nico : ItemTaker
{
    
    public override string ItemName { get { return "HardDrive"; } }

    public override void Interact()
    {
        Cutscene cutscene = GetItem().GetComponent<HardDrive>().cutscene;
        RailControlPoint stop = GetItem().GetComponent<HardDrive>().stop;
        DoorTriggerable door = GetItem().GetComponent<HardDrive>().door;

        if (!TakeItem()) {
            return;
        }

        if (cutscene != null)
            cutscene.Play();
        if (door != null)
            door.Interact();
        if (stop != null)
            stop.Continue();
    }

}