using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Fruits : MonoBehaviour, IFruits
{
    [SerializeField] private FruitsSO fruitsSO;
    [SerializeField] private List<Fruits> _connectedFruits;

    [field:SerializeField] public List<Image> ConnectedNode { get; private set; }

    public event Action OnFruitsPurchase;

    public bool IsActive { get; private set; }

    public Button FruitsButton { get; private set; }

    public Fruits ParentFruits { get; private set; } = null;

    public void Initialize(Fruits parent)
    {
        ParentFruits = parent;
        FruitsButton = GetComponentInChildren<Button>();
        FruitsButton.onClick.AddListener(PurchaseFruits);

        OnFruitsPurchase += HandleFruitsPurchase;

        Transform nodeTrm = transform.Find("Nodes");

        for (int i = 0; i < 3; i++)
        {
            ConnectedNode.Add(nodeTrm.GetChild(i).GetComponent<Image>());
        }
    }

    private void PurchaseFruits()
    {
        if(fruitsSO.price < CurrencyManager.Instance.GetCurrency(CurrencyType.Eon) && !IsActive)
        {
            CurrencyManager.Instance.ModifyCurrency
                (CurrencyType.Eon, ModifyType.Substract, fruitsSO.price);

            OnFruitsPurchase?.Invoke();
        }
    }

    [ContextMenu("TestPurchase")]
    private void HandleFruitsPurchase()
    {
        IsActive = true;
        ConnectedNode.ForEach(line => Debug.Log(line));

        ConnectedNode.ForEach(line => line.color = Color.red);
        Debug.Log("±¸¸Å");
    }

    #region ConnectLineOnEditor
    [ContextMenu("ConnectLine")]
    private void ConnectLine()
    {
        foreach (Fruits f in _connectedFruits)
        {
            f.ConnectedNode.Clear();
            Transform root = f.transform.Find("Nodes");
            GameObject[] obj = new GameObject[3];
            Image[] nodes = new Image[3];

            for (int i = 0; i < 3; i++) {
                obj[i] = new GameObject($"Node{i}");
                nodes[i] = obj[i].AddComponent<Image>();
                nodes[i].transform.SetParent(root, false);

                f.ConnectedNode.Add(nodes[i]);
            }

            var rect = transform as RectTransform;
            Vector2 node1Pos = Vector2.zero;
            Vector2 selfPos = rect.position;
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
