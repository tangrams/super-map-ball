using Mapzen.VectorData;
using System;
using UnityEngine;

namespace Mapzen.Unity
{
    [Serializable]
    public class LayerStyle
    {
        public PolygonOptions PolygonBuilder;

        public PolylineOptions PolylineBuilder;

        public PolygonOptions GetPolygonOptions(Feature feature, float inverseTileScale)
        {
            var options = PolygonBuilder;

            if (options.MaxHeight == 0.0f)
            {
                object heightValue;
                if (feature.TryGetProperty("height", out heightValue) && heightValue is double)
                {
                    options.MaxHeight = (float)(double)heightValue;
                }
            }

            if (options.MinHeight == 0.0f)
            {
                object heightValue;
                if (feature.TryGetProperty("min_height", out heightValue) && heightValue is double)
                {
                    options.MinHeight = (float)(double)heightValue;
                }
            }

            options.MaxHeight *= inverseTileScale;
            options.MinHeight *= inverseTileScale;

            // FIXME: This is a hack to avoid building side z-fighting.
            object identifier;
            if (feature.TryGetProperty("id", out identifier) && identifier is double)
            {
                double modx = 10;
                double mody = 100;
                double modz = 1000;
                options.Offset.x = (float)((double)identifier % modx / modx);
                options.Offset.y = (float)((double)identifier % mody / mody);
                options.Offset *= inverseTileScale;
                options.MaxHeight += (float)((double)identifier % modz / modz) * inverseTileScale;
            }

            return options;
        }

        public PolylineOptions GetPolylineOptions(Feature feature, float inverseTileScale)
        {
            var options = PolylineBuilder;

            options.Width *= inverseTileScale;
            options.MaxHeight *= inverseTileScale;

            // FIXME: This is a hack to avoid building side z-fighting.
            object identifier;
            if (feature.TryGetProperty("id", out identifier) && identifier is double)
            {
                double modz = 10;
                options.MaxHeight += (float)((double)identifier % modz / modz) * inverseTileScale;
            }

            return options;
        }
    }
}
