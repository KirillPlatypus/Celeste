using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace PlayerObject.Coin
{
    [CreateAssetMenu(fileName = "ActiveCoin", menuName = "ActiveCoinObject", order = 0)]
    
    public class ActiveCoins : ScriptableObject 
    {
        
        public List<string> SampleSceneCoin;
        public List<string> Level2Coin;
        private List<string> Null;

        public List<string> GetListForSpecificScene(string SceneName)
        {
            if(SceneName == "SampleScene")
            {
                return SampleSceneCoin;
            }
            else if(SceneName == "Level2")
            {
                return Level2Coin;
            }
            return Null;
        }
    }
}