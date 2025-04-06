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
            GameObject[] obj = new GameObject[3];
            Image[] nodes = new Image[3];

            for (int i = 0; i < 3; i++) {
                obj[i] = new GameObject($"{f}Node{i + 1}");
                nodes[i] = obj[i].AddComponent<Image>();
                nodes[i].transform.SetParent(transform, false);
            }

            Vector2 node1Pos = Vector2.zero;
            Vector2 selfPos = _fruitsBtn.GetComponent<Image>().rectTransform.position;
            Vector2 fruitsPos = f.GetComponentInChildren<Image>().rectTransform.position;

            for (int i = 0; i < 2; i++)
            {
                if (node1Pos == Vector2.zero) {
                    node1Pos = new Vector2(selfPos.x, (fruitsPos.y + selfPos.y) / 2);
                    ConnectNode(selfPos, node1Pos, nodes[0], true);
                }

                Vector3 node2Pos = new Vector2(fruitsPos.x, node1Pos.y);
                ConnectNode(node1Pos, node2Pos, nodes[1], false);
                ConnectNode(node2Pos, fruitsPos, nodes[2], true);
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
