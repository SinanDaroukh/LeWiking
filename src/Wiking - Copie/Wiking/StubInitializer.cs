using System;
using System.Data.Entity;


namespace Wiking
{
    // StubInitializer necessaire à l'initialisation de la base de données
    // Il hérite de l'interface DropCreateDBAlways<>
    // Et introduit les données stubbées à la base de données
    // DropCreateDatabaseIfModelChanges
    // DropCreateDatabaseAlways
    public class StubInitializer : DropCreateDatabaseIfModelChanges<VikingDBEntities> 
    {
        protected override void Seed(VikingDBEntities context) 
        {

            // Données Stubées
            Viking vik1 = new Viking("Baptistos Urbanos", "khaldrogo.jpg", "Baptistos Urbanos, Mi Dieu-Mi Viking.", "Le grand Batistos Urbanos a bati sa légende sur le premier programme sur bouclier. Il tapait ses éléves avec mais finissait par leur mettre des supers notes.\nDoté d'un physique hors norme et d'un charisme hors du commun, la légende raconte qu'il rigola à une blague que lui fire deux de ses disciples, Sinanos et Fabienos, jumeaux un peu débiles mais plein de bonne volonté, et leur mis 20/20.", Sexe.M, "Freya", "Thor", new DateTime(3200, 3, 1), new DateTime(3500, 2, 1));
            Viking vik2 = new Viking("Sinanos Débilos", "sinanos.jpg", "Sinanos Débilos, Nain Viking.", "Sinan Débilos, est un combattant ridicule. Il se retrouve assez souvent dans la hûte des soins pour draguer l'infirmière Laguerta.\nIl a une barbe rousse et louche un peu. Son air sournois est destabilisant, pas de quoi inquiéter le grand Baptistos Urbanos qui a fait de Sinanos Débilos sa victime préférée.", Sexe.M, "Geneviève la conquérante", "Mermhoud Débilos", new DateTime(2000, 3, 1), new DateTime(2000, 3, 9));
            Viking vik3 = new Viking("Fabienos Débilos", "fabienos.jpg", "Fabienos Débilos, Viking Simplet.", "Fabienos Débilos est un barde, il a pour vocation de détendre les guerriers lors des assaults contre le royaume britannique. A l'âge de 13 ans, il tailla ,dans son fourreau d'épée, son tout premier pipot.\nUne mésaventure lui arriva sur le champ de bataille, il ingéra sa flûte de pan. Ainsi depuis ce jour, il ne peut que s'exprimer qu'en sifflant certaines mélodies.", Sexe.M, "Geneviève la conquérante", "Mermhoud Débilos", new DateTime(2000, 3, 1), new DateTime(2000, 3, 9));
            Viking vik4 = new Viking("Ragnar", "ragnar.jpg", "un viking très méchant", "enft il est gentil", Sexe.M, "Mama", "Papa", new DateTime(1999, 2, 1), new DateTime(2100, 2, 1));
            Viking vik5 = new Viking("Lagartha", "lagartha.png", "une gentille fille", "eeeee", Sexe.F, "Mama1", "Papa2", new DateTime(1999, 2, 1), new DateTime(2100, 2, 1));
            Viking vik6 = new Viking("Abdd", "abdd.jpg", "une gentille bdd", "enft elle est méchante", Sexe.F, "Mama1", "Papa2", new DateTime(1949, 2, 4), new DateTime(2000, 2, 1));
            Viking vik7 = new Viking("Sparto", "sparto.jpeg", "Il est costaud mais n'a rien dans le cerveau...", "Il a une hache très tranchante, un casque à double pointe, torse nu. Son allure de gladiateur est impressionante. Son anneau dans le nez est intimidant mais son asthme ragaillardit un peu ses adversaires. Il reste néanmoins très redouté.", Sexe.M, "Tharta", "Lakrem", new DateTime(2100, 9, 4), new DateTime(2100, 9, 4));           
            Viking vik8 = new Viking("Floki", "floki.png", "une 'gentille fiFle", "Une chanson douce que me chantait ma maman, grave cool la chanson o woooaaah ", Sexe.M, "Mama1", "Papa2", new DateTime(1999, 2, 1), new DateTime(2100, 2, 1));
            Viking vik9 = new Viking("Arthur Tronche", "arthur.jpg", "Viking Programmeur.", "Dôté d'une force surhumaine, il brise le crâne de ses adversaires avec son clavier en pierre.", Sexe.M, "Marguerite", "Jean-Luc", new DateTime(1998,5, 26), new DateTime(2098, 5, 26));  

            Divinite dvn1 = new Divinite("Freya", "freya.png", "Une jolie déesse", "eft elle est moche", "Divinité de la Guerre");
            Divinite dvn2 = new Divinite("Thor", "thor.png", "Un joli dieu", "eft il est moche", "Dieu des Dieux");
            Divinite dvn3 = new Divinite("Loki", "loki.png", "Un dieu fou", "eft il est moche", "Divinité de la Folie");

            Navire bto = new Navire("Bateau d'Oseberg", "oseberg.png", "un grand bateau", "eft il est géant", 1, 1, 2);

            Arme swd = new Arme("Excalibur", "excalibur.png", "Une longue épee", "Portée par le roi Arthur");
            Arme mto = new Arme("Marteau de Thor", "martho.png", "Un marteau bien dur", "Portée par Thor");

            Utilisateur sin = new Utilisateur("Sinan", "dracula");
            Utilisateur fab = new Utilisateur("Fabien", "cruela");
            Utilisateur urb = new Utilisateur("Urbain", "baptiste");

            context.UserSet.AddRange(new Utilisateur[] { sin, fab, urb });

            context.VikingSet.AddRange(new Viking[] { vik1, vik2, vik3, vik4, vik5, vik6, vik7, vik8, vik9 });

            context.NavireSet.AddRange(new Navire[] { bto });

            context.DiviniteSet.AddRange(new Divinite[] { dvn1, dvn2, dvn3 });

            context.ArmeSet.AddRange(new Arme[] { swd, mto });

            base.Seed(context);
        }
    }
}
