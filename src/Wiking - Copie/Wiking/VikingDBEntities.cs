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

            modelBuilder.Entity<Utilisateur>().ToTable("User");
            modelBuilder.Entity<Utilisateur>().HasKey(n => n.UniqueId);
            modelBuilder.Entity<Utilisateur>().Property(n => n.UniqueId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Utilisateur>().Property(n => n.Login).IsRequired();
            modelBuilder.Entity<Utilisateur>().Property(n => n.Pass).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public static void AddDB(Viking o)
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


        
        public static void RemoveDB(Viking o)
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

        public static void UpdateDB(Freya o)
        {

        }

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

    }
}