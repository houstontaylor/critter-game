using UnityEngine;

public class BrokenRailInteractible : ItemTaker
{
    public RailControlPoint rail;
    public override string ItemName { get { return "RailSegment"; } }
    

    // If the player is holding a rail segment, consume it and advance the rail
    public override void Interact() {
        if (!TakeItem()) {
            return;
        }

        // Let the rail know it's repaired
        rail.Continue();
    }
}
