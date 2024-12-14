namespace AnimalLibrary
{
    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    public enum eFavoriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CommentAttribute : Attribute
    {
        public string Comment { get; }

        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }

    [Comment("Абстрактный класс для животных")]
    public abstract class Animal
    {
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }
        public string WhatAnimal { get; set; }

        public Animal(string country, bool hideFromAnimals, string name, string whatAnimal)
        {
            (Country, HideFromOtherAnimals, Name, WhatAnimal) = (country, hideFromAnimals, name, whatAnimal);
        }
        public Animal() { }

        public abstract eClassificationAnimal GetClassificationAnimal();
        public abstract eFavoriteFood GetFavouriteFood();
        public abstract string SayHello();
        public void Deconstruct(out string country, out bool hideFromOtherAnimals, out string name, out string whatAnimal)
        {
            country = Country;
            hideFromOtherAnimals = HideFromOtherAnimals;
            name = Name;
            whatAnimal = WhatAnimal;
        }
    }

    [Comment("Класс коровы")]
    public class Cow : Animal
    {
        public Cow(string country, bool hideFromOtherAnimals, string name, string whatAnimal) : base(country, hideFromOtherAnimals, name, whatAnimal) { }
        public Cow() { }
        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Plants;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Herbivores;
        }
        public override string SayHello()
        {
            return "Moo";
        }
    }

    [Comment("Класс льва")]
    public class Lion : Animal
    {
        public Lion(string country, bool hideFromOtherAnimals, string name, string whatAnimal) : base(country, hideFromOtherAnimals, name, whatAnimal) { }
        public Lion() { }
        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Meat;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Omnivores;
        }
        public override string SayHello()
        {
            return "Roar";
        }
    }

    [Comment("Класс свиньи")]
    public class Pig : Animal
    {
        public Pig(string country, bool hideFromOtherAnimals, string name, string whatAnimal) : base(country, hideFromOtherAnimals, name, whatAnimal) { }
        public Pig() { }
        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Everything;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Omnivores;
        }
        public override string SayHello()
        {
            return "Oink";
        }
    }
}

