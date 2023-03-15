using System.Collections.Generic;
using Components.BusinessParams;
using Gameframe.SaveLoad;

namespace Utilities
{
    public static class SaveUtility
    {
        private static SaveLoadManager _saveManagerInstance;
        private static SaveLoadManager SaveManagerInstance
        {
            get
            {
                if (_saveManagerInstance == null)
                    _saveManagerInstance = SaveLoadManager.Create(
                        null, null,
                        SerializationMethodType.UnityJson);

                return _saveManagerInstance;
            }
        }

        public static void SaveProgress()
        {

        }

        public static List<BusinessData> LoadBusinessData()
        {
            return new List<BusinessData>();
        }

        public static int LoadBalance()
        {
            return 0;
        }
    }
}