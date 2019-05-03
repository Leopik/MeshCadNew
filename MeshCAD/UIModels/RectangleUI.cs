using HelixToolkit.Wpf;
using MeshCAD.Elements;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeshCAD.UIModels
{
    public class RectangleUI : BelongingUIElement
    {
        public const float EPS = 0.01f;
        private double GetAngleABC(Point3D a, Point3D b, Point3D c)
        {
            double[] ab = { b.X - a.X, b.Y - a.Y, b.Z - a.Z };
            double[] bc = { c.X - b.X, c.Y - b.Y, c.Z - b.Z };

            double abVec = Math.Pow(ab[0] * ab[0] + ab[1] * ab[1] + ab[2] * ab[2], 0.5);
            double bcVec = Math.Pow(bc[0] * bc[0] + bc[1] * bc[1] + bc[2] * bc[2], 0.5);

            double[] abNorm = { ab[0] / abVec, ab[1] / abVec, ab[2] / abVec };
            double[] bcNorm = { bc[0] / bcVec, bc[1] / bcVec, bc[2] / bcVec };

            double res = abNorm[0] * bcNorm[0] + abNorm[1] * bcNorm[1] + abNorm[2] * bcNorm[2];

            return Math.Acos(res) * 180.0 / Math.PI;
        }
        Rectangle Rectangle;
        public RectangleUI(Rectangle rectangle)
        {
            ModelElement = rectangle;
            Rectangle = rectangle;
            var rect = new RectangleVisual3D();

            double angle = GetAngleABC(rectangle.Vertices[0].Point, rectangle.Vertices[1].Point, rectangle.Vertices[2].Point);
            Point3D diagonalPoint, widthPoint, lengthPoint;
            Point3D basePoint = rectangle.Vertices[0].Point;
            if (IsDoubleEqual(90, angle))
            {
                //Vertice[0] and Vertice[2] - diagonal
                diagonalPoint = rectangle.Vertices[2].Point;
                widthPoint = rectangle.Vertices[3].Point;
                lengthPoint = rectangle.Vertices[1].Point;
            }
            else
            {
                //Vertice[0] and Vertice[3] - diagonal
                diagonalPoint = rectangle.Vertices[3].Point;
                widthPoint = rectangle.Vertices[2].Point;
                lengthPoint = rectangle.Vertices[1].Point;
            }

            diagonalPoint = diagonalPoint.Multiply(SCALE_FACTOR);
            widthPoint = widthPoint.Multiply(SCALE_FACTOR);
            lengthPoint = lengthPoint.Multiply(SCALE_FACTOR);
            basePoint = basePoint.Multiply(SCALE_FACTOR);

            rect.LengthDirection = lengthPoint - basePoint;
            var widthDirection = widthPoint - basePoint;

            //check if it is really a rectangle
            if (!(IsDoubleEqual(Vector3D.DotProduct(widthDirection, rect.LengthDirection), 0)
                && IsDoubleEqual(Vector3D.DotProduct(rect.LengthDirection, lengthPoint - diagonalPoint), 0)
                && IsDoubleEqual(Vector3D.DotProduct(widthDirection, widthPoint - diagonalPoint), 0)))
                throw new Exception($"Прямоугольник №{rectangle.Number}: четыре точки №{rectangle.Vertices[0].Number}, " +
                    $"№{rectangle.Vertices[1].Number}, " +
                    $"№{rectangle.Vertices[2].Number}, " +
                    $"№{rectangle.Vertices[3].Number}, " +
                    $"не формируют прямоугльник");

            rect.Normal = Vector3D.CrossProduct(rect.LengthDirection, widthDirection);
            rect.Origin = new Point3D((basePoint.X + diagonalPoint.X) / 2,
                (basePoint.Y + diagonalPoint.Y) / 2,
                (basePoint.Z + diagonalPoint.Z) / 2);

            rect.Width = basePoint.DistanceTo(widthPoint);
            rect.Length = basePoint.DistanceTo(lengthPoint);
            VisualElement = rect;
            BaseMaterial = MaterialHelper.CreateMaterial((Color)ColorConverter.ConvertFromString(Colors[rectangle.BoardNumber]));

            Title = "Прямоугольник №" + rectangle.Number; 
        }

        public bool IsDoubleEqual(double a, double b)
        {
            return ((a - EPS < b) && (b < a + EPS)) ;
        }
    }
}
