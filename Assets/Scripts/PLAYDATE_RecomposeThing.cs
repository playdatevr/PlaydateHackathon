
namespace VRTK.Examples
{

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct recomposeChoice {
    //obj to use, obj to destroy (needed alternatives), obj leftover = recomposition(gobjs);
    public List<recipeItem> objsCreated;
    public List<recipeItem> objsUsed;
    public List<recipeItem> objsNeededDestroy;
    public List<recipeItem> objsLeftover;

    public recomposeChoice(List<recipeItem> oc, List<recipeItem> ou, List<recipeItem> ond, List<recipeItem> ol) {
        this.objsCreated = oc;
        this.objsUsed = ou;
        this.objsNeededDestroy = ond;
        this.objsLeftover = ol;
    }
}

public class PLAYDATE_RecomposeThing : VRTK_InteractableObject {
   
    public float coeffUsed = 1.0f;
    public float coeffNeeded = -1.0f;
    public float coeffLeft = 0.2f;
 
    private GameObject myself = null;
    private string prefabDir = "Things/";
    private string parent = "AllThings";
    private float instantiateRange = 1.1f;

    private string thingToMake;
    private GameObject parentObject;

    void Awake() {
        parentObject = GameObject.Find(parent);
    }

    void Start()
    {
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        print("DECOMPOSE");
        Recompose();
    }

    void Update()
    {
    }

    void Recompose() {

        // check for CSV load
        if(PLAYDATE_RecipeLoaderSingleton.recipeDict == null) { return;  }


        print("//1. Get objects to recompose and tag them");
        //1. Get objects to recompose
        List<recipeItem> objsToRecompose = GetObjectsToRecomposeAndTag();

        print("//2. Choose recipe");
        List<recipeItem> chosenRecipe = ChooseThingsToRecomposeFromRecipe(objsToRecompose);
        //obj to use, obj to destroy (needed alternatives), obj leftover = recomposition(gobjs);

        print("//2.5.. Calculate recomposition object with algorithm");
        recomposeChoice thisChoice = calculateChoice(chosenRecipe);

        print("//3. Destroy objects, create new object.");
        if(DestroyAndCreate(thisChoice) > 0) {
            print("DestroyAndCreate successful!");
        }


    }

    List<recipeItem> GetObjectsToRecomposeAndTag() {

        // TODO: insert Yujie's grabber script

        // TODO: TAG STUFF SHIDFSDK FJSALFJK AHERE

        //test
        List<recipeItem> testObjects = new List<recipeItem>();
        testObjects.Add(new recipeItem("treeB", 5));
        testObjects.Add(new recipeItem("log", 2));
        return testObjects;

    }

    int countIntersect(List<recipeItem> a, List<recipeItem> b) {
        Dictionary<string, int> aDict = new Dictionary<string, int>();
        Dictionary<string, int> bDict = new Dictionary<string, int>();
        foreach(recipeItem ri in a) {
            aDict.Add(ri.makeName, ri.makeCount);
        }
        foreach(recipeItem ri in b) {
            bDict.Add(ri.makeName, ri.makeCount);
        }

        int intersectCount = 0;

        foreach(var kv in aDict) {
            if(bDict.ContainsKey(kv.Key)) {
                // key exists in both
                intersectCount += Math.Min(kv.Value, bDict[kv.Key]);
            }
        }
        print("ICICIC = " + intersectCount);
        return intersectCount;
    }

    int countDiff(List<recipeItem> a, List<recipeItem> b) {
        return 1;
    }

    List<recipeItem> ChooseThingsToRecomposeFromRecipe(List<recipeItem> gobjs) {

        // debug
        foreach(recipeItem ri in gobjs) {
            print("=-=");
            print(ri.makeName);
            print(ri.makeCount);
        }
//        recipeList = PLAYDATE_RecipeLoaderSingleton.recipeDict[name];

        // iterate through recipes and calculate score for each recipe.

 //       float coeffUsed = 1.0f;
  //      float coeffNeeded = -1.0f;
   //     float coeffLeft = 0.2f;
        float bestScore = -999.0f;
        string bestResult = "";

        foreach(var kv in PLAYDATE_RecipeLoaderSingleton.recipeDict) {
            var thisRecipeResult = kv.Key; // e.g. "treeA"
            List<recipeItem> thisRecipe = kv.Value;

            int cused = countIntersect(gobjs, thisRecipe);
            int cleft = countDiff(gobjs, thisRecipe);
            int cneeded = countDiff(thisRecipe, gobjs);

            float score = coeffUsed * cused + coeffLeft * cleft + coeffNeeded * cneeded;

            if(score > bestScore) {
                bestScore = score;
                bestResult = thisRecipeResult;
            }
        }

        if(bestResult == "") { 
            return null;
        }

        List<recipeItem> bestRecipe = PLAYDATE_RecipeLoaderSingleton.recipeDict[bestResult];


        return bestRecipe;

    }

    recomposeChoice calculateChoice(List<recipeItem> cr) {
        List<recipeItem> oCreated, oUsed, oNeeded, oDestroy;
        oCreated = new List<recipeItem>();
        oUsed = new List<recipeItem>();
        oNeeded = new List<recipeItem>();
        oDestroy = new List<recipeItem>();

       //test
        oCreated.Add(new recipeItem("log", 1));
        oCreated.Add(new recipeItem("treeB", 6));

        recomposeChoice ourChoice = new recomposeChoice(oCreated, oUsed, oNeeded, oDestroy);
        return ourChoice;
    }
    
    int DestroyAndCreate(recomposeChoice ch) {

        // DESTROY OBJECTS
        // out of tagged objects,
            // find appropriate number of used objects and destroy them (random order)
            // find appropriate number of needed objecs and destroy them (TODO MAYBE)
            // LEAVE THE LEFTOVER ALONE!
              

        // CREATE OBJECTS
        Vector3 thisPos = this.gameObject.transform.position;
        foreach(recipeItem ri in ch.objsCreated) {
            VRTK.Examples.PLAYDATE_DecomposeThing.InstantiatePrefab (ri.makeName, ri.makeCount, thisPos);
        }

        return 1;
    }

}

}
