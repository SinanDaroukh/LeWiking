using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiking
{
    public class Utilisateur
    {
        private string _login;
        private string _pass;



        public Guid UniqueId { get; set; }
        public String Login
        {
            get
            {
                return _login;
            }

            set
            {
                _login = value;
            }
        }

        public String Pass
        {
            get
            {
                return _pass;
            }

            set
            {
                _pass = value;
            }
        }

        public Utilisateur() { }

        public Utilisateur(string login, string pass)
        {
            Login = login;
            Pass = pass;
        }

        #region Méthodes à redéfinir
        public override bool Equals(object right)
        {
            //check null
            if (object.ReferenceEquals(right, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, right))
            {
                return true;
            }

            if (this.GetType() != right.GetType())
            {
                return false;
            }

            return this.Equals(right as Utilisateur);
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode() + 17 * Pass.GetHashCode();
        }

        public bool Equals(Utilisateur other)
        {
            return (this.Login.Equals(other.Login) && this.Pass.Equals(other.Pass));
        }

        public override string ToString()
        {
            return $"\nIdentifiant : {UniqueId} \nLogin : {Login} \nPass :{Pass}\n";
        }
        #endregion
    }
}
