using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;

namespace DynaShape.ZeroTouch.GeometryBinders.GeometryBinders;

/// <summary>
/// Wrapper class for circle binder
/// </summary>
public class CircleBinder
{
    private CircleBinder() { }

    /// <summary>
    /// Create a CircleBinder to preview circle geometry.
    /// </summary>
    /// <param name="center">The center of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="planeNormal">An optional normal for the circle's plane.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="circleBinder"></returns>
    public static DynaShape.GeometryBinders.CircleBinder ByCenterRadius(
        Point center,
        float radius,
        [DefaultArgument("Vector.ZAxis()")] Vector planeNormal,
        [DefaultArgument("null")] Color color)
    {
        return new DynaShape.GeometryBinders.CircleBinder(
            center.ToTriple(),
            radius,
            planeNormal.ToTriple(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
    }
}