using UnityEngine;
public class Nico : ItemTaker
{
    
    public override string ItemName { get { return "HardDrive"; } }

    public override void Interact()
    {
        // Get the cutscene from the hard drive that the player is holding
        Cutscene cutscene = GetItem().GetComponent<HardDrive>().cutscene;
        if (cutscene == null) {
            Debug.LogError("Hard drive does not have a cutscene set");
            return;
        }

        RailControlPoint stop = GetItem().GetComponent<HardDrive>().stop;
        if (stop == null) {
            Debug.LogError("Hard drive does not have a stop set");
            return;
        }

        DoorTriggerable door = GetItem().GetComponent<HardDrive>().door;
        if (door == null) {
            Debug.LogError("Hard drive does not have a door set");
            return;
        }

        if (!TakeItem()) {
            return;
        }

        // TODO: Pause
        cutscene.Play();
        // TODO: unpause

        door.Interact();
        stop.Continue();
    }

}