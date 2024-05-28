using UnityEngine;


public class RailStartInteractable : ItemTaker
{
    public Rail rail;
    public override string ItemName { get { return "Nico"; } }

    public override void Interact()
    {
        if (!TakeItem(false)) {
            return;
        }

        // Move Nico onto the rail
        GameObject holding = _playerController.holding;
        rail.Mount(holding);
        Nico nico = holding.AddComponent<Nico>();
        nico.PopupObject = holding.GetComponent<Pickupable>().PopupObject;
        Destroy(holding.GetComponent<Pickupable>());
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "Nico";
    }
}
