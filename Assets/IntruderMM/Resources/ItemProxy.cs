using UnityEngine;

public class ItemProxy : ScriptableObject
{
    public string idName;
    public Mesh pickupMesh;
    [Header("Custom Pickup")]
    public PickupProxy customPickupPrefab;
    public string customItemName;
    public string customItemDescription;
    public Sprite inventoryIcon;
    public bool showOnCharacter = false;
    public AttachmentBoneType attachmentBoneType = AttachmentBoneType.None;
}