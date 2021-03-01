using System.Collections.Generic;
using ServiceStack;
using RedisGeo.ServiceModel;
using ServiceStack.Redis;

namespace RedisGeo.ServiceInterface
{
    public class RedisGeoServices : Service
    {
        public object Any(FindGeoResults request)
        {
            var stateCode = Redis.Get<string>("mapping:" + request.State);
            if (stateCode == null)
                return new List<RedisGeoResult>();
            var results = Redis.FindGeoResultsInRadius(stateCode, 
                longitude: request.Lng, latitude: request.Lat,
                radius: request.WithinKm.GetValueOrDefault(20), unit: RedisGeoUnit.Kilometers,
                sortByNearest: true);

            return results;
        }
    }
}