using Xunit;
using NFluent;
using System;
using static NDescribe.Specs;

namespace NDescribe.Tests
{
  public class CoffeeMachineSpecs
  {
    Specify that =
      CoffeeMachine =>
        "Sould automate the production of coffee";

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
      new CoffeeMachineSpecs().Run();
    }
  }
  public class CoffeeMachine{
    public void Start(){}
    public string Screen {get;} = "Welcome";
  }
}
