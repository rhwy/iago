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

          And("Total amount inserted should be 0",()=>{
              Check.That(machine.TotalAmountInserted).IsEqualTo(0);
          });
        });


        When("user insert coins",()=>{
          var machine = new CoffeeMachine();
          machine.Start();

          var serieOfCoinsToInsert = new []{
            new { coin=1,currentTotalExpected=1},
            new { coin=2,currentTotalExpected=4},
            new { coin=5,currentTotalExpected=8},
          };

          Then("their value are added to total amount", (values)=>{
            machine.InsertCoin(values.coin);

            Check.That(machine.TotalAmountInserted)
              .IsEqualTo(values.currentTotalExpected);

          }, serieOfCoinsToInsert);

        });
    }
  }

}
