using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;
using System.Data.Entity;
using static Wiking.VikingDBEntities;

namespace Wiking
{
    public class Program
    {
        static void Main(string[] args)
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

            //Viking o = new Viking("sdsds", "floki.psdsng", "une gesdsntille fille", "enft elle esdsst méchante", Sexe.M, "Mama1", "Papa2", new DateTime(1999, 2, 1), new DateTime(2100, 2, 1));
            //AddDB(o);

            List<Viking> l = GetAllVikings();
            List<Arme> a = GetAllArmes();
            List<Divinite> d = GetAllDivinites();
            List<Navire> n = GetAllNavires();

            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                //WriteLine("Base après sauvegarde des changements :");
                //foreach (var y in db.VikingSet)
                //{
                //    WriteLine($"\t{n}");
                //}
                //foreach (var y in db.UserSet)
                //{
                //    WriteLine($"\t{n}");
                //}


                foreach (var i in l)
                {
                    WriteLine($"\t{i}");
                }

                foreach (var i in n)
                {
                    WriteLine($"\t{i}");
                }

                foreach (var i in d)
                {
                    WriteLine($"\t{i}");
                }

                foreach (var i in a)
                {
                    WriteLine($"\t{i}");
                }
            }



            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
        }
    }
}