package packagingservice;

public class Child {

    private final String name;
    private final int age;
    private final ChildGender gender;
    private final boolean hasBeenNice;
    private final Gift assignedGift;

    public Child(String name, int age, ChildGender gender,
                 boolean hasBeenNice, Gift assignedGift) {
        this.name = name;
        this.age = age;
        this.gender = gender;
        this.hasBeenNice = hasBeenNice;
        this.assignedGift = assignedGift;
    }

    public String getName() {
        return name;
    }

    public int getAge() {
        return age;
    }

    public ChildGender getGender() {
        return gender;
    }

    public boolean hasBeenNice() {
        return hasBeenNice;
    }

    public Gift getAssignedGift() {
        return assignedGift;
    }
}
