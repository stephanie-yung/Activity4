//Stephanie Yung

interface PhoneNumberInterface
{

    //define 9 digit number
    uint nineDigitNumber
    {
        get;
        set;
    }

    //define phone type: work, personal, home
    string? phoneType
    {
        get;
        set;
    }

    //define international coverage
    bool internationalCoverage
    {
        get;
        set;
    }

    void printAreaCode(); //print area code

    void printType(); //print phoneType

    void printCoverage();

    event EventHandler? PhoneTypeChanged;

}

class Phone : PhoneNumberInterface
{

    //property implementation
    public uint nineDigitNumber { get; set; }
    public string? phoneType { get; set; }
    public bool internationalCoverage { get; set; }


    //print area code implementation
    public void printAreaCode()
    {
        string phonenumberStr = this.nineDigitNumber.ToString();

        string areaCode = phonenumberStr.Substring(0, 3);


        Console.WriteLine($"This is the areacode: {areaCode}");
    }

    //print phone type implementation
    public void printType()
    {
        string? pType = this.phoneType;

        Console.WriteLine($"This is the phone type: {pType}");

    }

    //print phone type implementation
    public void printCoverage()
    {

        Console.WriteLine($"International Coverage on phone: {this.internationalCoverage}");

    }

    //change phone number type
    public void ChangeNumberType()
    {
        Console.WriteLine("Enter new phone type: "); //ask user for new phone type
        string? pType = Console.ReadLine(); //get user input
        this.phoneType = pType; //assign new phone type
        Console.WriteLine($"New phone type: {this.phoneType}"); //print the new phone type
        OnPhoneTypeChanged(); // call to notify subscriber(s)
    }

    //Event

    //1. define a delegate
    public delegate void PhoneTypeChangedEventHandler(object source, EventArgs args);

    //2.define an event based on that delegate
    public event EventHandler? PhoneTypeChanged;


    //public event PhoneTypeChangedEventHandler? PhoneTypeChanged;


    //3. raise the event | notify subscribers
    protected virtual void OnPhoneTypeChanged()
    {
        //check if there are subscribers & notify subscribers
        if (PhoneTypeChanged != null)
            PhoneTypeChanged(this, null!);
            
    }

}

//responsible for when the number is finished changing
public class ChangedNumberTypeService
{
    public void OnPhoneTypeChanged(object source, EventArgs eventArgs)
    {
        Console.WriteLine($"ChangedNumberType (class) Your phone type has been changed.");
    }
}

class MainClass
{
    static void Main()
    {
        Phone phone1 = new Phone(); //publisher

        //assign values
        phone1.nineDigitNumber = 2127724000;
        phone1.phoneType = "Work";
        phone1.internationalCoverage = false;

        //print methods
        phone1.printAreaCode(); 
        phone1.printType();
        phone1.printCoverage();
        

        //event
        var changeTypeService = new ChangedNumberTypeService(); //subscriber
        phone1.PhoneTypeChanged += changeTypeService.OnPhoneTypeChanged!;
        phone1.ChangeNumberType();
        Console.ReadKey();

    }
}