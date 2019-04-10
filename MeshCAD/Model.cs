using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshCAD
{
    class Model
    {
        public int VerticesCount;
        public int BoardCount;
        public int PlateEvenStepCount;
        public int PlateFixedStepCount;
        public int RodCount;
        public int TriangleCount;
        public int RectangleCount;
        public int EtazherkaCount;
        public int AdditionalRodVerticesCount = 0;
        public int MaterialsCount = 0;
        public int FixedLinesCountX = 0;
        public int FixedLinesCountY = 0;
        public int FixedLinesCountZ = 0;

        public List<Material> Materials = new List<Material>();

        public List<double> FixedLinesX = new List<double>();
        public List<double> FixedLinesY = new List<double>();
        public List<double> FixedLinesZ = new List<double>();

        public List<PlateFixedStep> PlatesFixedStep = new List<PlateFixedStep>();
        public List<PlateEvenStepOrBoard> PlatesEvenStepOrBoards = new List<PlateEvenStepOrBoard>();

        public List<Vertice> BasicVertices = new List<Vertice>();

        public List<Rectangle> Rectangles = new List<Rectangle>();

        public int MountVerticesWithoutStoikiCount;

        public List<int> MountVerticesWithoutStoiki = new List<int>();

        public List<Rod> Rods = new List<Rod>();

        public int RodTypeCount;
        public List<RodType> RodTypes = new List<RodType>();

        public int MassInVerticesCount;

        public int CheckVerticeCount;
        public List<int> CheckVertices;

        public Model(int verticesCount, 
            int boardCount, 
            int plateEvenStepCount, 
            int plateFixedStepCount, 
            int rodCount, 
            int triangleCount, 
            int rectangleCount, 
            int etazherkaCount)
        {
            VerticesCount = verticesCount;
            BoardCount = boardCount;
            PlateEvenStepCount = plateEvenStepCount;
            PlateFixedStepCount = plateFixedStepCount;
            RodCount = rodCount;
            TriangleCount = triangleCount;
            RectangleCount = rectangleCount;
            EtazherkaCount = etazherkaCount;
        }
    }

    class Material
    {
        double Coef1;
        double Coef2;
        double Coef3;
        double Coef4;
        double Coef5;

        public Material(double coef1, double coef2, double coef3, double coef4, double coef5)
        {
            Coef1 = coef1;
            Coef2 = coef2;
            Coef3 = coef3;
            Coef4 = coef4;
            Coef5 = coef5;
        }
    }

    class PlateFixedStep
    {
        public int FirstVerticeNumber;
        public int FirstPlateRectangleNumber;
        
        public int StartLineXNumber;
        public int EndLineXNumber;
        
        public int StartLineYNumber;
        public int EndLineYNumber;
        
        public int StartLineZNumber;
        public int EndLineZNumber;
        
        public int MaterialTypeNumber;
    }

    class PlateEvenStepOrBoard
    {
        public int FirstVerticeNumber;
        public int FirstRectangleNumber;

        public List<Vertice> BaseVertices = new List<Vertice>();

        public int Lines1To3VerticeCount;
        public int Lines1To2VerticeCount;

        public int MaterialTypeNumber;

        public int GeneralizedElementsCount;
        public List<GeneralizedElement> GeneralizedElements = new List<GeneralizedElement>();
    }

    class GeneralizedElement {
        public int LeftVerticalLineNumber;
        public int RightVerticalLineNumber;
        public int BottomHorizontalLineNumber;
        public int TopHorizontalLineNumber;

        public double Mass;
        public double CoeffZapoln;
    }

    class Rod
    {
        public int FirstVerticeNumber;
        public int SecondVerticeNumber;
        public int ThirdVerticeNumber;

        public int MaterialTypeNumber;
    }

    class RodType
    {
        public List<double> Coeffs = new List<double>();
    }

    class Vertice
    {
        double X, Y, Z;

        public Vertice(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class Rectangle {
        public int FirstVerticeNumber;
        public int SecondVerticeNumber;
        public int ThirdVerticeNumber;
        public int ForthVerticeNumber;

        public int MaterialTypeNumber;

        public Rectangle(int firstVerticeNumber, int secondVerticeNumber, int thirdVerticeNumber, int forthVerticeNumber, int materialTypeNumber)
        {
            FirstVerticeNumber = firstVerticeNumber;
            SecondVerticeNumber = secondVerticeNumber;
            ThirdVerticeNumber = thirdVerticeNumber;
            ForthVerticeNumber = forthVerticeNumber;
            MaterialTypeNumber = materialTypeNumber;
        }
    }


}
