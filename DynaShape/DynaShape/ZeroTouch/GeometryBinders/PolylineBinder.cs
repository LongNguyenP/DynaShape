using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.GeometryBinders;

/// <summary>
/// Wrapper class for PolylineBinder
/// </summary>
public class PolylineBinder
{
    private PolylineBinder(){}

    /// <summary>
    /// Create a PolylineBinder.
    /// </summary>
    /// <param name="vertices">The vertices of the Polyline.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <param name="loop">An optional setting to force the Polyline to be a closed loop.</param>
    /// <returns name="polylineBinder"></returns>
    [NodeCategory("Create")]
    public static DynaShape.GeometryBinders.PolylineBinder ByPoints(
        List<Point> vertices,
        [DefaultArgument("null")] Color color,
        [DefaultArgument("false")] bool loop)
    {
        var binder = TracingUtils.GetObjectFromTrace<DynaShape.GeometryBinders.PolylineBinder>();

        binder.Initialize(
            vertices.ToTriples(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor,
            loop);

        return binder;
    }
}