namespace Iago.Samples.CoffeeMachineExperience
{
  using Iago;
  using static Iago.Specs;
  using NFluent;
  
  public class CoffeeMachineSpecs
  {
    Specify that = () =>
        "CoffeeMachine Sould automate the production of coffee";

    public void Run()
    {
        When("coffee machine is started", ()=> {
          var machine = new CoffeeMachine();
          machine.Start();

          Then("screen must be equal to 'Welcome'", ()=>{
            Check.That(machine.Screen).IsEqualTo("Welcome");
          });
        });
    }
  }

}
