using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Fruits : MonoBehaviour
{
    [SerializeField] private FruitsSO fruitsSO;
    [SerializeField] private List<Fruits> _connectedFruits;

    public event Action OnFruitsPurchase;

    public bool IsConnected { get; private set; }

    [SerializeField] private Button _fruitsBtn;

    private void Awake()
    {
        _fruitsBtn = GetComponentInChildren<Button>();
        _fruitsBtn.onClick.AddListener(PurchaseFruits);

        OnFruitsPurchase += HandleFruitsPurchase;
    }

    private void PurchaseFruits()
    {
        if(fruitsSO.price < CurrencyManager.Instance.GetCurrency(CurrencyType.Eon))
        {
            CurrencyManager.Instance.ModifyCurrency
                (CurrencyType.Eon, ModifyType.Substract, fruitsSO.price);

            OnFruitsPurchase?.Invoke();
        }
    }

    private void HandleFruitsPurchase()
    {
        _connectedFruits.Where(f => f.IsConnected == false).ToList().ForEach(f => f.ConnectLine());
    }

    #region ConnectLineOnEditor
    [ContextMenu("ConnectLine")]
    private void ConnectLine()
    {

        foreach(Fruits f in _connectedFruits)
        {
            GameObject obj1 = new GameObject($"{f}Nod1");
            GameObject obj2 = new GameObject($"{f}Nod2");
            GameObject obj3 = new GameObject($"{f}Nod3");
            Image node1 = obj1.AddComponent<Image>();
            Image node2 = obj2.AddComponent<Image>();
            Image node3 = obj3.AddComponent<Image>();
            node1.transform.SetParent(transform, false);
            node2.transform.SetParent(transform, false);
            node3.transform.SetParent(transform, false);

            Image targetFruits = f.GetComponentInChildren<Image>();

            Vector2 node1Pos = Vector2.zero;
            Vector2 selfPos = _fruitsBtn.GetComponent<Image>().rectTransform.position;
            Vector2 fruitsPos = targetFruits.rectTransform.position;

            for (int i = 0; i < 2; i++)
            {
                if (node1Pos == Vector2.zero)
                {
                    node1Pos = new Vector2(selfPos.x, (fruitsPos.y + selfPos.y) / 2);
                    ConnectNode(selfPos, node1Pos, node1, true);
                }

                Vector3 node2Pos = new Vector2(fruitsPos.x, node1Pos.y);

                ConnectNode(node1Pos, node2Pos, node2, false);
                ConnectNode(node2Pos, fruitsPos, node3, true);
            }
        }
    }

    private void ConnectNode(Vector3 pos1, Vector3 pos2, Image node, bool isVert)
    {
        Vector3 centerPos = (pos1 + pos2) / 2f;
        float distance = Vector3.Distance(pos1, pos2);

        node.rectTransform.position = centerPos;

        if (isVert)
            node.rectTransform.sizeDelta = new Vector2(10, distance);
        else
            node.rectTransform.sizeDelta = new Vector2(distance, 10);
    }
    #endregion
}
