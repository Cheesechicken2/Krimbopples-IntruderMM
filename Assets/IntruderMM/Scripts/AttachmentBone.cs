using UnityEngine;

public enum AttachmentBoneType
{
    None,
    Head,
    BeltRight,
    BeltBack,
    BeltLeft,
    BeltFront,
    Back,
    Chest
}

public class AttachmentBone : MonoBehaviour
{
    AttachmentBoneType attachmentBoneType = AttachmentBoneType.Head;
}
