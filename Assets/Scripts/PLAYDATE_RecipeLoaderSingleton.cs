 using UnityEngine;
 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Text.RegularExpressions;

public struct recipeItem {
    public string makeName;
    public int makeCount;
    public recipeItem(string n, int c) {
        this.makeName = n;
        this.makeCount = c;
    }
}

 
 public class PLAYDATE_RecipeLoaderSingleton : MonoBehaviour 
 {

     public TextAsset recipeCsvfile;
     private string recipeCsv;

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
 
     private static PLAYDATE_RecipeLoaderSingleton instance = null;
     private static  int internalcount = 0;

     public static Dictionary<string, List<recipeItem>> recipeDict;

     
     // Game Instance Singleton
     public static PLAYDATE_RecipeLoaderSingleton Instance
     {
         get
         { 
             return instance; 
         }
     }
     
     private void Awake()
     {
         // if the singleton hasn't been initialized yet
         if (instance != null && instance != this) 
         {
             Destroy(this.gameObject);
         }
 
         instance = this;
         DontDestroyOnLoad( this.gameObject );

         if(LoadRecipeCSV(recipeCsvfile) > 0) {
            print("recipe loaded!");
         }
     }
 
     void Update()
     {
         //Debug.Log( "SINGLETON UPDATE"  + internalcount);
         //internalcount += 1;
     }

    // modified from https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
     int LoadRecipeCSV(TextAsset data) {
//    public static int RecipeCSVRead(TextAsset data) {
        recipeDict = new Dictionary<string, List<recipeItem>>();

 
        var lines = Regex.Split (data.text, LINE_SPLIT_RE);
 
        if(lines.Length <= 1) return -1;
 
        var header = Regex.Split(lines[0], SPLIT_RE);
        print( "=====CSVLOAD STARt=======");
        for(var i=1; i < lines.Length; i++) {
 
            var values = Regex.Split(lines[i], SPLIT_RE);
            if(values.Length == 0 ||values[0] == "") continue;
 


            //stuff
            List<recipeItem> ri = new List<recipeItem>();
            string fromName = "";


            for(var j=0; j < header.Length && j < values.Length; j++ ) {

                // parse and clean values
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if(int.TryParse(value, out n)) {
                    finalvalue = n;
                } else if (float.TryParse(value, out f)) {
                    finalvalue = f;
                }


                //only add to dict if quantity > 0
                
                if(j == 0) {
                    fromName = (string)values[j];
                } else {
                    if((int)finalvalue > 0) {
                        print(fromName + "->" + header[j] + ":" +  finalvalue);
                        ri.Add(new recipeItem(header[j],  (int)finalvalue));
                    }
                }
                

            }
            print("====");
            recipeDict.Add(fromName, ri);

        }
        print("=====CSVLOAD END=======");
        return 1;
        
    }


 }

