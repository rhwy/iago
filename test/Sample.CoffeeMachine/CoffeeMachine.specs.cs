using System;
using System.Dynamic;
using Iago;
using Iago.Language;
using NFluent;
using static Iago.Language.Specs;

namespace Sample.CoffeeMachine
{
    public class CoffeeMachineSpecs
    {
        Specify that = () =>
            "CoffeeMachine Should automate the production of coffee";

        public void Run()
        {
            When("coffee machine is started", ()=>
            {
                Then("screen must be equal to 'Welcome'", ()=>{
                    
                    
                });

                And("Total amount inserted should be 0",()=>{
                    
                });
            });


            When("user insert coins",()=>{

                var serieOfCoinsToInsert = new []{
                    new { coin=1,currentTotalExpected=1},
                    new { coin=2,currentTotalExpected=4},
                    new { coin=5,currentTotalExpected=8},
                };

                Then("their value are added to total amount", (values)=>{

                }, serieOfCoinsToInsert);

        });
    }
  }
}
