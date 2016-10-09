
namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections.Generic;

    public class PLAYDATE_DecomposeThing : VRTK_InteractableObject
    {
        
        private GameObject myself = null;
        private static string prefabDir = "Things/";
        private static string parent = "AllThings";
        public static float instantiateRange = 1.1f;

        private string thingToMake;
        private static GameObject parentObject;

        
        void Awake() {
            print("awakeawakeDecompose");
        }
        

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            print("WERE'GONNADECOMPOPSE_++");
            print("usingUSINGUSING " + usingObject);


            Decompose();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
        }


        public static void InstantiatePrefab(string name, int count, Vector3 thisPos) {
            print("INSTANTIATING PREFAB: " + name + " x" + count + " at: " + thisPos);
            parentObject = GameObject.Find(parent);
            for(int i = 0; i < count; i++) {
                Vector3 randPos = new Vector3(Random.Range(-1 * instantiateRange, instantiateRange), Random.Range(-0.0f * instantiateRange, instantiateRange), Random.Range(-1 * instantiateRange, instantiateRange));

                GameObject go = (GameObject)Instantiate(Resources.Load(prefabDir + name), thisPos + randPos, Quaternion.identity);
                go.transform.parent = parentObject.transform;
                go.name = name;
            }
        }

        void Decompose() {

            // DESTROY

            Vector3 thisPos = this.gameObject.transform.position;
            string thisName = this.gameObject.name;


            // GetRecipe
            print("STARTING RECIPE");

            List<recipeItem> recipeList = GetThingsToMakeFromRecipe(thisName);
            if(recipeList == null) {
                // either can't decompose or item doesn't exit.
                return;
            }
           
            print("DESTROYING OBJECT");
            Destroy(this.gameObject);
            
            foreach(recipeItem ri in recipeList) {
            
                print("   ..creating object in recipe list ");
                InstantiatePrefab(ri.makeName, ri.makeCount, thisPos);

            }

            print ("ENDING RECIPE");
        }

        public List<recipeItem> GetThingsToMakeFromRecipe(string name) {

            List<recipeItem> recipeList = new List<recipeItem>();


            if(PLAYDATE_RecipeLoaderSingleton.recipeDict == null) {
                Debug.LogError("Item " + name + " does not exist in recipe!");
                return null;
            }
            if(PLAYDATE_RecipeLoaderSingleton.recipeDict[name].Count <= 0) {
                Debug.Log("Item " + name + " can't be decomposed any further.");
                return null;
            }
    
            print("looking up " + name);
            recipeList = PLAYDATE_RecipeLoaderSingleton.recipeDict[name];

/*
            for(int i = 0; i < 6; i++) {
                recipeItem ri = new recipeItem();
                // this relies on the fact that the prefab name has to be identical to the object name

                if(name == "treeA") {
                    ri.makeName = "log";
                } else {
                    ri.makeName = "treeA";
                }
                //ri.makeName = name;

                ri.makeCount = 2; 

                recipeList.Add(ri);
            }    
*/

            return recipeList;
        }

    }
}
