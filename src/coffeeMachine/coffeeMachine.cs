namespace Iago.Samples.CoffeeMachineExperience
{
  public class CoffeeMachine{
    public void Start(){}
    public string Screen {get;} = "Welcome";
    public int TotalAmountInserted {get;private set;} = 0;
    public void InsertCoin(int coin){
      TotalAmountInserted += coin;
    }
  }
}
