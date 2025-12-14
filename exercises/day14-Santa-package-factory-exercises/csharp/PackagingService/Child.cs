namespace PackagingService
{
    public class Child
    {
        public string Name { get; }
        public int Age { get; }
        public ChildGender Gender { get; }
        public bool HasBeenNice { get; }
        public Gift AssignedGift { get; }
        
        public Child(string name, int age, ChildGender gender, 
                     bool hasBeenNice, Gift assignedGift)
        {
            Name = name;
            Age = age;
            Gender = gender;
            HasBeenNice = hasBeenNice;
            AssignedGift = assignedGift;
        }
    }
}
