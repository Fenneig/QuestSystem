using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem.Data
{
    public abstract class Repository<TDef> : ScriptableObject where TDef : Definition
    {
        [SerializeField] protected List<TDef> _defs;

        public List<TDef> Defs => _defs;

        public TDef GetDefinitionById(string definitionId) => 
            _defs.FirstOrDefault(def => def.ID.Equals(definitionId, StringComparison.InvariantCultureIgnoreCase));
    }
}