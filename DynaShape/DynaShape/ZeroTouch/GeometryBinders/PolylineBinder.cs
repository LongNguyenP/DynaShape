using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;

namespace DynaShape.ZeroTouch.GeometryBinders.GeometryBinders;

/// <summary>
/// Wrapper class for PolylineBinder
/// </summary>
public class PolylineBinder
{
    private PolylineBinder(){}

    /// <summary>
    /// Create a PolylineBinder to preview Polyline geometry.
    /// </summary>
    /// <param name="vertices">The vertices of the Polyline.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <param name="loop">An optional setting to force the Polyline to be a closed loop.</param>
    /// <returns name="polylineBinder"></returns>
    public static DynaShape.GeometryBinders.PolylineBinder ByVertices(
        List<Point> vertices,
        [DefaultArgument("null")] Color color,
        [DefaultArgument("false")] bool loop)
    {
        return new DynaShape.GeometryBinders.PolylineBinder(
            vertices.ToTriples(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor,
            loop);
    }
}