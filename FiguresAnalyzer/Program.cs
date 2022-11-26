using System;

using GeometryFigures;

namespace FiguresAnalyzer
{
    public class Program
    {
        private static void Main(string[] args)
            => new Program().Run();

        public void Run()
        {
            ChangeLocale();
            EqTriangle eqTriangle = new EqTriangle(0, 0, 1);
            Console.WriteLine(eqTriangle.Area());
            Console.WriteLine(eqTriangle.CircumscribedCircleRadius());
            Console.WriteLine(eqTriangle.ToString());
        }

        private void ChangeLocale()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Threading.Thread.CurrentThread.CurrentCulture
                = new System.Globalization.CultureInfo("en-US", false);
            System.Threading.Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo("en-US", false);
        }
    }
}