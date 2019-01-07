using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SuperTiled2Unity;
using SuperTiled2Unity.Editor;
using UnityEngine;

public class TmxDungeonImporter : CustomTmxImporter {
    public override void TmxAssetImported (TmxAssetImportedArgs args) {
        SuperObject[] superObjects = args.ImportedSuperMap.GetComponentsInChildren<SuperObject>();
        foreach (var item in superObjects)
        {
            SuperCustomProperties props = item.GetComponent<SuperCustomProperties>();
            foreach (var prop in props.m_Properties)
            {
                if(prop.m_Name == "AddComponent"){
                    Type componentType = Type.GetType(prop.m_Value + ",Assembly-CSharp");
                    if(componentType != null){
                        item.gameObject.AddComponent(componentType);
                    }
                }
            }
        }

    }
}