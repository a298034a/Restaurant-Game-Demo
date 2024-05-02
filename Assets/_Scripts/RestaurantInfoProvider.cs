using UnityEngine;

namespace Restaurant 
{
    public class RestaurantInfoProvider
    {
        public RestaurantInfo Info => _info;
        private RestaurantInfo _info;

        public RestaurantInfoProvider()
        {
            //WIP: �M�s�ɨ��o�ثe��T
            _info = new RestaurantInfo();
        }

        private static RestaurantInfoProvider _instance;
        public static RestaurantInfoProvider Ins
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestaurantInfoProvider();
                }
                return _instance;
            }
        }
    }
}
