using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DynaShape;
using DynaShape.Goals;

namespace DynaShapeTests;

public class BasicTests
{
    private Solver solver;

    [SetUp]
    public void Setup()
    {
        solver = new Solver();
    }


    [Test]
    public void LengthGoalTest()
    {
        solver.Clear();
        LengthGoal lengthGoal = new LengthGoal(new Triple(0f, 0f, 0f), new Triple(2f, 0f, 0f), 0f, 1000f);
        solver.RegisterGoal(lengthGoal);
        solver.EnableMomentum = false;
        solver.Iterate();
        List<Triple> nodePositions = solver.GetNodePositions();
        Assert.AreEqual(nodePositions[0].X, 1f);
    }

    [Test]
    public void CustomizationIconResourceIsAvailable()
    {
        var customizationAssembly = Assembly.Load("DynaShape.customization");
        Assert.That(customizationAssembly.GetManifestResourceNames(), Does.Contain("DynaShapeImages.resources"));

        var resources = new ResourceManager("DynaShapeImages", customizationAssembly);
        var icon = resources.GetObject("DynaShape.ZeroTouch.GeometryBinders.CircleBinder.ByCenterRadius.Small");

        Assert.That(icon, Is.Not.Null);
        Assert.That(icon, Is.TypeOf<byte[]>());
    }

    [Test]
    public void CustomizationIconResourcesHaveSmallAndLargeVariants()
    {
        var customizationAssembly = Assembly.Load("DynaShape.customization");
        var resources = new ResourceManager("DynaShapeImages", customizationAssembly);
        var resourceSet = resources.GetResourceSet(CultureInfo.InvariantCulture, true, true);
        Assert.That(resourceSet, Is.Not.Null);

        var smallIcons = new HashSet<string>(StringComparer.Ordinal);
        var largeIcons = new HashSet<string>(StringComparer.Ordinal);
        var resourceEnumerator = resourceSet!.GetEnumerator();

        while (resourceEnumerator.MoveNext())
        {
            if (resourceEnumerator.Key is not string key)
                continue;

            var isSmall = key.EndsWith(".Small", StringComparison.Ordinal);
            var isLarge = key.EndsWith(".Large", StringComparison.Ordinal);
            if (!isSmall && !isLarge)
                continue;

            Assert.That(resourceEnumerator.Value, Is.TypeOf<byte[]>(), key);
            var iconName = key[..^(isSmall ? ".Small".Length : ".Large".Length)];
            (isSmall ? smallIcons : largeIcons).Add(iconName);
        }

        Assert.That(smallIcons, Is.Not.Empty);
        Assert.That(largeIcons, Is.EquivalentTo(smallIcons));
    }

    [Test]
    public void HangingChainTest()
    {
        solver.Clear();

        List<Triple> positions = new List<Triple>();
        for (int i = 0; i < 10; i++)
            positions.Add(new Triple(i, 0, 0));

        List<Goal> goals = new List<Goal>();

        for (int i = 0; i < 10; i++)
            goals.Add(new ConstantGoal(positions, -Triple.BasisZ));

        for (int i = 0; i < 9; i++)
            goals.Add(new LengthGoal(positions[i], positions[i + 1], 1f, 10f));

        goals.Add(new AnchorGoal(positions[0]));
        goals.Add(new AnchorGoal(positions[9]));

        foreach (Goal goal in goals)
            solver.RegisterGoal(goal);

        solver.EnableMomentum = true;

        for (int i = 0; i < 100; i++)
            solver.Iterate();

        List<Triple> nodePositions = solver.GetNodePositions();
        AreAlmostEqual(nodePositions[5], new Triple(5.361444, 0, -23.461422));
    }


    [TearDownAttribute]
    public void Dispose()
    {
        solver.Dispose();
    }


    private void AreAlmostEqual(Triple value, Triple truth, float tolerance = 0.001f)
    {
        Assert.LessOrEqual(Math.Abs(value.X - truth.X), tolerance);
        Assert.LessOrEqual(Math.Abs(value.Y - truth.Y), tolerance);
        Assert.LessOrEqual(Math.Abs(value.Z - truth.Z), tolerance);
    }
}