using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Member.Kmin._01_Script.SkillTree
{
    public class SkillTreeNode : MonoBehaviour, INode
    {
        [Header("<color=blue>Setting</color>")]
        [SerializeField] private NodeSO nodeSO;
        [SerializeField] private Image nodeImage;
        [SerializeField] private float width = 10;
        [SerializeField] private Sprite branchImage;
    
        [field: SerializeField] public List<SkillTreeNode> ConnectedNodes;
        [field: SerializeField] public bool IsRootNode { get; private set; }
        [field: SerializeField, HideInInspector] public List<Image> ConnectedBranch { get; private set; }
        [field: SerializeField, HideInInspector] public List<Image> FillBranch { get; private set; }
        public SkillTreeNode ParentNode { get; private set; }
        public Button NodeButton { get; private set; }
        public Image NodeIcon { get; private set; }
        public Image NodeOutline { get; private set; }
    
        public Color branchColor = Color.magenta;

        public void Initialize()
        {
            NodeButton = GetComponentInChildren<Button>();
            NodeIcon = NodeButton.transform.Find("Icon").GetComponent<Image>();
            NodeOutline = NodeButton.transform.Find("Outline").GetComponent<Image>();
            
            nodeSO.isPurchase = false;

            ConnectedNodes.ForEach(f => { f.ParentNode = this; });
        
            if (IsRootNode) {
                NodeButton.interactable = true;
                nodeSO.isActive = true;
                nodeSO.isPurchase = true;
            
                ConnectedNodes.ForEach(f => f.NodeButton.interactable = true);
            }
        }

        public NodeSO GetNodeSO() => nodeSO;

        #region ConnectNode
#if UNITY_EDITOR

        [ContextMenu("ConnectLine")]
        private void ConnectLine()
        {
            foreach (SkillTreeNode f in ConnectedNodes)
            {
                if (f.ConnectedBranch.Count > 0)
                {
                    f.ConnectedBranch.ForEach(n => { if (n != null) DestroyImmediate(n.gameObject); });
                    f.FillBranch.ForEach(n => { if (n != null) DestroyImmediate(n.gameObject); });
                
                    f.ConnectedBranch.Clear();
                    f.FillBranch.Clear();
                }

                Transform root = f.transform.Find("Branches");
                GameObject[] obj = new GameObject[3];
                Image[] nodes = new Image[3];

                for (int i = 0; i < 3; i++)
                {
                    obj[i] = new GameObject($"@Branches{i}");
                
                    nodes[i] = obj[i].AddComponent<Image>();
                    nodes[i].transform.SetParent(root, false);
                    nodes[i].raycastTarget = false;
                    nodes[i].transform.SetSiblingIndex(0);
                
                    f.ConnectedBranch.Add(nodes[i]);
                }

                var rect = transform as RectTransform;
                Vector2 node1Pos = Vector2.zero;
                Vector2 selfPos = rect.position;
                Vector2 fruitsPos = f.GetComponentInChildren<Image>().rectTransform.position;

                int origin = 0;

                if (node1Pos == Vector2.zero)
                {
                    node1Pos = new Vector2(selfPos.x, (fruitsPos.y + selfPos.y) / 2);
                    origin = selfPos.y < node1Pos.y ? 0 : 1;
                    ConnectBranch(selfPos, node1Pos, nodes[0], true);
                    ConnectFillBranch(f.ConnectedBranch[0], root, f, origin);
                }

                Vector3 node2Pos = new Vector2(fruitsPos.x, node1Pos.y);
                origin = node1Pos.x < node2Pos.x ? 0 : 1;
                ConnectBranch(node1Pos, node2Pos, nodes[1], false);
                ConnectFillBranch(f.ConnectedBranch[1], root, f, origin);

                origin = node2Pos.y < fruitsPos.y ? 0 : 1;
                ConnectBranch(node2Pos, fruitsPos, nodes[2], true);
                ConnectFillBranch(f.ConnectedBranch[2], root, f, origin);
            }
        }

        private void ConnectFillBranch(Image target, Transform root, SkillTreeNode parent, int origin)
        {
            Image fillImg = new GameObject($"@FillBranches{parent.FillBranch.Count}").AddComponent<Image>();
        
            fillImg.transform.SetParent(root, false);
            fillImg.rectTransform.anchoredPosition = target.rectTransform.anchoredPosition;
            fillImg.rectTransform.sizeDelta = target.rectTransform.sizeDelta;
            fillImg.sprite = branchImage;
            fillImg.type = Image.Type.Filled;
            fillImg.fillAmount = 0;
            fillImg.transform.SetSiblingIndex(root.childCount);
            fillImg.raycastTarget = false;

            fillImg.fillMethod = fillImg.rectTransform.sizeDelta.x > fillImg.rectTransform.sizeDelta.y ?
                Image.FillMethod.Horizontal : Image.FillMethod.Vertical;
            fillImg.fillOrigin = origin;
        
            parent.FillBranch.Add(fillImg);
        }

        [ContextMenu("ClearAllBranch")]
        private void ClearAllBranch()
        {
            foreach (var fruits in ConnectedNodes)
            {
                fruits.ConnectedBranch.ForEach(n => DestroyImmediate(n.gameObject));
                fruits.FillBranch.ForEach(n => DestroyImmediate(n.gameObject));
                fruits.ConnectedBranch.Clear();
                fruits.FillBranch.Clear();
            }
        }

        [ContextMenu("ClearBranch")]
        private void ClearBranch()
        {
            ConnectedBranch.ForEach(n => DestroyImmediate(n.gameObject));
            FillBranch.ForEach(n => DestroyImmediate(n.gameObject));
            ConnectedBranch.Clear();
            FillBranch.Clear();
        }

        private void ConnectBranch(Vector3 pos1, Vector3 pos2, Image node, bool isVert)
        {
            var rect = node.rectTransform;
            rect.position = (pos1 + pos2) / 2f;
            float distance = Vector3.Distance(pos1, pos2);
        
            rect.sizeDelta = isVert 
                ? new Vector2(width, distance + width) 
                : new Vector2(distance + width, width);
        }

#endif
        #endregion
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (nodeImage == null)
                nodeImage = GetComponent<Image>();
            
            nodeImage.sprite = nodeSO.icon == null ? nodeSO.statSO.Icon : nodeSO.icon;

            if (FillBranch == null)
                return;

            foreach (var node in FillBranch)
                node.color = branchColor;
        }
        #endif
    }
}