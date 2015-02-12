using Xunit;
using NFluent;
using System;
using System.Collections.Generic;
using System.Reflection;

using static NDescribe.Specs;
using static System.Console;

namespace NDescribe.Tests
{
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

  public class CoffeeMachineSpecsTests
  {
    [Fact]
    public void run_the_specs()
    {
      var specs = new CoffeeMachineSpecs();
      FieldInfo myFieldInfo1 = specs.GetType().GetField("that",
            BindingFlags.NonPublic | BindingFlags.Instance);
      var specMessage = myFieldInfo1.GetValue(specs) as Specify;
      WriteLine("[spec] " + specMessage());
      specs.Run();
    }
  }
  public class CoffeeMachine{
    public void Start(){}
    public string Screen {get;} = "Welcome";
  }
}
