using UnityEngine;

[CreateAssetMenu(menuName = "Item Data/Potion")]
public class PotionData : SceneData<Item, int>
{
    protected override int SetDataProperty(int value, int index)
    {
        return Mathf.Max(0, value);
    }

    public override bool TrySetValue(Item instance, int buildIndex)
    {
        bool result = base.TrySetValue(instance, buildIndex);

        return result;
    }
}
