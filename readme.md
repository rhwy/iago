# iago

[![Join the chat at https://gitter.im/rhwy/iago](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/rhwy/iago?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
IAGO is a Simple Bdd style tests library and runner for aspnet K runtime.

#About
Why Iago? There is lots of really great unit testing frameworks and BDD frameworks for the .Net platform, but that doesn't mean that there is no room for difference or improvements.

The values for Iago should be:

* Make it simple as possible to execute tests, even outside an IDE like visual studio.
* Make the experience of writing tests as smooth as possible by avoiding most boiler plate as possible (no inheritance, no attributes, no class per test,...).
* have a better way to name the tests and avoid the C# test naming hell (just because `"then it do something"` is better to write and read than `public void it_shoud_do_something(){}`)
* Use conventions as possible in order to create a smart environment that fit most of the needs as simple as possible.
* Use when adequate the new C#6 features in order to write the shortest and cleaner code possible (string interpolation, high order functions,...)
* Make visualization and reporting as clear as possible.
* Be open and extensible as a key value.

#How to start
Inside the repository, you'll find the project [CoffeeMachine](src/coffeeMachine). It's a sample project that we use while developing the lib and to demo the project. That is a good start point to start using Iago.

If you want to start a new project, this is the very small steps you need to start enjoy testing again.

## Create the project structure

**Note** : _this is preliminary documentation and if you pull the nuget right now, not everything will look like as it is explained here. Check the [todo](todo.md) to see the missing points._

As promised, it will be simple and quick.

* create a new folder, then a project.json.
* I recommend the following starting configuration:


     {
        "name":"HelloWorldLibrary",
        "dependencies":{
                "NFluent":"1.3.1.0",
                "iago.runner":"0.1.0-beta3-5"
        },
        "commands":{
                "test": "iago.runner"
        }
    }

  Let me explain the different lines:

  * `name` : name of your specification project
  * `dependencies`: the libraries you'll depend on
    * `NFluent` : it's the most reliable assertion library that you'll can find. While Iago helps you writing the structure of your tests, NFluent do the final work on really testing the things in a nice and clean way.
    * `iago.runner`: the runner of our testing library, the library itself will be pulled as a dependency of iago.runner.
  * `commands` : the shortcut commands you can run on your project folder.
    * `test` : it's just a shortcut that allows you to run the tests in your folder by running `k test`.

* you're ready for a `kpm restore` to pull the libraries in your workspace.

* run a first test just to check that everything is in place:
  * `k test`,

you should get in return something like that:

     --------------------------------------------------------
          _________
         /         \ ______  _______  _______   v0.2.0.0
         \_    ____//      |/  ___  \/   __  \
           |   |   /   /|  |  |   \__|  /  \  \
           |   |  /   /_|  |  |   __|  |    \  \
         __|   |_/_   __   |  |___|  |  \    |  |
        /         /  /  |  |         |\  \__/  /
        \________/\_/  /____\____/|  | \______/
                                  |__|
         A cool test and spec runner for DNX
     
     --------------------------------------------------------
     [00:000] ● scanning assembly [HelloWorldLibrary]
     [00:002] ● no specification found

* then create your first HelloWorldSpecs (`helloWorldSpecs.cs` but it could be whatever) file and start coding!

## The code

Inside `helloWorldSpecs.cs`, first create your namespace and usings:

         namespace HelloWorld.Specs
         {
           using Iago;
           using static Iago.Specs;
           using NFluent;
     
                public class HelloWorldSpecs
                {  
                }
         }

`Iago` and `Nfluent` are your testing tools.

The line `using static Iago.Specs` is just a smart shortcut that helps you using high order functions of static class Specs (the `When`, `Then` methods are attached to it). This is part of the magic that helps you writing simpler things.

Then the class `HelloWorldSpecs` is your testing class. It acts as a high level container defining the whole spec, scenario or whatever you use to call it.

Note that here you don't need attributes or inheritance because the default loader will look for classes ending with `Spec` and load them automatically. New loaders can be added if you feel better with other conventions [to be implemented]

Run `k test`, you should have that now:

    --------------------------------------------------------
               _________
              /         \ ______  _______  _______   v0.2.0.0
              \_    ____//      |/  ___  \/   __  \
                |   |   /   /|  |  |   \__|  /  \  \
                |   |  /   /_|  |  |   __|  |    \  \
              __|   |_/_   __   |  |___|  |  \    |  |
             /         /  /  |  |         |\  \__/  /
             \________/\_/  /____\____/|  | \______/
                                       |__|
              A cool test and spec runner for DNX
          
          --------------------------------------------------------
          [00:000] ● scanning assembly [coffeeMachine]
          [00:002] ● found 1 Specification
          [00:002] ✔ running HelloWord
          [00:014] ✔ end running HelloWord
          [00:014] ✔

There is still no specs ran but you should see that your spec class was found.


Add a first feature:

         namespace HelloWorld.Specs
         {
           using Iago;
           using static Iago.Specs;
           using NFluent;
     
          public class HelloWorldSpecs
          {  
               Specify that = () =>
                 "Hello world is a very simple demo class";
               
               public HelloWorldSpecs()
               {
                    When("hello machine is created", ()=>{
                      var machine = new HelloMachine();
                    
                    Then("default message should be [hello world]", ()=> {
                         Check.That(machine.Message).IsEqualTo("hello world");
               });
          });
        }
      }
    }

Run your tests, you'll see a compilation error. that's ok, write the minimal code to make it compile:

    public class HelloMachine
    {
      public string Message {get;set;};
    }


Then after a new run, you should see them fail like that:

    --------------------------------------------------------
               _________
              /         \ ______  _______  _______   v0.2.0.0
              \_    ____//      |/  ___  \/   __  \
                |   |   /   /|  |  |   \__|  /  \  \
                |   |  /   /_|  |  |   __|  |    \  \
              __|   |_/_   __   |  |___|  |  \    |  |
             /         /  /  |  |         |\  \__/  /
             \________/\_/  /____\____/|  | \______/
                                       |__|
              A cool test and spec runner for DNX
          
          --------------------------------------------------------
          [00:000] ● scanning assembly [coffeeMachine]
          [00:002] ● found 1 Specification
          [00:002] ✔ running HelloWorld
          [00:002] ✔  => Hello world is a very simple demo class
          [00:003] ✔
          [00:003] ✔   [when] hello machine is created
          [00:003] ✔    [then] default message should be [hello world]
          [00:014] ✘  The checked value is different from the expected one.
          [00:014] ✘  The checked value:
          [00:014] ✘  	[]
          [00:014] ✘  The expected value:
          [00:014] ✘  	["hello world"]
          [00:014] ✔ end running HelloWorld
          [00:014] ✔

**note:**_on the doc all is black & white but in your shell [INFO] is in blue,[PASS] is green and [FAIL] in red._

Now, make your tests pass by adding the expected default value:

    public class HelloMachine
    {
      public string Message {get;set;} = "hello world";
    }  

Then all should be green now:

    --------------------------------------------------------
               _________
              /         \ ______  _______  _______   v0.2.0.0
              \_    ____//      |/  ___  \/   __  \
                |   |   /   /|  |  |   \__|  /  \  \
                |   |  /   /_|  |  |   __|  |    \  \
              __|   |_/_   __   |  |___|  |  \    |  |
             /         /  /  |  |         |\  \__/  /
             \________/\_/  /____\____/|  | \______/
                                       |__|
              A cool test and spec runner for DNX
          
          --------------------------------------------------------
          [00:000] ● scanning assembly [coffeeMachine]
          [00:002] ● found 1 Specification
          [00:002] ✔ running HelloWorld
          [00:002] ✔  => Hello world is a very simple demo class
          [00:003] ✔
          [00:003] ✔   [when] hello machine is created
          [00:003] ✔    [then] default message should be [hello world]
          [00:014] ✔ end running HelloWorld
          [00:014] ✔

_You've just finished your first Iago Spec, congratulations!_

For more detailed exemples, please check the [coffeeMachine project](src/coffeeMachine).

# Development

To keep updated of the next developments or if you want to help, please read the [Todo](todo.md) file.

If you want to suggest an awesome improvement that is not listed on the readme, you're more than welcome to fill an issue with your idea (and tag it for what it is) or send a PR.

For a quickest exchange, feel free to talk about Iago on:

* Jabbr room: [https://jabbr.net/#/rooms/iago](https://jabbr.net/#/rooms/iago)
* twitter : ping me on [@rhwy](https://twitter.com/rhwy) or hashtag [#iagolib](https://twitter.com/hashtag/iagolib?f=realtime).

cheers!

Rui
