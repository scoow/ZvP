using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class ChengeParts : MonoBehaviour
{
   // [SerializeField]  SpriteLibrary spriteLibrary;
    [SerializeField]  string category;
    [SerializeField]  SpriteResolver spriteResolver;
    [SerializeField] string label;
    //private List<string> labelList;
    void Start()
    {
      //  var libraryAsset = spriteLibrary.spriteLibraryAsset;
      //  var labels = libraryAsset.GetCategoryLabelNames(category);
       // labelList = new List<string>();
      //  foreach (var label in labels)
      //  {
       //     labelList.Add(label);
      //  }
      //  foreach (var label in labelList)
       // {
      //      Console.WriteLine(label);
       // }
    }
    
    void Update()
    {
        
    }
    public void Chenge()
    {  
        spriteResolver.SetCategoryAndLabel(category,label);
       
        
    }
}
