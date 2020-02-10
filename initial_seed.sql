/* Create Categories */
INSERT INTO dbo.Categories VALUES('Dog');
INSERT INTO dbo.Categories VALUES('Cat');
INSERT INTO dbo.Categories VALUES('Reptile');
INSERT INTO dbo.Categories VALUES('Rabbit');
INSERT INTO dbo.Categories VALUES('Small Animal');

/* Create Diet Plans */
INSERT INTO dbo.DietPlans VALUES('Dog Weight Loss','Grain',1);
INSERT INTO dbo.DietPlans VALUES('Dog Weight Gain','Meat',2);
INSERT INTO dbo.DietPlans VALUES('Cat Weight Loss','Grain',1);
INSERT INTO dbo.DietPlans VALUES('Cat Weight Gain','Meat',2);
INSERT INTO dbo.DietPlans VALUES('Rabbit Weight Gain','Grain',1);

/* Create Employees */
INSERT INTO dbo.Employees VALUES('Jacob', 'Martinez','jmartinez', '123456', 10014, 'jmartinez@humanesociety.org');
INSERT INTO dbo.Employees VALUES('Abraham', 'Sanchez','asanchez', 'abcdef', 10001, 'asanchez@humanesociety.org');
INSERT INTO dbo.Employees VALUES('Joe', 'Smith','jsmith', 'ahsdfa', 10011, 'jsmith@humanesociety.org');
INSERT INTO dbo.Employees VALUES('Alice', 'Wondergirl','awondergirl', 'securepw', 11004, 'awondergirl@humanesociety.org');
INSERT INTO dbo.Employees VALUES('Edith', 'Harrison','eharrison', '654321', 10004, 'eharrison@humanesociety.org');

/* Create Clients */
INSERT INTO dbo.Clients VALUES('Tom', 'Sawyer', 'tsawyer99', 'mypasswrod', NULL, 'tsawyer99@yahoo.com');
INSERT INTO dbo.Clients VALUES('Huck', 'Finn', 'xxhuckxx', 'hukrules', NULL, 'xxhuckxx@aol.com');
INSERT INTO dbo.Clients VALUES('Teresa', 'Mother', 'imasaint', 'praiseme', NULL, 'mt01@gmail.com');
INSERT INTO dbo.Clients VALUES('Brad', 'Johnson', 'bjohnson', 'bj1234', NULL, 'bjohnson@hotmail.com');
INSERT INTO dbo.Clients VALUES('John', 'Cena', 'cantseeme', 'emeestnac', NULL, 'cena@wwe.com');

/* Create Animals Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));*/
INSERT INTO dbo.Animals VALUES('Buttercup', 3, 1, 'Gentle', 1, 0, 'Male', 'Available', 5, NULL, 1);
INSERT INTO dbo.Animals VALUES('Rocky', 75, 7, 'Energetic', 0, 1, 'Male', 'Available', 1, 1, 2);
INSERT INTO dbo.Animals VALUES('Cuddles', 12, 3, 'Gentle', 1, 1, 'Female', 'Adopted', 1, 2, 1);
INSERT INTO dbo.Animals VALUES('Penny', 100, 1, 'Gentle', 1, 0, 'Female', 'Adopted', 3, NULL, 5);
INSERT INTO dbo.Animals VALUES('Arya', 10, 2, 'Energetic', 0, 0, 'Female', 'Available', 2, 4, 4);

/* Create Rooms */ 
INSERT INTO dbo.Rooms VALUES(1001, 3);
INSERT INTO dbo.Rooms VALUES(1002, 5);
INSERT INTO dbo.Rooms VALUES(1003, 1);
INSERT INTO dbo.Rooms VALUES(1004, NULL);
INSERT INTO dbo.Rooms VALUES(1005, 2);
INSERT INTO dbo.Rooms VALUES(1006, NULL);
INSERT INTO dbo.Rooms VALUES(1007, NULL);
INSERT INTO dbo.Rooms VALUES(1008, NULL);
INSERT INTO dbo.Rooms VALUES(1009, NULL);
INSERT INTO dbo.Rooms VALUES(1010, 4);