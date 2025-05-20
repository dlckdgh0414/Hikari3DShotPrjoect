using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.Kmin._01_Script.Core.Save
{
    [Serializable]
    public class UseSkillSaveData : MonoBehaviour
    {
        public List<string> invenSkillID = new();
        public List<string> useSkillID = new();
    }
}