using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {            
            switch (crudOperation)
            {                
                case "create": //CREATE
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;
                case "read": //READ
                    if (db.Employees.Select(e => e.EmployeeNumber == employee.EmployeeNumber) == null)
                    {
                        throw new NullReferenceException();
                    }
                    else
                    {
                        db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
                    }
                    break;
                case "update": //UPDATE
                    Employee employeeFromDb = null;
                    try
                    {
                        employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).Single();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("No employees have a EmployeeID that matches the Employee passed in.");
                        Console.WriteLine("No update has been made.");
                        return;
                    }
                    employeeFromDb.FirstName = employee.FirstName;
                    employeeFromDb.LastName = employee.LastName;
                    employeeFromDb.UserName = employee.UserName;
                    employeeFromDb.Password = employee.Password;
                    employeeFromDb.EmployeeNumber = employee.EmployeeNumber;
                    employeeFromDb.Email = employee.Email;
                    db.SubmitChanges();
                    break;
                case "delete": //DELETE                     
                    try
                    {
                        employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).Single();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("No employees have a EmployeeID that matches the Employee passed in.");
                        Console.WriteLine("No changes have been made.");
                        return;
                    }
                    db.Employees.DeleteOnSubmit(employeeFromDb);
                    db.SubmitChanges();
                    break;
            }
        }
         
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            if (db.Animals.Select(a => a.AnimalId == id) == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return db.Animals.Where(a => a.AnimalId == id).FirstOrDefault();
            }
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            Animal animalFromDb = GetAnimalByID(animalId);
            foreach (KeyValuePair<int,string> update in updates)
            {

                switch (update.Key)
                {
                    case 1:
                        animalFromDb.Category = db.Categories.Where(a => a.Name == update.Value).Single();
                        db.SubmitChanges();
                        break;
                    case 2:
                        animalFromDb.Name = update.Value;
                        db.SubmitChanges();
                        break;
                    case 3:
                        animalFromDb.Age = int.Parse(update.Value);
                        db.SubmitChanges();
                        break;
                    case 4:
                        animalFromDb.Demeanor = update.Value;
                        db.SubmitChanges();
                        break;
                    case 5:
                        animalFromDb.KidFriendly = bool.Parse(update.Value);
                        db.SubmitChanges();
                        break;
                    case 6:
                        animalFromDb.PetFriendly = bool.Parse(update.Value);
                        db.SubmitChanges();
                        break;
                    case 7:
                        animalFromDb.Weight = int.Parse(update.Value);
                        db.SubmitChanges();
                        break;
                    default:
                        break;
                }
            }        
            db.SubmitChanges();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            //"1. Category", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. ID"
            var animal = db.Animals.Select(a => a);
            foreach (KeyValuePair<int, string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        animal = animal.Where(a => a.Category.Name == update.Value);
                        break;
                    case 2:
                        animal = animal.Where(a => a.Name == update.Value);
                        break;
                    case 3:
                        animal = animal.Where(a => a.Age == int.Parse(update.Value));
                        break;
                    case 4:
                        animal = animal.Where(a => a.Demeanor == update.Value);
                        break;
                    case 5:
                        animal = animal.Where(a => a.KidFriendly == bool.Parse(update.Value));
                        break;
                    case 6:
                        animal = animal.Where(a => a.PetFriendly == bool.Parse(update.Value));
                        break;
                    case 7:
                        animal = animal.Where(a => a.Weight == int.Parse(update.Value));
                        break;
                    case 8:
                        animal = animal.Where(a => a.CategoryId == int.Parse(update.Value));
                        break;
                }
                
            }
            return animal;
        }
         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var category = db.Categories.Where(c => c.Name == categoryName).SingleOrDefault();
            return category.CategoryId;
        }
        
        internal static Room GetRoom(int animalId)
        {
            var room = db.Rooms.Where(a => a.AnimalId == animalId).SingleOrDefault();
            return room;
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var diet = db.DietPlans.Where(p => p.Name == dietPlanName).SingleOrDefault();
            return diet.DietPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            if (animal.AdoptionStatus == "Available") 
            {

                try
                {
                    Adoption newAdoption = new Adoption();
                    newAdoption.AnimalId = animal.AnimalId;
                    newAdoption.ClientId = client.ClientId;
                    newAdoption.AdoptionFee = 75;
                    newAdoption.PaymentCollected = false;
                    animal.AdoptionStatus = newAdoption.ApprovalStatus = "pending";

                    animal.Adoptions.Add(newAdoption);
                    client.Adoptions.Add(newAdoption);

                    db.Adoptions.InsertOnSubmit(newAdoption);
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                    UserInterface.DisplayUserOptions("Error in adoption, will now be exiting...");
                }
            }
            UserInterface.DisplayUserOptions("This pet is not available.");
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}