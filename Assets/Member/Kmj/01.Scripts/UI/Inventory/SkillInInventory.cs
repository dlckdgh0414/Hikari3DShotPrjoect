using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillInInventory : MonoBehaviour
{
    [SerializeField] private SkillSO skillso;

    [SerializeField] private UseSkillDataSO useSkillList;

    private Button Thisbutton;
    private void Awake()
    {
        Thisbutton = GetComponent<Button>();
    }

    private void Update()
    {
    }
}
