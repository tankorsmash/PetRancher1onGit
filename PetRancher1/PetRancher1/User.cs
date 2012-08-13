using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetRancher1
{

    public class  CurrentSession
    {
        public User user;
        public Pet pet;

        public  CurrentSession()
        {
          
        }

        public void createUser(string name = "XNA_user")
        {
            this.user = new User(name);
        }

        public void addPet(string name = "YourFirstPet")
        {

            //Gets the name of the pet.
            //string name = Tools.Prompt("What is the name of your new male Pet?");
            this.pet = new Pet(this.user, name);

        }
    }


    public class User
    {

        public Inventory inventory;
        private string _name;

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public override string ToString()
        {
            return String.Format("User:{0}", this.name);
        }

        public User(string newName = "DefaultName")
        {

            this.name = newName;
            this.inventory = new Inventory(this);

            string text = String.Format("{0} was just created as an instance of {1}\n", this.name, this);
            Tools.Print(newFG: ConsoleColor.Green, text: text);


        }
    }

}
