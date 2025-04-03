using UnityEngine;

public class TestTwo : MonoBehaviour
{
    public ItemSO itemSO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemGameObject testItem = collision.gameObject?.GetComponent<ItemGameObject>();
        testItem.currentItem = itemSO;
    }
}
