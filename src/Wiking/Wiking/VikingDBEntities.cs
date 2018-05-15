using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace Wiking
{
    public class VikingDBEntities : DbContext
    {
        public VikingDBEntities(IDatabaseInitializer<VikingDBEntities> dataInitializer) : base("name=VikingDBContext")
        {
            /* -- Cas où le StubInitializer n'existait pas
            // On définit notre stratégie d'initialisation de la base de données
            // Change fois que le model connaît un changement la base de données sera recrée
            Database.SetInitializer<VikingDBEntities>(new DropCreateDatabaseIfModelChanges<VikingDBEntities>());
            */

            Database.SetInitializer<VikingDBEntities>(dataInitializer);
        }

        //Création de multiple DbSet<> dans le but d'accèder plus facilement aux données si besoins.
        public virtual DbSet<Utilisateur> UserSet { get; set; }

        public virtual DbSet<Navire> NavireSet { get; set; }

        public virtual DbSet<Viking> VikingSet { get; set; }

        public virtual DbSet<Divinite> DiviniteSet { get; set; }

        public virtual DbSet<Arme> ArmeSet { get; set; }

        public virtual DbSet<Freya> FreyaSet { get; set; }

        //Méthode pour la création du modèle
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Code Obsolète - Code First Fluent API
            ////Nom de la table
            //modelBuilder.Entity<Viking>().ToTable("Viking");

            ////Clé primaire de la table
            //modelBuilder.Entity<Viking>().HasKey(n => n.UniqueId);

            ////ici on explique que c'est lors de l'insertion en base que la clef primaire sera générée
            // ...

            ////Contraintes sur les attributs, hérité de par la classe Freya

            //modelBuilder.Entity<Viking>().Property(n => n.Name).IsRequired()
            //                                                    .HasMaxLength(42);
            //modelBuilder.Entity<Viking>().Property(n => n.Image).IsRequired();
            //modelBuilder.Entity<Viking>().Property(n => n.Text1).IsRequired();
            //modelBuilder.Entity<Viking>().Property(n => n.Text2).IsRequired();

            ////Contraintes sur les attributs propres à la classe Viking

            //modelBuilder.Entity<Viking>().Property(n => n.Sexe).IsRequired();

            ////Définitions des noms de colonnes dans la base de données

            //modelBuilder.Entity<Viking>().Property(n => n.Name).HasColumnName("Nom");
            //modelBuilder.Entity<Viking>().Property(n => n.Image).HasColumnName("Image");
            //modelBuilder.Entity<Viking>().Property(n => n.Text1).HasColumnName("Text Bref");
            //modelBuilder.Entity<Viking>().Property(n => n.Text2).HasColumnName("Text Long");
            //modelBuilder.Entity<Viking>().Property(n => n.Sexe).HasColumnName("Sexe");
            //modelBuilder.Entity<Viking>().Property(n => n.Pere).HasColumnName("Père");
            //modelBuilder.Entity<Viking>().Property(n => n.Mere).HasColumnName("Mère");
            //modelBuilder.Entity<Viking>().Property(n => n.Dtn).HasColumnName("Naissance");
            //modelBuilder.Entity<Viking>().Property(n => n.Dtd).HasColumnName("Decès");
            #endregion


            // Génération des tables par héritages de la classe Freya.
            modelBuilder.Entity<Viking>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Vikings");
            });

            modelBuilder.Entity<Arme>().Map(f =>
            {
                f.MapInheritedProperties();
                f.ToTable("Arme");
            });

            modelBuilder.Entity<Divinite>().Map(n =>
            {
                n.MapInheritedProperties();
                n.ToTable("Divinité");
            });

            modelBuilder.Entity<Navire>().Map(g =>
            {
                g.MapInheritedProperties();
                g.ToTable("Navire");
            });

            // Création de la Table Utilisateur
            // Ajout des restrictions nécessaire.
            modelBuilder.Entity<Utilisateur>().ToTable("User");
            modelBuilder.Entity<Utilisateur>().HasKey(n => n.UniqueId);
            // Unique Id permettant la génération de la table.
            modelBuilder.Entity<Utilisateur>().Property(n => n.UniqueId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Utilisateur>().Property(n => n.Login).IsRequired();
            modelBuilder.Entity<Utilisateur>().Property(n => n.Pass).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Fonction qui prend un viking en paramètre, et l'ajoute à la base de données.
        /// </summary>
        /// <param name="o"></param>
        public static void AddDBViking(Viking o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                Boolean j = false;
                foreach (var i in db.VikingSet)
                {
                    if (i.Equals(o))
                    {
                        j = true;
                    }
                }
                if ( j == false)
                {
                    db.VikingSet.Add(o);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Fonction qui prend un viking en paramètre, et le supprime de la base de données où il est présent
        /// </summary>
        /// <param name="o"></param>
        public static void RemoveDBViking(Viking o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                foreach ( var i in db.VikingSet)
                {
                    if (i.Equals(o))
                    {
                        db.VikingSet.Remove(i);
                        
                    }
                }
                db.SaveChanges();
            }
        }

        #region Add/Remove_Others
        public static void AddDBDivinite(Divinite o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                Boolean j = false;
                foreach (var i in db.DiviniteSet)
                {
                    if (i.Equals(o))
                    {
                        j = true;
                    }
                }
                if (j == false)
                {
                    db.DiviniteSet.Add(o);
                    db.SaveChanges();
                }
            }
        }

        public static void RemoveDBDivinite(Divinite o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                foreach (var i in db.DiviniteSet)
                {
                    if (i.Equals(o))
                    {
                        db.DiviniteSet.Remove(i);

                    }
                }
                db.SaveChanges();
            }
        }

        public static void AddDBArme(Arme o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                Boolean j = false;
                foreach (var i in db.ArmeSet)
                {
                    if (i.Equals(o))
                    {
                        j = true;
                    }
                }
                if (j == false)
                {
                    db.ArmeSet.Add(o);
                    db.SaveChanges();
                }
            }
        }

        public static void RemoveDBArme(Arme o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                foreach (var i in db.ArmeSet)
                {
                    if (i.Equals(o))
                    {
                        db.ArmeSet.Remove(i);

                    }
                }
                db.SaveChanges();
            }
        }

        public static void AddDBNavire(Navire o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                Boolean j = false;
                foreach (var i in db.NavireSet)
                {
                    if (i.Equals(o))
                    {
                        j = true;
                    }
                }
                if (j == false)
                {
                    db.NavireSet.Add(o);
                    db.SaveChanges();
                }
            }
        }

        public static void RemoveDBNavire(Navire o)
        {
            using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
            {
                foreach (var i in db.NavireSet)
                {
                    if (i.Equals(o))
                    {
                        db.NavireSet.Remove(i);

                    }
                }
                db.SaveChanges();
            }
        }
        #endregion

        public static void UpdateDB(Freya o)
        {

        }

        #region Utilisateur
        /// <summary>
        /// Fonction qui vérifie s'il l'utilisateur est valide, c'est à dire, s'il est dans la BDD.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static Utilisateur _IsAValidUser(String login, String pass)
        {
            try
            {
                Utilisateur u;
                using (VikingDBEntities context = new VikingDBEntities(new StubInitializer()))
                {
                    u = context.UserSet.FirstOrDefault(x => x.Login.Equals(login) && x.Pass.Equals(pass));
                }
                return u;
            }
            catch (Exception e)
            {
                MessageBox.Show($"La Connection a échoué\n {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// CreateUser est une fonction qui permet de créer un utilisateur dans BDD, ainsi, on effectue certaines vérifications pour éviter une duplication de code dans la base.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <param name="pass2"></param>
        public static void CreateUser(String login, String pass, String pass2)
        {

            if ( pass != pass2)
            {
                MessageBox.Show($"Les mots de passes ne sont pas les mêmes....");
                return;
            }
            Utilisateur o = new Utilisateur(login, pass);
            try
            {
                using (VikingDBEntities db = new VikingDBEntities(new StubInitializer()))
                {
                    Boolean j = false;
                    foreach (var i in db.UserSet)
                    {
                        if (i.Equals(o))
                        {
                            j = true;
                        }
                    }
                    if (j == false)
                    {
                        db.UserSet.Add(o);
                        db.SaveChanges();
                    }
                    else MessageBox.Show($"Utilisateur déjà existant...");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"La création a échoué...\n{e.Message}");
                return;
            }
        }
        #endregion
        /// <summary>
        /// Fonction qui va recupérer tous les vikings présents dans la BDD par l'intermédiaire du DbSet<> ici VikingSet.
        /// </summary>
        /// <returns></returns>
        public static List<Viking> GetAllVikings()
        {
            List<Viking> l;
                using (VikingDBEntities context = new VikingDBEntities(new StubInitializer()))
                {
                    AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
                    l = context.VikingSet.ToList();
                }
            return l;
        }

        #region GetAllOthers
        //
        // -- Même principe que le GetAllViking, mais par manque de temps, ces fonctions n'ont pas encore été implémenté.
        // -- De plus, il est sans doute possible d'éviter cette duplication de code.
        public static List<Arme> GetAllArmes()
        {
            List<Arme> l;
            using (VikingDBEntities context = new VikingDBEntities(new StubInitializer()))
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
                l = context.ArmeSet.ToList();
            }
            return l;
        }

        public static List<Navire> GetAllNavires()
        {
            List<Navire> l;
            using (VikingDBEntities context = new VikingDBEntities(new StubInitializer()))
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
                l = context.NavireSet.ToList();
            }
            return l;
        }

        public static List<Divinite> GetAllDivinites()
        {
            List<Divinite> l;
            using (VikingDBEntities context = new VikingDBEntities(new StubInitializer()))
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
                l = context.DiviniteSet.ToList();
            }
            return l;
        }
        #endregion 

    }
}