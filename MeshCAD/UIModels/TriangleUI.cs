using HelixToolkit.Wpf;
using MeshCAD.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeshCAD.UIModels
{
    public class TriangleUI : BelongingUIElement
    {
        public TriangleUI(Elements.Triangle triangle)
        {
            ModelElement = triangle;
            var trinagleVisual = new TriangleVisual3D();
            trinagleVisual.FirstPoint = triangle.Vertices[0].Point.Multiply(SCALE_FACTOR);
            trinagleVisual.SecondPoint = triangle.Vertices[1].Point.Multiply(SCALE_FACTOR);
            trinagleVisual.ThirdPoint = triangle.Vertices[2].Point.Multiply(SCALE_FACTOR);
            VisualElement = trinagleVisual;
            BaseMaterial = MaterialHelper.CreateMaterial((Color)ColorConverter.ConvertFromString(Colors[triangle.BoardNumber]));
            Title = "Треугольник №" + triangle.Number;
            //(triangle.Vertices[0].Point, triangle.Vertices[0].Point, triangle.Vertices[2].Point)
        }

        public class TriangleVisual3D : MeshElement3D
        {
            /// <summary>
            /// Identifies the <see cref="FirstPoint"/> dependency property.
            /// </summary>
            public static readonly DependencyProperty FirstPointProperty = DependencyProperty.Register(
                "FirstPoint",
                typeof(Point3D),
                typeof(TriangleVisual3D),
                new PropertyMetadata(new Point3D(0, 0, 0), GeometryChanged));

            /// <summary>
            /// Identifies the <see cref="SecondPoint"/> dependency property.
            /// </summary>
            public static readonly DependencyProperty SecondPointProperty = DependencyProperty.Register(
                "SecondPoint",
                typeof(Point3D),
                typeof(TriangleVisual3D),
                new PropertyMetadata(new Point3D(0, 0, 0), GeometryChanged));

            /// <summary>
            /// Identifies the <see cref="ThirdPoint"/> dependency property.
            /// </summary>
            public static readonly DependencyProperty ThirdPointProperty = DependencyProperty.Register(
                "ThirdPoint",
                typeof(Point3D),
                typeof(TriangleVisual3D),
                new PropertyMetadata(new Point3D(0, 0, 0), GeometryChanged));

            /// <summary>
            /// Gets or sets the first point of triangle.
            /// </summary>
            /// <value>The center.</value>
            public Point3D FirstPoint
            {
                get
                {
                    return (Point3D)this.GetValue(FirstPointProperty);
                }

                set
                {
                    this.SetValue(FirstPointProperty, value);
                }
            }

            /// <summary>
            /// Gets or sets the second point of triangle.
            /// </summary>
            /// <value>The center.</value>
            public Point3D SecondPoint
            {
                get
                {
                    return (Point3D)this.GetValue(SecondPointProperty);
                }

                set
                {
                    this.SetValue(SecondPointProperty, value);
                }
            }

            /// <summary>
            /// Gets or sets the third point of triangle.
            /// </summary>
            /// <value>The center.</value>
            public Point3D ThirdPoint
            {
                get
                {
                    return (Point3D)this.GetValue(ThirdPointProperty);
                }

                set
                {
                    this.SetValue(ThirdPointProperty, value);
                }
            }

            /// <summary>
            /// Do the tessellation and return the <see cref="MeshGeometry3D"/>.
            /// </summary>
            /// <returns>A triangular mesh geometry.</returns>
            protected override MeshGeometry3D Tessellate()
            {
                var builder = new MeshBuilder(true, true);
                builder.AddTriangle(FirstPoint, SecondPoint, ThirdPoint);
                return builder.ToMesh();
            }
        }
    }
}
