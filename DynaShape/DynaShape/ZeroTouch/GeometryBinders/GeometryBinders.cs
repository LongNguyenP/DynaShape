using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynaShape.GeometryBinders;
using HelixToolkit.SharpDX.Core;
using Color = DSCore.Color;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DynaShape.ZeroTouch.GeometryBinders;


/// <summary>
/// Wrapper class for GeometryBinders
/// </summary>
public class GeometryBinder
{
    private GeometryBinder()
    {
    }

    /// <summary>
    /// Modify a geometry binder's color.
    /// </summary>
    /// <param name="geometryBinder">The GeometryBinder to modify.</param>
    /// <param name="color">The new color to apply.</param>
    /// <returns></returns>
    public static DynaShape.GeometryBinders.GeometryBinder ChangeColor(DynaShape.GeometryBinders.GeometryBinder geometryBinder, Color color)
    {
        geometryBinder.Color = color.ToColor4();
        return geometryBinder;
    }

    /// <summary>
    /// Modify a geometry binder's visibility.
    /// </summary>
    /// <param name="geometryBinder">The GeometryBinder to modify.</param>
    /// <param name="isVisible">Is the binder visible?</param>
    /// <returns></returns>
    public static DynaShape.GeometryBinders.GeometryBinder ToggleVisibility(DynaShape.GeometryBinders.GeometryBinder geometryBinder, bool isVisible)
    {
        geometryBinder.Show = isVisible;
        return geometryBinder;
    }
}